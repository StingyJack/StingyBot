namespace StingyBot.Tfs
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Microsoft.TeamFoundation.Core.WebApi;
    using Microsoft.VisualStudio.Services.WebApi;

    /// <summary>
    ///     The collection of known tfs team collections, projects, and other cached info
    /// </summary>
    public class TfsKnownElements
    {
        public string BaseUrl { get; }
        public Dictionary<string, TeamProjectCollectionReference> KnownProjectCollectionReferences { get; set; }
        public Dictionary<string, TeamProjectCollection> KnownProjectCollections { get; set; }
        public Dictionary<string, TeamProjectReference> KnownProjectReferences { get; set; }
        public Dictionary<string, string> KnownProjectToCollectionMap { get; set; }

        public bool IsAllPresent
        {
            get
            {
                if (string.IsNullOrWhiteSpace(BaseUrl) == false)
                {
                    return true;
                }
                return false;
            }
        }

        public TfsKnownElements(string baseUrl)
        {
            BaseUrl = baseUrl;
            KnownProjectCollectionReferences = new Dictionary<string, TeamProjectCollectionReference>(StringComparer.OrdinalIgnoreCase);
            KnownProjectCollections = new Dictionary<string, TeamProjectCollection>(StringComparer.OrdinalIgnoreCase);
            KnownProjectReferences = new Dictionary<string, TeamProjectReference>(StringComparer.OrdinalIgnoreCase);
            KnownProjectToCollectionMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        public void ClearKnownProperties()
        {
            KnownProjectCollectionReferences.Clear();
            KnownProjectReferences.Clear();
            KnownProjectToCollectionMap.Clear();
        }

        public string BuildWorkingCollectionUrl(string collectionName)
        {
            var collection = KnownProjectCollections[collectionName];
            var refLink = collection.Links.Links["web"] as ReferenceLink;
            Debug.Assert(refLink != null, "refLink != null");
            return refLink.Href;
        }


        public MostLikelyProjectInfo GetMostLikelyProject(string projectHint)
        {
            TeamProjectReference locatedProjectReference = null;

            foreach (var project in KnownProjectReferences)
            {
                if (project.Value.Name.Equals(projectHint, StringComparison.OrdinalIgnoreCase)
                    || project.Value.Description?.IndexOf(projectHint, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    locatedProjectReference = project.Value;
                    break;
                }
            }

            if (locatedProjectReference == null)
            {
                throw new InvalidOperationException("Could not locate a most likely project");
            }

            var collectionName = KnownProjectToCollectionMap[locatedProjectReference.Name];
            var locatedCollectionReference = KnownProjectCollectionReferences[collectionName];
            return new MostLikelyProjectInfo
            {
                TeamProjectCollectionRef = locatedCollectionReference,
                TeamProjectRef = locatedProjectReference
            };
        }
    }
}