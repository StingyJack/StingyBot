namespace StingyBot.Common.Configuration
{
    using System;

    [Serializable]
    public class StaticMessage
    {
        public string Culture { get; set; }
        public string StaticText { get; set; }
    }
}