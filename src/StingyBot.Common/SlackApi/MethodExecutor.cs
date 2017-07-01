namespace StingyBot.Common.SlackApi
{
    using System.Net;
    using System.Text;

    public partial class MethodExecutor
    {
        private readonly string _slackBaseUrl;
        private readonly string _slackApiKey;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MethodExecutor" /> class.
        /// </summary>
        /// <param name="slackApiKey">The slack API key.</param>
        /// <param name="slackBaseUrl">The slack base URL. Use this to specify an alternate endpoint </param>
        public MethodExecutor(string slackApiKey = null, string slackBaseUrl = "https://slack.com/api/")
        {
            _slackApiKey = slackApiKey;
            _slackBaseUrl = slackBaseUrl;
        }

        /// <summary>
        /// Executes the specified API request parameters.
        /// </summary>
        /// <param name="apiRequestParams">The API request parameters.</param>
        /// <returns></returns>
        public MethodResponseBase Execute(ApiRequestParams apiRequestParams)
        {
            var url = $"{_slackBaseUrl}{apiRequestParams.GetMethodName()}";
            if (string.IsNullOrWhiteSpace(apiRequestParams.ApiToken)) { apiRequestParams.ApiToken = _slackApiKey; }

            var nvc = apiRequestParams.GetParams();
            string responseText;
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var responseBytes = client.UploadValues(url, "POST", nvc);
                responseText = Encoding.Default.GetString(responseBytes);
            }

            return MethodResponseBase.Parse(responseText);
        }

        public T Execute<T>(ApiRequestParams apiRequestParams) where T : MethodResponseBase
        {
            var url = $"{_slackBaseUrl}{apiRequestParams.GetMethodName()}";
            if (string.IsNullOrWhiteSpace(apiRequestParams.ApiToken)) { apiRequestParams.ApiToken = _slackApiKey; }

            var nvc = apiRequestParams.GetParams();
            string responseText;
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var responseBytes = client.UploadValues(url, "POST", nvc);
                responseText = Encoding.Default.GetString(responseBytes);
            }

            var response = MethodResponseBase.Parse<T>(responseText);

            return response;
        }
    }
}