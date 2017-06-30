// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace StingyBot.Common.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HandlerInterfaces;
    using Newtonsoft.Json;
    using Nlp;

    [Serializable]
    public class Hear
    {
        
       // private IMessageHandler _ConfiguredMessageHandler;

        [NonSerialized]
        private ICommandHandler _configuredCommandHandler;

        [NonSerialized]
        private IStaticTextResponseHandler _configuredStaticTextResponseHandler;

        [NonSerialized]
        private ILexAndParseEngine _lexAndParseEngine;

        public string Name { get; set; }
        public bool Enabled { get; set; }
        public int EvaluationPriority { get; set; }
        public TriggerType TriggerType { get; set; }
        public HandlerType HandlerType { get; set; }
        public List<SemanticReplacementToken> SemanticReplacementTokens { get; set; }
        public HandlerDef HandlerDef { get; set; }

        /// <summary>
        ///     The configured instance that should handle messages
        /// </summary>
        [JsonIgnore]
        public IMessageHandler ConfiguredMessageHandler { get; set; }
        //{
        //    get { return _ConfiguredMessageHandler; }
        //    set { _ConfiguredMessageHandler = value; }
        //}

        /// <summary>
        ///     The configured instance that should handle commands
        /// </summary>
        [JsonIgnore]
        public ICommandHandler ConfiguredCommandHandler
        {
            get { return _configuredCommandHandler; }
            set { _configuredCommandHandler = value; }
        }

        /// <summary>
        ///     The configured instance that should handle static responses
        /// </summary>
        [JsonIgnore]
        public IStaticTextResponseHandler ConfiguredStaticTextResponseHandler
        {
            get { return _configuredStaticTextResponseHandler; }
            set { _configuredStaticTextResponseHandler = value; }
        }

        /// <summary>
        ///     The configured lexing instance
        /// </summary>
        [JsonIgnore]
        public ILexAndParseEngine LexAndParseEngine
        {
            get { return _lexAndParseEngine; }
            set { _lexAndParseEngine = value; }
        }

        [JsonIgnore]
        public bool HasSemanticTokens { get { return SemanticReplacementTokens != null && SemanticReplacementTokens.Count > 0; }  }

        public List<string> GetTokenNames()
        {
            return SemanticReplacementTokens?.Select(s => s.CoalescedReplacement).ToList();
        }

    }
}