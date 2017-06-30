// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.IConfigurationProvider
// Assembly: Microsoft.Extensions.Configuration.Abstractions, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 5F64D0E4-986F-4F55-A371-10812112614E
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Configuration.Abstractions.1.1.0\lib\netstandard1.0\Microsoft.Extensions.Configuration.Abstractions.dll

namespace StingyBot.Configuration.MsOverride
{
    using System.Collections.Generic;
    using Primitives;

    /// <summary>Provides configuration key/values for an application.</summary>
    public interface IConfigurationProvider
    {
        /// <summary>
        /// Tries to get a configuration value for the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns><c>True</c> if a value for the specified key was found, otherwise <c>false</c>.</returns>
        bool TryGet(string key, out string value);

        /// <summary>Sets a configuration value for the specified key.</summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void Set(string key, string value);

        /// <summary>
        /// Returns a change token if this provider supports change tracking, null otherwise.
        /// </summary>
        /// <returns></returns>
        IChangeToken GetReloadToken();

        /// <summary>
        /// Loads configuration values from the source represented by this <see cref="T:StingyBot.Configuration.MsOverride.IConfigurationProvider" />.
        /// </summary>
        void Load();

        /// <summary>
        /// Returns the immediate descendant configuration keys for a given parent path based on this
        /// <see cref="T:StingyBot.Configuration.MsOverride.IConfigurationProvider" />'s data and the set of keys returned by all the preceding
        /// <see cref="T:StingyBot.Configuration.MsOverride.IConfigurationProvider" />s.
        /// </summary>
        /// <param name="earlierKeys">The child keys returned by the preceding providers for the same parent path.</param>
        /// <param name="parentPath">The parent path.</param>
        /// <returns>The child keys.</returns>
        IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string parentPath);
    }
}
