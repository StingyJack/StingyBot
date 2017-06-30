namespace StingyBot.Common.SlackApi
{
    using System.Collections.Specialized;
    using Newtonsoft.Json;

    /// <summary>
    ///     The base classs for Api request parameter collections
    /// </summary>
    public abstract class ApiRequestParams
    {
        [JsonProperty("token")]
        public string ApiToken { get; set; }

        public abstract string GetMethodName();

        public abstract NameValueCollection GetParams();
    }
}