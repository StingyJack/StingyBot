namespace StingyBot.Common.Nlp
{
    using System.Collections.Generic;
    using Configuration;

    public class MatchResult
    {
        public List<MatchScore> MatchScores { get; set; } = new List<MatchScore>();

        public Hear BestMatchingHear { get; set; }

        public SimplifiedSentence BestMatchingSentence { get; set; }

        public bool IsPossibleHelpRequest { get; set; }
    }
}