namespace StingyBot.SalesForce
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Common.HandlerInterfaces;
    using Common.Models;
    using Common.Nlp;
    using Microsoft.Extensions.Configuration;
    using Models;
    using Salesforce.Common;
    using Salesforce.Force;
    using SlackConnector.Models;

    public class SalesForceController : LoggableBase, IMessageHandler
    {
        private SalesForceSettings _settings;
        private ForceClient _client; //this is a Service user account
        private const int MAX_CASES_TO_SHOW = 5;

        private CaseMessageFormatter _caseMessageFormatter;
       
        #region "public interface members"

        public void Configure(IConfigurationRoot configRoot)
        {
            _settings = new SalesForceSettings();
            configRoot.Bind(_settings);

            if (_settings.IsValid == false)
            {
                throw new ArgumentException($"Salesforce configuration is not valid, some values are missing.");
            }

            if (_settings.Password.EndsWith(_settings.SecurityToken) == false)
            {
                _settings.Password = _settings.Password + _settings.SecurityToken;
            }

            _caseMessageFormatter = new CaseMessageFormatter();

            GetClientAsync().GetAwaiter().GetResult();
        }

        public Task<Message> AcceptUserInputAsync(MessageContext messageContext)
        {
            if (messageContext.Sentence.EvaluateSpecificTokenNamesMatch(LexiTokenMatch.All, new List<string> {Consts.TOKEN_CASE, Consts.TOKEN_LIST}))
            {
                return GetCaseListAsync(messageContext);
            }
            if (messageContext.Sentence.EvaluateSpecificTokenNamesMatch(LexiTokenMatch.All, new List<string> { Consts.TOKEN_CASE}))
            {
                return GetCaseDetailsAsync(messageContext);
            }
            return null;
        }

        #endregion //#region "public interface members"

        protected virtual async Task<ForceClient> GetClientAsync()
        {
            if (_client == null)
            {
                var auth = new AuthenticationClient();
                var url = _settings.IsSandboxUser
                    ? "https://test.salesforce.com/services/oauth2/Token"
                    : "https://login.salesforce.com/services/oauth2/token";

                //This is a real turd of a method - complete with async you cant do, a ConfigureAwait(dont save my context bro)
                // and weird json serialization errors or "InvalidGrant" instead of just saying "bad password".
                await auth.UsernamePasswordAsync(_settings.ConsumerKey, _settings.ConsumerSecret, _settings.UserName, _settings.Password, url).ConfigureAwait(false);

                _client = new ForceClient(auth.InstanceUrl, auth.AccessToken, auth.ApiVersion);
            }
            return _client;
        }


        //https://github.com/developerforce/Force.com-Toolkit-for-NET/blob/master/samples/SimpleConsole/Program.cs
        public async Task<Message> GetCaseDetailsAsync(MessageContext messageContext)
        {
            var caseElementIndex = messageContext.Sentence.GetIndexOfKnownToken(Consts.TOKEN_CASE);
            var caseNumberElement = messageContext.Sentence.SentenceAsLexedValues[caseElementIndex + 1].Value;
            var caseNumber = caseNumberElement.PadLeft(8, '0');
            var commentProps = new CaseComment().GetCommaSepListOfPropNames();
            var caseQuery = "SELECT Id, CaseNumber, Subject, Description, CreatedDate, OwnerId, LastModifiedDate, IsClosed, Priority " +
                            " , Account.Name " +
                            $" , (Select {commentProps} FROM CaseComments) " +
                            " FROM Case " +
                            $" WHERE CaseNumber = '{caseNumber}'";
            /* 
             * Case.GetSelectQuery() returns all these fields
             * 
             * SELECT Id, IsDeleted, CaseNumber, ContactId, AccountId, AssetId, ParentId, SuppliedName, SuppliedEmail, SuppliedPhone, SuppliedCompany, Type,
             * Status, Reason, Origin, Subject, Priority, Description, IsClosed, ClosedDate, IsEscalated, OwnerId, CreatedDate, CreatedById, LastModifiedDate,
             * LastModifiedById, SystemModstamp, LastViewedDate, LastReferencedDate, EngineeringReqNumber__c, SLAViolation__c, Product__c, PotentialLiability__c FROM Case 
             */


            var client = GetClientAsync().Result;
            var task = await client.QueryAsync<Case>(caseQuery);
            var result = task;

            if (result.Records.Count == 0)
            {
                return new Message { Text = $"records not found for case {caseNumber}" };
            }
            if (result.Records.Count > 1)
            {
                var matchingCaseNumbers = string.Join(",", result.Records.Select(c => c.CaseNumber));
                return new Message { Text = $"Multiple records found {matchingCaseNumbers}" };
            }
            
            var sfCase = result.Records.First();
            
            return _caseMessageFormatter.GetCaseMessage(sfCase);
        }

        public async Task<Message> GetCaseListAsync(MessageContext messageContext)
        {

            var caseQuery = "SELECT Id, CaseNumber, Subject, Description, CreatedDate, OwnerId, LastModifiedDate, IsClosed, Priority " +
                            " , Account.Name " +
                            " FROM Case " +
                            " ORDER BY Id";

            var client = GetClientAsync().Result;
            var task = await client.QueryAsync<Case>(caseQuery);
            var result = task;

            if (result.Records.Count == 0)
            {
                //TODO: move to messsage formatter
                return new Message { Text = "Case records not found" };
            }

            //TODO: move to message formatter
            var att = new SlackAttachment
            {
                Fallback = "Fallback Text",
                ColorHex = "#36a64f",
                MarkdownIn = SlackAttachment.GetAllMarkdownInTypes()
            };

            var headerText = $"Found {result.Records.Count} cases.";
            if (result.Records.Count > MAX_CASES_TO_SHOW)
            {
                headerText += $" Here are the newest  {MAX_CASES_TO_SHOW}";
            }
            var casesMessage = new Message
            {
                Text = headerText,
                Attachments = new List<SlackAttachment> { att }
            };

            var currentCount = 1;
            foreach (var sfCase in result.Records)
            {
                att.Fields.Add(new SlackAttachmentField
                {
                    Title = $"Details for case {sfCase.CaseNumber}",
                    Value = $"{sfCase.CreatedDateShort} - {sfCase.Account.Name} - {sfCase.Subject}",
                    IsShort = false
                });
                currentCount++;
                if (currentCount > MAX_CASES_TO_SHOW)
                {
                    break;
                }
            }

            return casesMessage;
        }

        public async Task<string> DescribeObjectAsync(string objectName)
        {
            var client = GetClientAsync();
            var result = await client.Result.DescribeAsync<object>(objectName);
            return result.ToString();
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}