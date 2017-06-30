namespace StingyBot.Common.Nlp
{
    public interface ILexAndParseEngine
    {
        void AddToken(SemanticReplacementToken semanticReplacementToken);
        SimplifiedSentence LexAndParseSentence(string incomingText, object tbdContext = null);
    }
}