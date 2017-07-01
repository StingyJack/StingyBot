namespace StingyBot.Common.SlashCommand
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using Configuration;
    using HandlerInterfaces;
    using SlackApi;

    public class SlashCommandManager : IDisposable
    {
        #region "fields and props"

        /// <summary>
        ///     Keyed by h.HandlerDef.CommandConfig.SlashCommand
        /// </summary>
        private readonly OicDic<CommandConfig> _commandConfigs = new OicDic<CommandConfig>();

        private readonly OicDic<ICommandHandler> _commandHandlers = new OicDic<ICommandHandler>();
        private readonly WebHookHost _webHookHost;

        #endregion //#region "fields and props"

        #region "ctors"

        public SlashCommandManager(BotConfig botConfig)
        {
            _webHookHost = new WebHookHost(botConfig.WebHookApiName,
                botConfig.WebHookRouteTemplate, botConfig.BaseWebHookAddress);

            foreach (var h in botConfig.Hears.Where(h => h.HandlerType == HandlerType.SlashCommand))
            {
                _commandConfigs.Add(h.HandlerDef.CommandConfig.SlashCommand, h.HandlerDef.CommandConfig);
                _commandHandlers.Add(h.HandlerDef.CommandConfig.SlashCommand, h.ConfiguredCommandHandler);
            }
        }

        #endregion //#region "ctors"

        #region "public members"

        public void Startup()
        {
            _webHookHost.Start();
        }

        public void Shutdown()
        {
            _webHookHost.Stop();
        }

        public async Task<HttpResponseMessage> ProcessCommandAsync(HttpRequestContext requestContext, HttpRequestMessage requestMessage)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException(nameof(requestContext));
            }

            if (requestMessage == null)
            {
                throw new ArgumentNullException(nameof(requestMessage));
            }

            if (requestMessage.Method != HttpMethod.Post)
            {
                return requestMessage.CreateErrorResponse(HttpStatusCode.MethodNotAllowed, "Only POST allowed");
            }

            VerifySecureConnection(requestContext, requestMessage);

            var postMessage = await GetWebHookPostMessageAsync(requestMessage);

            var candidateConfig = GetCandidateConfig(postMessage);

            if (candidateConfig == null)
            {
                return requestMessage.CreateErrorResponse(HttpStatusCode.BadRequest,
                    $"There is no handler available for command {postMessage.Command}");
            }

            if (string.Equals(candidateConfig.VerificationToken,
                    postMessage.Token, StringComparison.OrdinalIgnoreCase) == false)
            {
                return requestMessage.CreateErrorResponse(HttpStatusCode.Unauthorized,
                    $"Presented token does not match. GTFO.");
            }

            var handler = GetCommandHandler(candidateConfig.SlashCommand);

            if (handler == null)
            {
                return requestMessage.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    $"No handler declared for command {candidateConfig.SlashCommand}");
            }

            //TODO: ICommandHandler needs to accept other parameters or MessageContext needs to have more props.

            var message = await handler.PerformUserCommandAsync(new MessageContext());

            return requestMessage.CreateResponse(HttpStatusCode.OK, message);
        }

        private ICommandHandler GetCommandHandler(string slashCommand)
        {
            if (_commandHandlers.ContainsKey(slashCommand))
            {
                return _commandHandlers[slashCommand];
            }
            return null;
        }

        private CommandConfig GetCandidateConfig(PostMessage postMessage)
        {
            if (_commandConfigs.ContainsKey(postMessage.Command))
            {
                return _commandConfigs[postMessage.Command];
            }
            return null;
        }

        #endregion //#region "public members"

        #region "non-public members"

        protected void VerifySecureConnection(HttpRequestContext requestContext, HttpRequestMessage requestMessage)
        {
            //TODO: allow bypass by config setting?

            var isRemoteRequest = requestContext.IsLocal == false;
            var isInsecure = Uri.UriSchemeHttps.Equals(requestMessage.RequestUri.Scheme,
                                 StringComparison.OrdinalIgnoreCase) == false;

            if (isRemoteRequest && isInsecure)
            {
                var msg = requestMessage.CreateErrorResponse(HttpStatusCode.UpgradeRequired, "https only for remote requests");
                throw new HttpResponseException(msg);
            }
        }

        protected void VerifyTokenIsCorrect(PostMessage postMessage)
        {
            //locate the request
        }

        protected async Task<PostMessage> GetWebHookPostMessageAsync(HttpRequestMessage requestMessage)
        {
            var data = await requestMessage.Content.ReadAsFormDataAsync();
            return new PostMessage(data);
        }

        #endregion //#region "non-public members"

        #region "disposable"

        public void Dispose()
        {
            _webHookHost?.Dispose();
        }

        #endregion //#region "disposable"
    }
}