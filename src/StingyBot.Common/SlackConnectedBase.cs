namespace StingyBot.Common
{
    using System;
    using System.Threading.Tasks;
    using SlackApi;
    using SlackConnector;

    public abstract class SlackConnectedBase : LoggableBase
    {
        private static string _slackApiKey;

        protected async Task<ISlackConnection> GetNewSlackConnectionAsync(string apiKey = null, bool saveApiKeyForFutureConnections = false)
        {
            var keyToUse = RegisterApiKey(apiKey, saveApiKeyForFutureConnections);

            if (string.IsNullOrWhiteSpace(keyToUse))
            {
                throw new ArgumentException("Slack API key not found");
            }

            var slackConnector = new SlackConnector();
            var slackConnection = await slackConnector.Connect(apiKey);
            return slackConnection;
        }

        private string RegisterApiKey(string apiKey, bool saveApiKeyForFutureConnections)
        {
            if (saveApiKeyForFutureConnections
                && string.IsNullOrWhiteSpace(apiKey) == true)
            {
                throw new ArgumentNullException(nameof(apiKey), "cant save null or empty api key for future connections");
            }

            if (string.IsNullOrWhiteSpace(apiKey) == true
                && string.IsNullOrWhiteSpace(_slackApiKey) == true)
            {
                throw new InvalidOperationException("No api key presented to use for connections");
            }

            if (saveApiKeyForFutureConnections
                && string.IsNullOrWhiteSpace(apiKey) == false)
            {
                _slackApiKey = apiKey;
                return _slackApiKey;
            }

            return _slackApiKey;
        }

        protected MethodExecutor GetMethodExecutor()
        {
            return new MethodExecutor(_slackApiKey);
        }
    }
}