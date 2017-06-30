// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.ConfigurationSection
// Assembly: Microsoft.Extensions.Configuration, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 01735326-C363-4F22-987D-01A0C9D21466
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Configuration.1.1.0\lib\netstandard1.1\Microsoft.Extensions.Configuration.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;
    using System.Collections.Generic;
    using Primitives;

    /// <summary>
  /// Represents a section of application configuration values.
  /// </summary>
  public class ConfigurationSection : IConfigurationSection, IConfiguration
  {
    private readonly ConfigurationRoot _root;
    private readonly string _path;
    private string _key;

    /// <summary>
    /// Gets the full path to this section from the <see cref="T:Microsoft.Extensions.Configuration.IConfigurationRoot" />.
    /// </summary>
    public string Path
    {
      get
      {
        return this._path;
      }
    }

    /// <summary>Gets the key this section occupies in its parent.</summary>
    public string Key
    {
      get
      {
        if (this._key == null)
          this._key = ConfigurationPath.GetSectionKey(this._path);
        return this._key;
      }
    }

    /// <summary>Gets or sets the section value.</summary>
    public string Value
    {
      get
      {
        return this._root[this.Path];
      }
      set
      {
        this._root[this.Path] = value;
      }
    }

    /// <summary>
    /// Gets or sets the value corresponding to a configuration key.
    /// </summary>
    /// <param name="key">The configuration key.</param>
    /// <returns>The configuration value.</returns>
    public string this[string key]
    {
      get
      {
        return this._root[ConfigurationPath.Combine(this.Path, key)];
      }
      set
      {
        this._root[ConfigurationPath.Combine(this.Path, key)] = value;
      }
    }

    /// <summary>Initializes a new instance.</summary>
    /// <param name="root">The configuration root.</param>
    /// <param name="path">The path to this section.</param>
    public ConfigurationSection(ConfigurationRoot root, string path)
    {
      if (root == null)
        throw new ArgumentNullException("root");
      if (path == null)
        throw new ArgumentNullException("path");
      this._root = root;
      this._path = path;
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
      return this._root.GetSection(ConfigurationPath.Combine(this.Path, key));
    }

    /// <summary>
    /// Gets the immediate descendant configuration sub-sections.
    /// </summary>
    /// <returns>The configuration sub-sections.</returns>
    public IEnumerable<IConfigurationSection> GetChildren()
    {
      return this._root.GetChildrenImplementation(this.Path);
    }

    /// <summary>
    /// Returns a <see cref="T:Microsoft.Extensions.Primitives.IChangeToken" /> that can be used to observe when this configuration is reloaded.
    /// </summary>
    /// <returns></returns>
    public IChangeToken GetReloadToken()
    {
      return this._root.GetReloadToken();
    }
  }
}
