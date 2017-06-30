namespace StingyBot.Common.Configuration
{
    using HandlerInterfaces;

    public enum HandlerType
    {
        /// <summary>
        ///     No handler type defined
        /// </summary>
        Unknown,
        /// <summary>
        ///     Handles user initiated messages, usually triggered by type <see cref="TriggerType.DirectMention"/>
        ///     Expected to be handled by an implementer of <see cref="IMessageHandler"/>
        /// </summary>
        MessageHandler,
        /// <summary>
        ///     Handles user initiated messages, usually triggered by type <see cref="TriggerType.DirectMention"/>
        ///     Expects to return a static response.
        /// </summary>
        StaticResponse,
        /// <summary>
        ///     Handles user initiated slash comands, usually triggered by type <see cref="TriggerType.SlashCommand"/>
        ///     Expected to be handled by an implementer of <see cref="ICommandHandler"/>
        /// </summary>
        SlashCommand
    }

    public enum TriggerType
    {
        /// <summary>
        ///     Not defined
        /// </summary>
        Unknown,
        /// <summary>
        ///     Directly mentioning the bot name in the message.
        /// </summary>
        /// <example>
        ///      "@botname do something"
        /// </example>
        DirectMention,
      
        /// <summary>
        ///     Trigger
        /// </summary>
		SlashCommand, 

        // <summary>
        //     Ambient reference match to a phrase.
        // </summary>
        // <example>
        //     if the defined phrase is "triggered", and the user says "did you hear president orange? he
        // got me triggered!", the handler is actioned
        // </example>
        //Ambient,

        /// <summary>
        ///     Requires no user interaction, runs in the background to do cleanup actions, lazy updates, 
        /// etc.
        /// </summary>
        BackgroundTask

    }

}