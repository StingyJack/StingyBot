namespace StingyBot.Common.Tests
{
    using System.Collections.Generic;
    using Nlp;
    using NUnit.Framework;

    [TestFixture]
    public class LexedAndParsedSentenceTests
    {
        [Test]
        public void GetRawSentenceBetweenTokensFull()
        {
            var candidateSentence = "hello there wiql do the nasty thang; I love it.";
            var tokenizedSentence = "hello there TOKEN_WIQL do the nasty thangTOKEN_SEMICOLON I love it.";
            var sentenceAsLexedValues = new List<LexedValue>
            {
                new LexedValue
                {
                    IndexInCollection = 0,
                    IndexInOriginalString = 0,
                    SemanticReplacementToken = null,
                    Value = "hello"
                }
            };
            var sentence = new SimplifiedSentence(candidateSentence, tokenizedSentence, sentenceAsLexedValues);

            var result = sentence.GetRawSentenceTextBetweenTokens("TOKEN_WIQL", "TOKEN_SEMICOLON");

            Assert.AreEqual(" do the nasty thang", result);
        }
    }
}