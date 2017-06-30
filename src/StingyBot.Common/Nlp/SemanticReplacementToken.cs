namespace StingyBot.Common.Nlp
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class SemanticReplacementToken
    {
        public List<string> SourceWords { get; set; }
        public string CoalescedReplacement { get; set; }
        public bool IsRequired { get; set; }
    }
}