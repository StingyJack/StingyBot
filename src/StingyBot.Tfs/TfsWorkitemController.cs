namespace StingyBot.Tfs
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
    using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

    //https://www.visualstudio.com/en-us/docs/integrate/get-started/client-libraries/samples
    public class TfsWorkitemController : ConnectedBase, IMessageHandler
    {
        #region "props and fields"

        private WorkitemMessageFormatter _workitemMessageFormatter;

        #endregion //#region "props and fields"

        #region "public interface members"

        public void Configure(IConfigurationRoot configRoot)
        {
            LoadTfsConfiguration(configRoot);

            _workitemMessageFormatter = new WorkitemMessageFormatter();
        }

        public async Task<Message> AcceptUserInputAsync(MessageContext messageContext)
        {
            //figure out what function this needs to perform
            if (messageContext.Sentence.EvaluateSpecificTokenNamesMatch(LexiTokenMatch.All, new List<string> {Consts.TOKEN_WORKITEM}))
            {
                return await GetSingleWorkitemDetailByIdAsync(messageContext);
            }
            if (messageContext.Sentence.EvaluateSpecificTokenNamesMatch(LexiTokenMatch.All, new List<string> {Consts.TOKEN_WIQL}))
            {
                return await GetWorkitemDetailsByWiqlQueryAsync(messageContext);
            }
            return null;
        }

        #endregion //#region "public interface members"

        #region "workitem handling"

        //pulll in remaining items, add formatter/transformer for messages
        //then work out intents

        private async Task<Message> GetSingleWorkitemDetailByIdAsync(MessageContext messageContext)
        {
            //assumes workitem id follows the workitem token.
            var indexOfWorkitemToken = messageContext.Sentence.GetIndexOfKnownToken(Consts.TOKEN_WORKITEM);
            var lexedRecord = messageContext.Sentence.SentenceAsLexedValues[indexOfWorkitemToken + 1];
            var workItemId = Convert.ToInt32(lexedRecord.Value);

            var mostLikelyTeamProject = TfsKnownElements.GetMostLikelyProject(messageContext.ChannelName);
            var witClient = GetWorkItemTrackingHttpClient(mostLikelyTeamProject);

            var workitem = await witClient.GetWorkItemAsync(workItemId);
            var message = _workitemMessageFormatter.GetSingleWorkitemDetailsMessage(TfsKnownElements.BaseUrl,
                mostLikelyTeamProject.TeamProjectCollectionRef.Name,
                mostLikelyTeamProject.TeamProjectRef.Name, workitem);

            return message;
        }

        public async Task<Message> GetWorkitemDetailsByWiqlQueryAsync(MessageContext messageContext)
        {
            var mostLikelyTeamProject = TfsKnownElements.GetMostLikelyProject(messageContext.ChannelName);
            var witClient = GetWorkItemTrackingHttpClient(messageContext);

            var wiql = GetWiqlStatementFromMessageContext(messageContext);
            if (string.IsNullOrWhiteSpace(wiql))
            {
                return new Message {Text = "Learn to query scrub"};
            }

            var result = await witClient.QueryByWiqlAsync(new Wiql {Query = wiql}, mostLikelyTeamProject.TeamProjectRef.Name);
            if (result.WorkItems.Any() == false)
            {
                return _workitemMessageFormatter.GetNoWorkitemsFoundForQuery();
            }

            var workitems = await witClient.GetWorkItemsAsync(result.WorkItems.Select(w => w.Id));

            return _workitemMessageFormatter.GetWorkitemsQueryResult(workitems, result);
        }

        private string GetWiqlStatementFromMessageContext(MessageContext messageContext)
        {
            var sentence = messageContext.Sentence;

            var wiqlToken = sentence.SentenceAsLexedValues.FirstOrDefault(w => w.HasToken
                                                                               &&
                                                                               w.SemanticReplacementToken.CoalescedReplacement.Equals(Consts.TOKEN_WIQL,
                                                                                   StringComparison.OrdinalIgnoreCase));
            if (wiqlToken == null)
            {
                return null;
            }

            var wiqlStart = sentence.OriginalSentence.IndexOf("\"", wiqlToken.IndexInOriginalString, StringComparison.Ordinal);
            var wiqlEnd = sentence.OriginalSentence.IndexOf("\"", wiqlStart + 1, StringComparison.OrdinalIgnoreCase);

            var wiql = sentence.OriginalSentence.Substring(wiqlStart + 1, wiqlEnd - wiqlStart - 1);

            return wiql;
        }

        /*
        private async Task<WorkItem> CreateWorkRequestAsync(string requestor, string requestedWork, string customerName,
            string channelHint)
        {
            var mostLikelyTeamProject = TfsKnownElements.GetMostLikelyProject(channelHint);

            var connection = new VssConnection(new Uri(TfsKnownElements.BaseUrl), new VssCredentials());
            var witClient = connection.GetClient<WorkItemTrackingHttpClient>();
            var document = new JsonPatchDocument
                           {
                               new JsonPatchOperation
                               {
                                   From = "Title",
                                   Path = "/fields/System.Title",
                                   Operation = Operation.Add
                               }
                           };

            var type = "Product Backlog Item";
            return await witClient.CreateWorkItemAsync(document, mostLikelyTeamProject.TeamProjectRef.Name, type);
        }
        */

        #endregion //#region "workitem handling"

        #region "utils"

        public void Dispose()
        {
            //nothing unmanaged to dispose for this.
        }

        #endregion //#region "utils"
    }
}