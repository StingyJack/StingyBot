// ReSharper disable once UnusedMember.Global
namespace StingyBot.Common.Configuration
{
    using System;

    [Serializable]
    public class AdditionalConfig
    {
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }
    }
}