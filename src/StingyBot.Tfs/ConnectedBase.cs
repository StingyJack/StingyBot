namespace StingyBot.Tfs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Microsoft.Extensions.Configuration;
    using Microsoft.TeamFoundation.Core.WebApi;
    using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
    using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
    using Microsoft.VisualStudio.Services.Common;
    using Microsoft.VisualStudio.Services.WebApi;

    public abstract class ConnectedBase : LoggableBase
    {
        #region "props and fields"

        protected TfsKnownElements TfsKnownElements { get; set; }

        #endregion //#region "props and fields"

        protected TfsConfiguration LoadTfsConfiguration(IConfigurationRoot configRoot)
        {
            var tfsConfig = new TfsConfiguration();
            configRoot.Bind(tfsConfig);
            if (tfsConfig.IsValid == false)
            {
                throw new ArgumentException($"Tfs configuration is not valid: {tfsConfig.Instances?.Count} instances");
            }

            var onlySupportsOneTfsInstance = tfsConfig.Instances.First();
            var baseUrl = onlySupportsOneTfsInstance.InstanceUrl;

            if (onlySupportsOneTfsInstance.AutoLoadInstanceMetaData)
            {
                LogInfo($"Auto loading Tfs instance metadata...");
                RebuildTfsKnownElementsAsync(baseUrl).GetAwaiter().GetResult();
                LogInfo($"Auto load of Tfs instance metadata complete");
            }
            else
            {
                LogInfo($"Auto load of Tfs instance metadata was skipped");
                TfsKnownElements = new TfsKnownElements(baseUrl);
            }
            return tfsConfig;
        }

        #region "Connection members"

        protected virtual VssConnection GetServerConnection()
        {
            var serverConnection = new VssConnection(new Uri(TfsKnownElements.BaseUrl), new VssCredentials());
            return serverConnection;
        }

        protected virtual async Task<IEnumerable<TeamProjectCollectionReference>> GetTeamProjectCollectionsAsync()
        {
            var serverConnection = GetServerConnection();
            var collectionClient = serverConnection.GetClient<ProjectCollectionHttpClient>();
            return await collectionClient.GetProjectCollections();
        }

        protected virtual async Task<TeamProjectCollection> GetTeamProjectCollectionAsync(string id)
        {
            var serverConnection = GetServerConnection();
            var collectionClient = serverConnection.GetClient<ProjectCollectionHttpClient>();
            return await collectionClient.GetProjectCollection(id);
        }

        protected virtual VssConnection GetProjectCollectionConnection(string collectionUrl)
        {
            var serverConnection = new VssConnection(new Uri(collectionUrl), new VssCredentials());
            return serverConnection;
        }

        protected virtual async Task<IEnumerable<TeamProjectReference>> GetTeamProjectsAsync(string collectionName)
        {
            var collectionUrl = TfsKnownElements.BuildWorkingCollectionUrl(collectionName);
            var serverConnection = GetProjectCollectionConnection(collectionUrl);
            var projectClient = serverConnection.GetClient<ProjectHttpClient>();
            return await projectClient.GetProjects(ProjectState.All, 100, 0);
        }

        protected virtual async Task<List<WorkItemField>> GetWorkitemFields(MostLikelyProjectInfo mostLikelyProjectInfo)
        {
            var witClient = GetWorkItemTrackingHttpClient(mostLikelyProjectInfo);
            return await witClient.GetFieldsAsync();
        }

        #endregion //#region "Connection members"

        protected async Task<bool> RebuildTfsKnownElementsAsync(string baseUrl)
        {
            if (TfsKnownElements == null)
            {
                TfsKnownElements = new TfsKnownElements(baseUrl);
            }

            TfsKnownElements.ClearKnownProperties();

            var projectCollections = await GetTeamProjectCollectionsAsync();

            foreach (var projectCollectionReference in projectCollections)
            {
                var projectCollection = await GetTeamProjectCollectionAsync(projectCollectionReference.Id.ToString());
                TfsKnownElements.KnownProjectCollectionReferences.Add(projectCollectionReference.Name, projectCollectionReference);
                TfsKnownElements.KnownProjectCollections.Add(projectCollection.Name, projectCollection);

                foreach (var projectReference in await GetTeamProjectsAsync(projectCollectionReference.Name))
                {
                    TfsKnownElements.KnownProjectReferences.Add(projectReference.Name, projectReference);
                    TfsKnownElements.KnownProjectToCollectionMap.Add(projectReference.Name, projectCollectionReference.Name);
                }
            }

            return true;
        }


        #region "client members"

        protected virtual WorkItemTrackingHttpClient GetWorkItemTrackingHttpClient(MostLikelyProjectInfo mostLikelyConnectionInfo)
        {
            var collectionUrl = TfsKnownElements.BuildWorkingCollectionUrl(mostLikelyConnectionInfo.TeamProjectCollectionRef.Name);
            var connection = GetProjectCollectionConnection(collectionUrl);

            var witClient = connection.GetClient<WorkItemTrackingHttpClient>();
            return witClient;
        }

        protected virtual WorkItemTrackingHttpClient GetWorkItemTrackingHttpClient(MessageContext messageContext)
        {
            var mostLikelyTeamProject = TfsKnownElements.GetMostLikelyProject(messageContext.ChannelName);
            return GetWorkItemTrackingHttpClient(mostLikelyTeamProject);
        }

        #endregion //#region "client members"
    }
}