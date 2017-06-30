// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.Memory.MemoryConfigurationSource
// Assembly: Microsoft.Extensions.Configuration, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 01735326-C363-4F22-987D-01A0C9D21466
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Configuration.1.1.0\lib\netstandard1.1\Microsoft.Extensions.Configuration.dll

namespace StingyBot.Configuration.MsOverride.Memory
{
    using System.Collections.Generic;

    /// <summary>
  /// Represents in-memory data as an <see cref="T:Microsoft.Extensions.Configuration.IConfigurationSource" />.
  /// </summary>
  public class MemoryConfigurationSource : IConfigurationSource
  {
    /// <summary>The initial key value configuration pairs.</summary>
    public IEnumerable<KeyValuePair<string, string>> InitialData { get; set; }

    /// <summary>
    /// Builds the <see cref="T:Microsoft.Extensions.Configuration.Memory.MemoryConfigurationProvider" /> for this source.
    /// </summary>
    /// <param name="builder">The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" />.</param>
    /// <returns>A <see cref="T:Microsoft.Extensions.Configuration.Memory.MemoryConfigurationProvider" /></returns>
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
      return (IConfigurationProvider) new MemoryConfigurationProvider(this);
    }
  }
}
