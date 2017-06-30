namespace StingyBot.Common.Configuration
{
    using System;

    [Serializable]
    public class Knows
    {
        public string Key { get; set; }
        public bool Enabled { get; set; }
        public string MessageFile { get; set; }
    }
}