namespace StingyBot.Common.Configuration
{
    using System;

    [Serializable]
    public class HandlerDef
    {
        /// <summary>
        ///     Assembly qualified type name
        /// </summary>
        public string HandlerFullTypeName { get; set; }

        /// <summary>
        ///     A list of json config file names to use for loading (lack of comments making me regret json files for config)
        /// </summary>
        public string[] ConfigFiles { get; set; }

        /// <summary>
        ///     Command handler configuration
        /// </summary>
        public CommandConfig CommandConfig { get; set; }

        /// <summary>
        ///     The static message file
        /// </summary>
        public string MessageFile { get; set; }
    }
}