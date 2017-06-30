// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.IConfigurationBuilder
// Assembly: Microsoft.Extensions.Configuration.Abstractions, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 5F64D0E4-986F-4F55-A371-10812112614E
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Configuration.Abstractions.1.1.0\lib\netstandard1.0\Microsoft.Extensions.Configuration.Abstractions.dll

namespace StingyBot.Configuration.MsOverride
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a type used to build application configuration.
    /// </summary>
    public interface IConfigurationBuilder
    {
        /// <summary>
        /// Gets a key/value collection that can be used to share data between the <see cref="T:StingyBot.Configuration.MsOverride.IConfigurationBuilder" />
        /// and the registered <see cref="T:Microsoft.Extensions.Configuration.IConfigurationSource" />s.
        /// </summary>
        Dictionary<string, object> Properties { get; }

        /// <summary>Gets the sources used to obtain configuation values</summary>
        IEnumerable<IConfigurationSource> Sources { get; }

        /// <summary>Adds a new configuration source.</summary>
        /// <param name="source">The configuration source to add.</param>
        /// <returns>The same <see cref="T:StingyBot.Configuration.MsOverride.IConfigurationBuilder" />.</returns>
        IConfigurationBuilder Add(IConfigurationSource source);

        /// <summary>
        /// Builds an <see cref="T:Microsoft.Extensions.Configuration.IConfiguration" /> with keys and values from the set of sources registered in
        /// <see cref="P:StingyBot.Configuration.MsOverride.IConfigurationBuilder.Sources" />.
        /// </summary>
        /// <returns>An <see cref="T:Microsoft.Extensions.Configuration.IConfigurationRoot" /> with keys and values from the registered sources.</returns>
        IConfigurationRoot Build();
    }
}
