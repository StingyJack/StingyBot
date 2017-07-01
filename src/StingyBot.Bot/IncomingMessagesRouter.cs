namespace StingyBot.Bot
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Common.Configuration;
    using Common.Models;
    using Common.Nlp;
    using Common.SlackApi;
    using Common.SlackApi.Methods;
    using Handlers;
    using SlackConnector.Models;

    /// <summary>
    /// </summary>
    /// <remarks>
    ///     Trying to connect to slack to debug? - http://stackoverflow.com/questions/37573932/how-to-test-a-local-bot
    /// </remarks>
    public class IncomingMessagesRouter : LoggableBase
    {
        public ContactDetails BotSelf { get; set; }
        private readonly string _botInternalSelfIdent;
        public ContactDetails TeamDetails { get; set; }
        protected readonly BotConfig _BotConfig;
        private readonly StaticTextResponseHandler _defaultHelpResponseHandler; //TODO: This shouldnt be calling a specific handler and needing a reference.

        public IncomingMessagesRouter(ContactDetails botSelf, ContactDetails teamDetails, BotConfig botConfig)
        {
            BotSelf = botSelf;
            _botInternalSelfIdent = BotSelf.GetBotInternalSelfIdent();
            TeamDetails = teamDetails;
            _BotConfig = botConfig;
            _defaultHelpResponseHandler = new StaticTextResponseHandler();
            _defaultHelpResponseHandler.Configure(_BotConfig.Knows.First(k => k.Enabled == true && k.Key == "defaultHelp").MessageFile);
        }

        public async Task<Message> ProcessIncomingMessageAsync(Message incomingMessage)
        {
            if (incomingMessage == null)
            {
                return new Message {Text = $"{BotSelf.Name} is not a null modem"};
            }

            if (IsActionPotentiallyRequiredForThisBot(incomingMessage.Text, incomingMessage.ChatHub.Id) == false)
            {
                return null;
            }

            var incomingMessageText = incomingMessage.Text.Trim();
            incomingMessageText = incomingMessageText.Replace(BotSelf.GetBotInternalSelfIdent(), $"@{BotSelf.Name}"); //TODO: Is this necessary?

            var matchResult = MatchEvaluator.EvaluateMatches(_BotConfig.Hears, incomingMessageText);

            if (matchResult == null || matchResult.BestMatchingHear == null || matchResult.BestMatchingSentence == null)
            {
                LogErr(
                    $"Required match elements not found: matchResult {matchResult}, matchResult.BestMatchingHear {matchResult?.BestMatchingHear}," +
                    $"matchResult.BestMatchingSentence {matchResult?.BestMatchingSentence}.");
                return GetUnknownCommandMessage(incomingMessageText);
            }

            var messageContext = new MessageContext
            {
                Sentence = matchResult.BestMatchingSentence,
                RawMessage = incomingMessageText,
                ChannelName = incomingMessage.ChatHub.Name,
                UserName = "why isnt this present?" //Fix in SlackConnector and submit PR. add dll to references folder in the meantime.
            };

            if (matchResult.IsPossibleHelpRequest)
            {
                return await _defaultHelpResponseHandler.ConstructResponse(messageContext);
            }


            if (matchResult.BestMatchingHear.HandlerType == HandlerType.MessageHandler)
            {
                return await matchResult.BestMatchingHear.ConfiguredMessageHandler.AcceptUserInputAsync(messageContext);
            }

            if (matchResult.BestMatchingHear.HandlerType == HandlerType.StaticResponse)
            {
                return await matchResult.BestMatchingHear.ConfiguredStaticTextResponseHandler.ConstructResponse(messageContext);
            }

            return null;
        }


        private bool IsActionPotentiallyRequiredForThisBot(string incomingMessageText, string channelId)
        {
            if (IsThisTheBotDmChannel(channelId))
            {
                return true;
            }

            if (incomingMessageText.IndexOf(_botInternalSelfIdent, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true; //anything this should listen for? "/commands", etc)
            }
            // slashcommands may not come through here
            if (incomingMessageText.IndexOf("/", StringComparison.OrdinalIgnoreCase) != 0)
            {
                return false; //quick short circuit
            }

            foreach (var hears in _BotConfig.Hears.Where(s => s.HandlerType == HandlerType.SlashCommand))
            {
                if (incomingMessageText.StartsWith(hears.HandlerDef.CommandConfig.SlashCommand, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private static Message GetUnknownCommandMessage(string incomingMessageText)
        {
            return new Message {Text = $"You sent '{incomingMessageText}' which I do not understand"};
        }

        private bool IsThisTheBotDmChannel(string channelId)
        {
            var imList = new MethodExecutor().ExecImList(new ImListRequestParams());

            if (imList.Ims.Any(i => i.Id == channelId))
            {
                return true;
            }
            return false;
        }
    }
}