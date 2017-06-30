// Decompiled with JetBrains decompiler
// Type: ConfigurationBuilder
// Assembly: Microsoft.Extensions.Configuration, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 01735326-C363-4F22-987D-01A0C9D21466
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\1.1.0\lib\netstandard1.1\dll

namespace StingyBot.Configuration.MsOverride
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Used to build key/value based configuration settings for use in an application.
    /// </summary>
    public class ConfigurationBuilder : IConfigurationBuilder
    {
        private readonly IList<IConfigurationSource> _sources = (IList<IConfigurationSource>)new List<IConfigurationSource>();

        /// <summary>
        /// Gets a key/value collection that can be used to share data between the <see cref="T:IConfigurationBuilder" />
        /// and the registered <see cref="T:IConfigurationProvider" />s.
        /// </summary>
        public Dictionary<string, object> Properties { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Returns the sources used to obtain configuation values.
        /// </summary>
        public IEnumerable<IConfigurationSource> Sources
        {
            get
            {
                return (IEnumerable<IConfigurationSource>)this._sources;
            }
        }

        /// <summary>Adds a new configuration source.</summary>
        /// <param name="source">The configuration source to add.</param>
        /// <returns>The same <see cref="T:IConfigurationBuilder" />.</returns>
        public IConfigurationBuilder Add(IConfigurationSource source)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            this._sources.Add(source);
            return (IConfigurationBuilder)this;
        }

        /// <summary>
        /// Builds an <see cref="T:IConfiguration" /> with keys and values from the set of providers registered in
        /// <see cref="P:StingyBot.Configuration.MsOverride.ConfigurationBuilder.Sources" />.
        /// </summary>
        /// <returns>An <see cref="T:IConfigurationRoot" /> with keys and values from the registered providers.</returns>
        public IConfigurationRoot Build()
        {
            List<IConfigurationProvider> configurationProviderList = new List<IConfigurationProvider>();
            foreach (IConfigurationSource source in (IEnumerable<IConfigurationSource>)this._sources)
            {
                IConfigurationProvider configurationProvider = source.Build((IConfigurationBuilder)this);
                configurationProviderList.Add(configurationProvider);
            }
            return (IConfigurationRoot)new ConfigurationRoot((IList<IConfigurationProvider>)configurationProviderList);
        }
    }
}
