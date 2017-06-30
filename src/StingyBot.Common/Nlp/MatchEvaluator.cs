namespace StingyBot.Common.Nlp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Configuration;

    public static class MatchEvaluator 
    {
        public static MatchResult EvaluateMatches(List<Hear> hears, string incomingMessageText)
        {
            const int SCORE_SOME = 50; //const int SCORE_ALL = 100; isnt used
            const int SCORE_NONE = 0;

            var handlerMatchScores = new List<MatchScore>();
            MatchScore allMatchesFound = null;

            //follow the rules in Readme-botConfig.json ### hearing priority 
            foreach (var hear in hears.Where(h => h.HasSemanticTokens == true).OrderBy(h => h.EvaluationPriority))
            {
                var lnpEngine = hear.LexAndParseEngine;
                var sentence = lnpEngine.LexAndParseSentence(incomingMessageText);
                var hearTokenNames = hear.GetTokenNames();

                var matchCount = sentence.GetCountOfMatchingTokens(hearTokenNames);

                var hms = new MatchScore { Hear = hear, Sentence = sentence };

                if (hearTokenNames.Count == matchCount)
                {
                    allMatchesFound = hms;
                    break;
                }

                if (matchCount > 0)
                {
                    hms.Score = SCORE_SOME;
                }
                else
                {
                    hms.Score = SCORE_NONE;
                }

                handlerMatchScores.Add(hms);

            }

            if (incomingMessageText.IndexOf("help", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return new MatchResult {IsPossibleHelpRequest = true};
            }

            if (allMatchesFound == null && handlerMatchScores.Count == 0)
            {
                return null;
            }

            Hear bestMatchingHear;
            SimplifiedSentence bestMatchingSentence;

            if (allMatchesFound != null)
            {
                bestMatchingHear = allMatchesFound.Hear;
                bestMatchingSentence = allMatchesFound.Sentence;
            }
            else
            {
                var bestMatch = handlerMatchScores.OrderBy(h => h.Hear.EvaluationPriority)
                    .ThenByDescending(h => h.Score).First();
                bestMatchingHear = bestMatch.Hear;
                bestMatchingSentence = bestMatch.Sentence;
            }

            return new MatchResult
            {
                MatchScores = handlerMatchScores,
                BestMatchingSentence = bestMatchingSentence,
                BestMatchingHear = bestMatchingHear
            };
        }
    }
}
