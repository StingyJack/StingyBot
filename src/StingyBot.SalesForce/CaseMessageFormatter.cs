namespace StingyBot.SalesForce
{
    using System.Collections.Generic;
    using Common;
    using Common.Models;
    using Models;
    using SlackConnector.Models;

    public class CaseMessageFormatter : IMessageFormatter
    {
        public Message GetCaseMessage(Case sfCase)
        {
            var commentAttachment = new SlackAttachment();
            foreach (var caseComment in sfCase.CaseComments.Records)
            {
                commentAttachment.Fields.Add(new SlackAttachmentField
                {
                    Title = caseComment.CreatedById,
                    Value = caseComment.CommentBody
                });
            }

            var message = new Message
            {
                Text = "Here is the requested information for the salesforce case",
                Attachments = new List<SlackAttachment>
                {
                    new SlackAttachment
                    {
                        Fallback = "Fallback Text",
                        ColorHex = "#36a64f",
                        PreText = $"Details for case {sfCase.CaseNumber}",
                        MarkdownIn = SlackAttachment.GetAllMarkdownInTypes(),
                        Fields = new List<SlackAttachmentField>
                        {
                            new SlackAttachmentField
                            {
                                Title = "Customer",
                                Value = sfCase.Account.Name,
                                IsShort = true
                            },
                            new SlackAttachmentField
                            {
                                Title = "Case Open Date",
                                Value = sfCase.CreatedDateShort,
                                IsShort = true
                            },
                            new SlackAttachmentField
                            {
                                Title = "Subject",
                                Value = sfCase.Subject,
                                IsShort = true
                            },
                            new SlackAttachmentField
                            {
                                Title = "Owner",
                                Value = sfCase.OwnerId,
                                IsShort = true
                            },
                            new SlackAttachmentField
                            {
                                Title = $"Last Update: {sfCase.LastModifiedDateShort}",
                                Value = "Need to find this field",
                                IsShort = false
                            }
                        }
                    },
                    commentAttachment
                }
            };

            return message;
        }
    }
}