namespace StingyBot.Common.Nlp
{

    public enum LexiTokenMatch
    {
        None = 0,
        Some = 1,
        All = 2
    }

 

/// <summary>
    ///     How strongly was the token mentioned
    /// </summary>
    /// <example>
    ///     "None"
    ///     "_Light_"
    ///     "*Strong*". "STRONG", or "__Strong__" (not sure about these)
    /// </example>>
    public enum Emphasis
    {
        None = 0,
        Light = 1,
        Strong = 2
    }

}