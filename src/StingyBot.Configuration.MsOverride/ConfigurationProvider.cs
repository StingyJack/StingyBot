// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.ConfigurationProvider
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

    /// <summary>
  /// Base helper class for implementing an <see cref="T:Microsoft.Extensions.Configuration.IConfigurationProvider" />
  /// </summary>
  public abstract class ConfigurationProvider : IConfigurationProvider
  {
    private ConfigurationReloadToken _reloadToken = new ConfigurationReloadToken();

    /// <summary>The configuration key value pairs for this provider.</summary>
    protected IDictionary<string, string> Data { get; set; }

    /// <summary>
    /// Initializes a new <see cref="T:Microsoft.Extensions.Configuration.IConfigurationProvider" />
    /// </summary>
    protected ConfigurationProvider()
    {
      this.Data = (IDictionary<string, string>) new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Attempts to find a value with the given key, returns true if one is found, false otherwise.
    /// </summary>
    /// <param name="key">The key to lookup.</param>
    /// <param name="value">The value found at key if one is found.</param>
    /// <returns>True if key has a value, false otherwise.</returns>
    public virtual bool TryGet(string key, out string value)
    {
      return this.Data.TryGetValue(key, out value);
    }

    /// <summary>Sets a value for a given key.</summary>
    /// <param name="key">The configuration key to set.</param>
    /// <param name="value">The value to set.</param>
    public virtual void Set(string key, string value)
    {
      this.Data[key] = value;
    }

    /// <summary>Loads (or reloads) the data for this provider.</summary>
    public virtual void Load()
    {
    }

    /// <summary>Returns the list of keys that this provider has.</summary>
    /// <param name="earlierKeys">The earlier keys that other providers contain.</param>
    /// <param name="parentPath">The path for the parent IConfiguration.</param>
    /// <returns>The list of keys for this provider.</returns>
    public virtual IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string parentPath)
    {
      string prefix = parentPath == null ? string.Empty : parentPath + ConfigurationPath.KeyDelimiter;
      IEnumerable<string> source = this.Data.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (kv => kv.Key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))).Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (kv => ConfigurationProvider.Segment(kv.Key, prefix.Length))).Concat<string>(earlierKeys);
      ConfigurationKeyComparer instance = ConfigurationKeyComparer.Instance;
      return (IEnumerable<string>) source.OrderBy<string, string>((Func<string, string>) (k => k), (IComparer<string>) instance);
    }

    private static string Segment(string key, int prefixLength)
    {
      int num = key.IndexOf(ConfigurationPath.KeyDelimiter, prefixLength, StringComparison.OrdinalIgnoreCase);
      if (num >= 0)
        return key.Substring(prefixLength, num - prefixLength);
      return key.Substring(prefixLength);
    }

    /// <summary>
    /// Returns a <see cref="T:Microsoft.Extensions.Primitives.IChangeToken" /> that can be used to listen when this provider is reloaded.
    /// </summary>
    /// <returns></returns>
    public IChangeToken GetReloadToken()
    {
      return (IChangeToken) this._reloadToken;
    }

    /// <summary>
    /// Triggers the reload change token and creates a new one.
    /// </summary>
    protected void OnReload()
    {
      Interlocked.Exchange<ConfigurationReloadToken>(ref this._reloadToken, new ConfigurationReloadToken()).OnReload();
    }
  }
}
