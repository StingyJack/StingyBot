namespace StingyBot.Common.Nlp
{
    /// <summary>
    ///     A value extracted from the lexical replacement operation
    /// </summary>
    public class LexedValue
    {
        /// <summary>
        ///     The items index in the sentence "collection"
        /// </summary>
        /// <remarks>
        ///     A OrderedDictionary[T] would have been nice
        /// </remarks>
        public int  IndexInCollection { get; set; }

        /// <summary>
        ///     The index from the original string
        /// </summary>
        public int IndexInOriginalString { get; set; }
        
        /// <summary>
        ///     The token matched to this entry
        /// </summary>
        public SemanticReplacementToken SemanticReplacementToken { get; set; }

        /// <summary>
        ///     The value extracted
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        ///     Duh
        /// </summary>
        public int ValueLength => Value == null ? -1 : Value.Length;

        /// <summary>
        ///     If it has a token
        /// </summary>
        public bool HasToken => SemanticReplacementToken == null == false;
    }
}
