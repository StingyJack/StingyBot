// ReSharper disable ClassNeverInstantiated.Global
namespace StingyBot.Common.Configuration
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class CommandConfig
    {
        public string SlashCommand { get; set; }
        public string HostAddress { get; set; }
        public List<AdditionalConfig> AdditionalConfigs { get; set; }
        public string VerificationToken { get; set; }
    }
}