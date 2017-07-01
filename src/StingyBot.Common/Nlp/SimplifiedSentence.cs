namespace StingyBot.Common.Nlp
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    ///     Represents a natural language sentence, that has been and replaced 
    /// with semantic tokens to make it easier for the program to use. 
    /// </summary>
    /// <remarks>
    ///     This is a "Machine Sentence" or "Program Friendly Sentence". It takes 
    /// "unparalleled", "wonderful", "incontrovertible" and makes them all "nice"
    /// </remarks>
    public class SimplifiedSentence
    {
        #region "data members and consts"

        private readonly List<LexedValue> _sentenceAsLexedValues;

        public string OriginalSentence { get; }

        public string Sentence { get; }

        public ReadOnlyCollection<LexedValue> SentenceAsLexedValues { get; }

        #endregion //#region "data members and consts"

        #region "ctor"

        /// <summary>
        /// </summary>
        /// <param name="candidateSentence"></param>
        /// <param name="tokenizedSentence"></param>
        /// <param name="sentenceAsLexedValues"></param>
        public SimplifiedSentence(string candidateSentence, string tokenizedSentence, List<LexedValue> sentenceAsLexedValues)
        {
            _sentenceAsLexedValues = sentenceAsLexedValues;
            SentenceAsLexedValues = new ReadOnlyCollection<LexedValue>(_sentenceAsLexedValues);
            OriginalSentence = candidateSentence;
            Sentence = tokenizedSentence;
        }

        #endregion //#region     "ctor"

        #region "private members"

        #endregion //#region "private members"

        #region "public members"

        public int GetIndexOfKnownToken(string tokenName)
        {
            var lexValue = _sentenceAsLexedValues.FirstOrDefault(l => l.HasToken
                                                                      && l.SemanticReplacementToken.CoalescedReplacement.Equals(tokenName, StringComparison.OrdinalIgnoreCase));

            return lexValue == null ? -1 : lexValue.IndexInCollection;
        }

        public bool EvaluateSpecificTokenNamesMatch(LexiTokenMatch minMatchCondition, List<string> tokenNames)
        {
            var currentMatchCondition = LexiTokenMatch.None;
            var tokenCount = tokenNames.Count;
            var matchCount = GetCountOfMatchingTokens(tokenNames);

            if (matchCount == 0)
            {
                currentMatchCondition = LexiTokenMatch.None;
            }
            else if (matchCount < tokenCount)
            {
                currentMatchCondition = LexiTokenMatch.Some;
            }
            else if (matchCount >= tokenCount)
            {
                currentMatchCondition = LexiTokenMatch.All;
            }

            return currentMatchCondition >= minMatchCondition;
        }

        public int GetCountOfMatchingTokens(List<string> tokenNames)
        {
            var matchCount = 0;

            foreach (var token in tokenNames)
            {
                if (GetIndexOfKnownToken(token) >= 0)
                {
                    matchCount++;
                }
            }
            return matchCount;
        }

        /// <summary>
        ///     Gets the non token replaced sentence content using two tokens as exclusive boundaries
        /// </summary>
        /// <param name="startingTokenExclusive"></param>
        /// <param name="endingTokenExclusive"></param>
        /// <returns></returns>
        /// <example>
        ///     Given the text/replacement tokens are "wiql/TOKEN_WIQL" and ";/TOKEN_SEMICOLON"
        ///     if the sentence is "hello there wiql do the nasty thang; I love it."
        ///     The result would be " do the nasty thang"
        /// </example>
        public string GetRawSentenceTextBetweenTokens(string startingTokenExclusive, string endingTokenExclusive)
        {
            throw new NotImplementedException("fuck. if I really need this, go get the stanfordNLP or something else.");
        }

        #endregion //#region "public members"
    }
}