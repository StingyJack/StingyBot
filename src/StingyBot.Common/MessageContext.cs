namespace StingyBot.Common
{
    using Nlp;

    public class MessageContext
    {
        public string RawMessage { get; set; }

        public SimplifiedSentence Sentence { get; set; }

        public string ChannelName { get; set; }

        public string UserName { get; set; }
    }
}