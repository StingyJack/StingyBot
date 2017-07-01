namespace StingyBot.Common.Nlp
{
    using Configuration;

    public class MatchScore
    {
        public Hear Hear { get; set; }
        public int Score { get; set; }
        public SimplifiedSentence Sentence { get; set; }
    }
}