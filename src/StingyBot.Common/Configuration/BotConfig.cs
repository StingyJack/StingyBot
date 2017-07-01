namespace StingyBot.Common.Configuration
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class BotConfig
    {
        public SlackConfig SlackConfig { get; set; }

        public string BaseWebHookAddress { get; set; }

        public string WebHookApiName { get; set; }

        public string WebHookRouteTemplate { get; set; }

        public List<Hear> Hears { get; set; } = new List<Hear>();

        public List<Knows> Knows { get; set; } = new List<Knows>();

        public bool? RequiresExternalWebHookAccess { get; set; }

        public List<HandlerDef> BackgroundTaskHandlerDefs { get; set; }

        public List<string> GetConfigurationErrors()
        {
            var returnValue = new List<string>();
            if (SlackConfig == null)
            {
                returnValue.Add("Slack config is null");
            }

            //fill this out. Check for mismatched items 
            // slashcommand without token
            //  message handler without definition
            //  etc, see SlashCommandManager.ProcessCommandAsync for more

            return returnValue;
        }
    }
}