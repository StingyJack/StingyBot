// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.MemoryConfigurationBuilderExtensions
// Assembly: Microsoft.Extensions.Configuration, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 01735326-C363-4F22-987D-01A0C9D21466
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Configuration.1.1.0\lib\netstandard1.1\Microsoft.Extensions.Configuration.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;
    using System.Collections.Generic;
    using Memory;

    /// <summary>
  /// IConfigurationBuilder extension methods for the MemoryConfigurationProvider.
  /// </summary>
  public static class MemoryConfigurationBuilderExtensions
  {
    /// <summary>
    /// Adds the memory configuration provider to <paramref name="configurationBuilder" />.
    /// </summary>
    /// <param name="configurationBuilder">The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" /> to add to.</param>
    /// <returns>The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" />.</returns>
    public static IConfigurationBuilder AddInMemoryCollection(this IConfigurationBuilder configurationBuilder)
    {
      if (configurationBuilder == null)
        throw new ArgumentNullException("configurationBuilder");
      configurationBuilder.Add((IConfigurationSource) new MemoryConfigurationSource());
      return configurationBuilder;
    }

    /// <summary>
    /// Adds the memory configuration provider to <paramref name="configurationBuilder" />.
    /// </summary>
    /// <param name="configurationBuilder">The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" /> to add to.</param>
    /// <param name="initialData">The data to add to memory configuration provider.</param>
    /// <returns>The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" />.</returns>
    public static IConfigurationBuilder AddInMemoryCollection(this IConfigurationBuilder configurationBuilder, IEnumerable<KeyValuePair<string, string>> initialData)
    {
      if (configurationBuilder == null)
        throw new ArgumentNullException("configurationBuilder");
      configurationBuilder.Add((IConfigurationSource) new MemoryConfigurationSource()
      {
        InitialData = initialData
      });
      return configurationBuilder;
    }
  }
}
