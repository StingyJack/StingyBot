namespace StingyBot.Common.SlackApi
{
    using System.Collections.Specialized;

    /// <summary>
    ///     The usual fields in a slash command or webhook post that is sent from slack.
    /// </summary>
    public class PostMessage
    {
        public string Token { get; set; }
        public string TeamId { get; set; }
        public string TeamDomain { get; set; }
        public string ChannelId { get; set; }
        public string ChannelName { get; set; }
        public string Timestamp { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public string TriggerWord { get; set; }
        public string ResponseUrl { get; set; }
        public string Command { get; set; }

        public PostMessage()
        {
        }

        public PostMessage(NameValueCollection requestData)
        {
            Token = requestData["token"];
            TeamId = requestData["team_id"];
            TeamDomain = requestData["team_domain"];
            ChannelId = requestData["channel_id"];
            ChannelName = requestData["channel_name"];
            Timestamp = requestData["timestamp"];
            UserId = requestData["user_id"];
            UserName = requestData["user_name"];
            Text = requestData["text"];
            TriggerWord = requestData["trigger_word"];
            ResponseUrl= requestData["response_url"];
            Command= requestData["command"];
        }
    }
}