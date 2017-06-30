// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.ConfigurationRoot
// Assembly: Microsoft.Extensions.Configuration, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 01735326-C363-4F22-987D-01A0C9D21466
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Configuration.1.1.0\lib\netstandard1.1\Microsoft.Extensions.Configuration.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Primitives;

    /// <summary>The root node for a configuration.</summary>
  public class ConfigurationRoot : IConfigurationRoot, IConfiguration
  {
    private ConfigurationReloadToken _changeToken = new ConfigurationReloadToken();
    private IList<IConfigurationProvider> _providers;

    /// <summary>
    /// Gets or sets the value corresponding to a configuration key.
    /// </summary>
    /// <param name="key">The configuration key.</param>
    /// <returns>The configuration value.</returns>
    public string this[string key]
    {
      get
      {
        foreach (IConfigurationProvider configurationProvider in this._providers.Reverse<IConfigurationProvider>())
        {
          string str;
          if (configurationProvider.TryGet(key, out str))
            return str;
        }
        return (string) null;
      }
      set
      {
        if (!this._providers.Any<IConfigurationProvider>())
          throw new InvalidOperationException(Resources.Error_NoSources);
        foreach (IConfigurationProvider provider in (IEnumerable<IConfigurationProvider>) this._providers)
          provider.Set(key, value);
      }
    }

    /// <summary>
    /// Initializes a Configuration root with a list of providers.
    /// </summary>
    /// <param name="providers">The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationProvider" />s for this configuration.</param>
    public ConfigurationRoot(IList<IConfigurationProvider> providers)
    {
      if (providers == null)
        throw new ArgumentNullException("providers");
      this._providers = providers;
      foreach (IConfigurationProvider provider in (IEnumerable<IConfigurationProvider>) providers)
      {
        IConfigurationProvider p = provider;
        p.Load();
        ChangeToken.OnChange((Func<IChangeToken>) (() => p.GetReloadToken()), (Action) (() => this.RaiseChanged()));
      }
    }

    /// <summary>Gets the immediate children sub-sections.</summary>
    /// <returns></returns>
    public IEnumerable<IConfigurationSection> GetChildren()
    {
      return this.GetChildrenImplementation((string) null);
    }

    internal IEnumerable<IConfigurationSection> GetChildrenImplementation(string path)
    {
      return this._providers.Aggregate<IConfigurationProvider, IEnumerable<string>>(Enumerable.Empty<string>(), (Func<IEnumerable<string>, IConfigurationProvider, IEnumerable<string>>) ((seed, source) => source.GetChildKeys(seed, path))).Distinct<string>().Select<string, IConfigurationSection>((Func<string, IConfigurationSection>) (key =>
      {
        string key1;
        if (path != null)
          key1 = ConfigurationPath.Combine(path, key);
        else
          key1 = key;
        return this.GetSection(key1);
      }));
    }

    /// <summary>
    /// Returns a <see cref="T:Microsoft.Extensions.Primitives.IChangeToken" /> that can be used to observe when this configuration is reloaded.
    /// </summary>
    /// <returns></returns>
    public IChangeToken GetReloadToken()
    {
      return (IChangeToken) this._changeToken;
    }

    /// <summary>
    /// Gets a configuration sub-section with the specified key.
    /// </summary>
    /// <param name="key">The key of the configuration section.</param>
    /// <returns>The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationSection" />.</returns>
    /// <remarks>
    ///     This method will never return <c>null</c>. If no matching sub-section is found with the specified key,
    ///     an empty <see cref="T:Microsoft.Extensions.Configuration.IConfigurationSection" /> will be returned.
    /// </remarks>
    public IConfigurationSection GetSection(string key)
    {
      return (IConfigurationSection) new ConfigurationSection(this, key);
    }

    /// <summary>
    /// Force the configuration values to be reloaded from the underlying sources.
    /// </summary>
    public void Reload()
    {
      foreach (IConfigurationProvider provider in (IEnumerable<IConfigurationProvider>) this._providers)
        provider.Load();
      this.RaiseChanged();
    }

    private void RaiseChanged()
    {
      Interlocked.Exchange<ConfigurationReloadToken>(ref this._changeToken, new ConfigurationReloadToken()).OnReload();
    }
  }
}
