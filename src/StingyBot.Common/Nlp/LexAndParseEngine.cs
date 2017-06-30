namespace StingyBot.Common.Nlp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    ///     A rudimentary engine to extract user specific phrasing into common patterns 
    /// for machines to understand
    /// </summary>
    /// <remarks>
    ///     Probably should use ANTLR or StanfordNLP, but I havent had time to look at those and
    /// dont even know if they are the right tool for the job. Also Luis.ai, wit.ai, etc
    ///     There are probably conversational contexts that neede to be
    /// </remarks>
    /// <remarks>
    ///     Finding some way to help understand or express a tone of voice via Markup or similar (ACSS, SSML, etc)
    /// would prob be real cool.
    /// </remarks>
    public class LexAndParseEngine : LoggableBase, ILexAndParseEngine
    {
        #region "props / felds"
        
        public List<SemanticReplacementToken> SemanticReplacementTokens { get; private set; } = new List<SemanticReplacementToken>();
        
        #endregion //#region "props / felds"

        #region "ctor"

        /// <summary>
        /// 
        /// </summary>
        public LexAndParseEngine(IEnumerable<SemanticReplacementToken> semanticReplacementTokens)
        {
            SemanticReplacementTokens.AddRange(semanticReplacementTokens);
        }

        #endregion //#region "ctor"

        #region "public members"

        public void AddToken(SemanticReplacementToken semanticReplacementToken)
        {
            if (SemanticReplacementTokens.Any(s => s.CoalescedReplacement.Equals(semanticReplacementToken.CoalescedReplacement, StringComparison.OrdinalIgnoreCase)))
            {
                return;    //not sure if ex is warranted
            }
            SemanticReplacementTokens.Add(semanticReplacementToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="incomingText"></param>
        /// <param name="tbdContext"></param>
        /// <returns></returns>
        public SimplifiedSentence LexAndParseSentence(string incomingText, object tbdContext = null)
        {
            var tokenizedResult = ApplyReplacementTokensToMessageText(incomingText);
            var sentence = new SimplifiedSentence(incomingText, tokenizedResult.Item1, tokenizedResult.Item2);
            return sentence;
        }

        #endregion //#region "public members"

        #region "sentence handling"

        private Tuple<string, List<LexedValue>> ApplyReplacementTokensToMessageText(string incomingMessageText)
        {
            var lexedValues = new List<LexedValue>();

            var splits = incomingMessageText.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            var index = 0;
            foreach (var splitSourceWord in splits)
            {
                var lexedValue = new LexedValue {IndexInCollection = index, Value = splitSourceWord };
                var replacementToken = GetReplacementToken(splitSourceWord);
                if (replacementToken != null)
                {
                    lexedValue.SemanticReplacementToken = replacementToken;
                }
                index++;
                lexedValues.Add(lexedValue);
            }

            var rebuiltSentence = new StringBuilder();
            foreach (var lt in lexedValues)
            {
                rebuiltSentence.AppendFormat("{0} ", lt.HasToken ? lt.SemanticReplacementToken.CoalescedReplacement : lt.Value);
            }
            var resultString = rebuiltSentence.ToString();

            return new Tuple<string, List<LexedValue>>(resultString, lexedValues);
        }

        #endregion //#region "sentence handling"

        #region "helpers"

        private SemanticReplacementToken GetReplacementToken(string sourceWord)
        {
            SemanticReplacementToken match = null;
            foreach (var srt in SemanticReplacementTokens)
            {
                if (srt.SourceWords.Any(s => s.Equals(sourceWord, StringComparison.OrdinalIgnoreCase)))
                {
                    match = srt;
                    break;
                }
            }
            return match;
        }

        #endregion //#region "helpers"
    }
}