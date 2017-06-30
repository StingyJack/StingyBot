// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.Memory.MemoryConfigurationProvider
// Assembly: Microsoft.Extensions.Configuration, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 01735326-C363-4F22-987D-01A0C9D21466
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Configuration.1.1.0\lib\netstandard1.1\Microsoft.Extensions.Configuration.dll

namespace StingyBot.Configuration.MsOverride.Memory
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
  /// In-memory implementation of <see cref="T:Microsoft.Extensions.Configuration.IConfigurationProvider" />
  /// </summary>
  public class MemoryConfigurationProvider : ConfigurationProvider, IEnumerable<KeyValuePair<string, string>>, IEnumerable
  {
    private readonly MemoryConfigurationSource _source;

    /// <summary>Initialize a new instance from the source.</summary>
    /// <param name="source">The source settings.</param>
    public MemoryConfigurationProvider(MemoryConfigurationSource source)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      this._source = source;
      if (this._source.InitialData == null)
        return;
      foreach (KeyValuePair<string, string> keyValuePair in this._source.InitialData)
        this.Data.Add(keyValuePair.Key, keyValuePair.Value);
    }

    /// <summary>Add a new key and value pair.</summary>
    /// <param name="key">The configuration key.</param>
    /// <param name="value">The configuration value.</param>
    public void Add(string key, string value)
    {
      this.Data.Add(key, value);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the collection.</returns>
    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
      return this.Data.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }
  }
}
