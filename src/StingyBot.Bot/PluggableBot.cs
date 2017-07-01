namespace StingyBot.Bot
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Common;
    using Common.Configuration;
    using Common.HandlerInterfaces;
    using Common.Models;
    using Common.Nlp;
    using Common.SlashCommand;
    using log4net;
    using Microsoft.Extensions.Configuration;
    using SlackConnector;
    using SlackConnector.Models;

    public class PluggableBot : SlackConnectedBase, IPluggableBot
    {
        #region "fields and consts"

        private bool _shouldBeRunning;
        protected ISlackConnection _BotListenAndSendSlackConnection;
        protected Dictionary<string, IConfigurationRoot> _Configurations;
        protected IncomingMessagesRouter _IncomingMessagesRouter;
        protected BotConfig _BotConfig;
        protected List<IBackgroundTaskHandler> _BackgroundTaskHandlers = new List<IBackgroundTaskHandler>();

        #endregion //#region "fields and consts"

        #region "configuration"

        /// <summary>
        ///     This will configure the bot using the default configuration method (botconfig.json file)
        /// </summary>
        /// <inheritdoc />
        public BotConfig Configure(ILog itsBigItsHeavyItsWood)
        {
            LogInfo("Loading configuration from disk...");
            ReinitializeLogger(itsBigItsHeavyItsWood);

            var botConfigBuilder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("botConfig.json")
                .AddJsonFile("botConfig.json.secret", true);

            var botConfigurationRoot = botConfigBuilder.Build();
            SetBotConfig(botConfigurationRoot);

            var configs = new Dictionary<string, IConfigurationRoot>
            {
                {nameof(BotConfig), botConfigurationRoot}
            };

            foreach (var hear in _BotConfig.Hears.Where(h => h.HandlerDef != null
                                                             && h.HandlerDef.ConfigFiles != null
                                                             && h.HandlerDef.ConfigFiles.Length > 0))
            {
                var handler = hear.HandlerDef;
                var settingsBuilder = new ConfigurationBuilder()
                    .SetBasePath(Environment.CurrentDirectory);

                foreach (var configFile in handler.ConfigFiles)
                {
                    // a declared config file must be present, but secret are always optional
                    settingsBuilder.AddJsonFile(configFile,
                        configFile.EndsWith(".secret", StringComparison.OrdinalIgnoreCase),
                        true);
                }

                var builtConfig = settingsBuilder.Build();
                configs.Add(hear.Name, builtConfig);
            }

            foreach (var backgroundTask in _BotConfig.BackgroundTaskHandlerDefs.Where(h => h.ConfigFiles != null
                                                                                           && h.ConfigFiles.Length > 0))
            {
                var settingsBuilder = new ConfigurationBuilder()
                    .SetBasePath(Environment.CurrentDirectory);

                foreach (var configFile in backgroundTask.ConfigFiles)
                {
                    // a declared config file must be present, but secret are always optional
                    settingsBuilder.AddJsonFile(configFile,
                        configFile.EndsWith(".secret", StringComparison.OrdinalIgnoreCase),
                        true);
                }

                var builtConfig = settingsBuilder.Build();
                configs.Add(backgroundTask.HandlerFullTypeName, builtConfig);
            }

            return Configure(itsBigItsHeavyItsWood, configs);
        }

        /// <inheritdoc />
        public BotConfig Configure(ILog itsBigItsHeavyItsWood, Dictionary<string, IConfigurationRoot> configs)
        {
            LogInfo("Configuring bot and handlers...");
            if (configs == null || configs.ContainsKey(nameof(BotConfig)) == false)
            {
                throw new ArgumentException("BotConfig must be present");
            }

            ReinitializeLogger(itsBigItsHeavyItsWood);
            _Configurations = configs;

            SetBotConfig(configs[nameof(BotConfig)]);
            SetMessageHandlers();
            SetSlashCommandListeners();
            SetLexAndParseEngines();
            SetBackgroundTaskHandlers();
            LogInfo("Configuration complete!");

            return _BotConfig.CloneByJson();
        }

        private void SetSlashCommandListeners()
        {
            if (_BotConfig.Hears.Any(h => h.HandlerType == HandlerType.SlashCommand) == false)
            {
                LogInfo("No slash commands to listen for");
                //external web hook access not defined, and web hooks not are needed.
                //enable this by defining a value of "true"
                if (_BotConfig.RequiresExternalWebHookAccess.HasValue == false)
                {
                    _BotConfig.RequiresExternalWebHookAccess = false;
                }
                return;
            }

            LogInfo("Configuring slash command handling");
            SlashCommandManagerProvider.SetInstance(new SlashCommandManager(_BotConfig));

            //external web hook access not defined, but web hooks are needed.
            //suppress this by defining a value of "false"
            if (_BotConfig.RequiresExternalWebHookAccess.HasValue == false)
            {
                _BotConfig.RequiresExternalWebHookAccess = true;
            }
        }

        protected virtual void SetBotConfig(IConfigurationRoot botConfigurationRoot)
        {
            if (_BotConfig == null)
            {
                _BotConfig = new BotConfig();
                botConfigurationRoot.Bind(_BotConfig);
            }

            for (var i = _BotConfig.Hears.Count - 1; i > 0; i--)
            {
                var hear = _BotConfig.Hears[i];
                if (hear.Enabled == false)
                {
                    _BotConfig.Hears.RemoveAt(i);
                }
            }
        }

        protected virtual void SetMessageHandlers()
        {
            var asm = Assembly.GetEntryAssembly();
            LogInfo($"Current host is {asm.FullName}, assembly load path is {Environment.CurrentDirectory}");

            foreach (var hear in _BotConfig.Hears.Where(h => h.HandlerDef != null))
            {
                var messageHandlerDef = hear.HandlerDef;

                try
                {
                    var type = Type.GetType(messageHandlerDef.HandlerFullTypeName, true, true);
                    if (type == null)
                    {
                        throw new InvalidOperationException($"Message handler {messageHandlerDef.HandlerFullTypeName} could not be located");
                    }

                    var instance = Activator.CreateInstance(type);
                    if (instance == null)
                    {
                        throw new InvalidOperationException($"Message handler {messageHandlerDef.HandlerFullTypeName} could not be created");
                    }

                    var userInputHandler = instance as IUserActionHandler;
                    if (userInputHandler == null)
                    {
                        throw new InvalidOperationException(
                            $"Handler {messageHandlerDef.HandlerFullTypeName} does not implement {nameof(IUserActionHandler)}");
                    }

                    IConfigurationRoot config = null;

                    if (_Configurations.ContainsKey(hear.Name))
                    {
                        config = _Configurations[hear.Name];
                    }

                    if (hear.HandlerType == HandlerType.MessageHandler)
                    {
                        var msgHandler = instance as IMessageHandler;
                        if (msgHandler == null)
                        {
                            throw new InvalidOperationException(
                                $"Message Handler {messageHandlerDef.HandlerFullTypeName} does not implement {nameof(IMessageHandler)}");
                        }
                        msgHandler.Configure(config);
                        hear.ConfiguredMessageHandler = msgHandler;
                    }
                    else if (hear.HandlerType == HandlerType.SlashCommand)
                    {
                        var cmdHandler = instance as ICommandHandler;
                        if (cmdHandler == null)
                        {
                            throw new InvalidOperationException(
                                $"Command Handler {messageHandlerDef.HandlerFullTypeName} does not implement {nameof(ICommandHandler)}");
                        }
                        cmdHandler.Configure(config, _BotConfig.SlackConfig.ApiKey);
                        hear.ConfiguredCommandHandler = cmdHandler;
                    }
                    else if (hear.HandlerType == HandlerType.StaticResponse)
                    {
                        var stHandler = instance as IStaticTextResponseHandler;
                        if (stHandler == null)
                        {
                            throw new InvalidOperationException(
                                $"Command Handler {messageHandlerDef.HandlerFullTypeName} does not implement {nameof(ICommandHandler)}");
                        }
                        stHandler.Configure(hear.HandlerDef.MessageFile);
                        hear.ConfiguredStaticTextResponseHandler = stHandler;
                    }
                    else
                    {
                        throw new InvalidOperationException(
                            $"Command Handler {messageHandlerDef.HandlerFullTypeName} is set for an invalid response type");
                    }

                    LogInfo($"Handler {userInputHandler.GetType().FullName} created and added to the collection");
                }
                catch (Exception ex)
                {
                    LogErr($"Failed to create message handler {messageHandlerDef.HandlerFullTypeName} - {ex}");
                    throw;
                }
            }
        }

        private void SetBackgroundTaskHandlers()
        {
            foreach (var handlerDef in _BotConfig.BackgroundTaskHandlerDefs)
            {
                var type = Type.GetType(handlerDef.HandlerFullTypeName, true, true);
                if (type == null)
                {
                    throw new InvalidOperationException($"Background task handler {handlerDef.HandlerFullTypeName} could not be located");
                }

                var instance = Activator.CreateInstance(type);
                if (instance == null)
                {
                    throw new InvalidOperationException($"Background task handler {handlerDef.HandlerFullTypeName} could not be created");
                }

                var backgroundTask = instance as IBackgroundTaskHandler;
                if (backgroundTask == null)
                {
                    throw new InvalidOperationException($"Background task handler {handlerDef.HandlerFullTypeName} is not {nameof(IBackgroundTaskHandler)}");
                }

                if (_Configurations.ContainsKey(handlerDef.HandlerFullTypeName))
                {
                    var config = _Configurations[handlerDef.HandlerFullTypeName];
                    backgroundTask.Configure(config);
                }
                _BackgroundTaskHandlers.Add(backgroundTask);
            }
        }


        private void SetLexAndParseEngines()
        {
            foreach (var hear in _BotConfig.Hears.Where(h => h.HasSemanticTokens == true))
            {
                hear.LexAndParseEngine = new LexAndParseEngine(hear.SemanticReplacementTokens);
            }
        }

        #endregion //#region "configuration"

        #region "startup and events"

        public async Task StartupAsync()
        {
            SlackConnector.LoggingLevel = ConsoleLoggingLevel.None; //was All
            LogInfo("Starting bot...");
            _shouldBeRunning = true;

            await ConnectToSlack();
            _IncomingMessagesRouter = new IncomingMessagesRouter(_BotListenAndSendSlackConnection.Self, _BotListenAndSendSlackConnection.Team, _BotConfig);

            var scmp = SlashCommandManagerProvider.GetInstance();
            if (scmp != null)
            {
                LogInfo("Starting slash command handler");
                scmp.Startup();
            }
            else
            {
                LogInfo("No slash commands to start the handler for");
            }

            if (_BackgroundTaskHandlers.Count > 0)
            {
                LogInfo("Starting background task handlers");
                foreach (var bt in _BackgroundTaskHandlers) { bt.Start(); }
            }

            LogInfo("Startup complete!");
        }

        private async Task ConnectToSlack()
        {
            if (_BotListenAndSendSlackConnection != null)
            {
                try
                {
                    _BotListenAndSendSlackConnection.OnDisconnect -= OnDisconnect;
                    _BotListenAndSendSlackConnection.OnMessageReceived -= OnMessageReceived;
                }
                catch (Exception)
                {
                    //nothing to detach?
                }
            }

            _BotListenAndSendSlackConnection = await GetNewSlackConnectionAsync(_BotConfig?.SlackConfig?.ApiKey, true);
            if (_BotListenAndSendSlackConnection.IsConnected)
            {
                LogInfo("Connected to slack");
                _BotListenAndSendSlackConnection.OnMessageReceived += OnMessageReceived;
                _BotListenAndSendSlackConnection.OnDisconnect += OnDisconnect;
            }
            else
            {
                LogFatal("Failed to connect to Slack");
                throw new InvalidOperationException("Slack connection failure");
            }
        }

        public void Shutdown()
        {
            LogInfo("Shutting down...");
            _shouldBeRunning = false;
            _BotListenAndSendSlackConnection.OnDisconnect -= OnDisconnect;
            _BotListenAndSendSlackConnection.OnMessageReceived -= OnMessageReceived;
            _BotListenAndSendSlackConnection.Disconnect();

            SlashCommandManagerProvider.GetInstance()?.Shutdown();

            foreach (var hear in _BotConfig.Hears)
            {
                hear.ConfiguredMessageHandler?.Dispose();
                hear.ConfiguredCommandHandler?.Dispose();
                hear.ConfiguredStaticTextResponseHandler?.Dispose();
            }

            foreach (var bt in _BackgroundTaskHandlers) { bt.Stop(); }
            LogInfo("Shutdown complete...");
        }

        private async void OnDisconnect()
        {
            if (_shouldBeRunning)
            {
                LogWarn($"Diconnected but should be running. Reconnecting");
                await ConnectToSlack();
                LogInfo("Reconnected.");
                return;
            }
            LogInfo("Disconnected");
        }

        private Task OnMessageReceived(SlackMessage message)
        {
            LogDebug("Message Received");

            var msg = new Message {Text = message.Text, ChatHub = message.ChatHub};

            var response = _IncomingMessagesRouter.ProcessIncomingMessageAsync(msg).GetAwaiter().GetResult();
            if (response == null)
            {
                return null;
            }

            _BotListenAndSendSlackConnection.Say(new BotMessage
            {
                Text = response.Text,
                Attachments = response.Attachments,
                ChatHub = message.ChatHub
            });
            return Task.FromResult(0);
        }

        #endregion //#region "startup and events"
    }
}