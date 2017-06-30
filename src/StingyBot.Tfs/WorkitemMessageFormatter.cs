namespace StingyBot.Tfs
{
    using System;
    using System.Collections.Generic;
    using Common;
    using Common.Models;
    using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
    using SlackConnector.Models;

    /// <summary>
    /// 
    /// </summary>
    public class WorkitemMessageFormatter : IMessageFormatter
    {
        public Message GetSingleWorkitemDetailsMessage(string baseUrl, string projectCollectionName, string projectName, WorkItem workitem)
        {
            if (workitem == null || workitem.Id.HasValue == false)
            {
                throw new ArgumentException("workitem and workitem.id cannot be null");
            }

            var workitemUrl = BuildWorkItemUrl(baseUrl, projectCollectionName, projectName, workitem.Id.Value);
            var workitemType = GetWiField(workitem, "System.WorkItemType");
            var title = GetWiField(workitem, "System.Title");
            var status = GetWiField(workitem, "System.State");
            var summary = GetWiField(workitem, "Microsoft.VSTS.TCM.ReproSteps");
            var message = new Message
                          {
                              Text = $"{workitemType} - {workitem.Id} - {workitemUrl} \n"
                              + $" *Title*: {title} \n"
                              + $" *Status*: {status}     \n"
                              + $" *Summary*: {summary} \n\n"
                              + $" *Last Update*: \n",
                              Attachments = new List<SlackAttachment>()
                          };
            

            return message;
        }

        private string GetWiField(WorkItem workitem, string fieldName)
        {
            if (workitem.Fields.ContainsKey(fieldName))
            {
                return workitem.Fields[fieldName] as string;
            }
            return null;
        }

        protected internal Message GetNoWorkitemsFoundForQuery()
        {
            throw new NotImplementedException();
        }

        protected internal Message GetWorkitemsQueryResult(List<WorkItem> workitems, WorkItemQueryResult queryResult)
        {

            /*
            var recordsToShow = workitems.Count > _TfsConfiguration.MaxRecordsInResultSets
                ? _TfsConfiguration.MaxRecordsInResultSets
                : workitems.Count;

            for (var row = 1; row <= recordsToShow; row++)
            {
            }
            */

            throw new NotImplementedException();
        }

        public static string BuildWorkItemUrl(string baseUrl, string collectionName, string projectName, int id)
        {
            return $"{baseUrl}/{collectionName}/{projectName}/_workitems/edit/{id}";
        }
    }
}