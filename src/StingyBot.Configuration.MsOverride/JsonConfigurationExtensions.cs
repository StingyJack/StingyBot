// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.JsonConfigurationExtensions
// Assembly: Microsoft.Extensions.Configuration.Json, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 0EA8CCDB-7A8B-4677-AAD5-4045FDD46D90
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Configuration.Json.1.1.0\lib\net451\Microsoft.Extensions.Configuration.Json.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;
    using Json;

    /// <summary>
  /// Extension methods for adding <see cref="T:Microsoft.Extensions.Configuration.Json.JsonConfigurationProvider" />.
  /// </summary>
  public static class JsonConfigurationExtensions
  {
    /// <summary>
    /// Adds the JSON configuration provider at <paramref name="path" /> to <paramref name="builder" />.
    /// </summary>
    /// <param name="builder">The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" /> to add to.</param>
    /// <param name="path">Path relative to the base path stored in
    /// <see cref="P:Microsoft.Extensions.Configuration.IConfigurationBuilder.Properties" /> of <paramref name="builder" />.</param>
    /// <returns>The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" />.</returns>
    public static IConfigurationBuilder AddJsonFile(this IConfigurationBuilder builder, string path)
    {
      return builder.AddJsonFile((IFileProvider) null, path, false, false);
    }

    /// <summary>
    /// Adds the JSON configuration provider at <paramref name="path" /> to <paramref name="builder" />.
    /// </summary>
    /// <param name="builder">The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" /> to add to.</param>
    /// <param name="path">Path relative to the base path stored in
    /// <see cref="P:Microsoft.Extensions.Configuration.IConfigurationBuilder.Properties" /> of <paramref name="builder" />.</param>
    /// <param name="optional">Whether the file is optional.</param>
    /// <returns>The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" />.</returns>
    public static IConfigurationBuilder AddJsonFile(this IConfigurationBuilder builder, string path, bool optional)
    {
      return builder.AddJsonFile((IFileProvider) null, path, optional, false);
    }

    /// <summary>
    /// Adds the JSON configuration provider at <paramref name="path" /> to <paramref name="builder" />.
    /// </summary>
    /// <param name="builder">The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" /> to add to.</param>
    /// <param name="path">Path relative to the base path stored in
    /// <see cref="P:Microsoft.Extensions.Configuration.IConfigurationBuilder.Properties" /> of <paramref name="builder" />.</param>
    /// <param name="optional">Whether the file is optional.</param>
    /// <param name="reloadOnChange">Whether the configuration should be reloaded if the file changes.</param>
    /// <returns>The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" />.</returns>
    public static IConfigurationBuilder AddJsonFile(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
    {
      return builder.AddJsonFile((IFileProvider) null, path, optional, reloadOnChange);
    }

    /// <summary>
    /// Adds a JSON configuration source to <paramref name="builder" />.
    /// </summary>
    /// <param name="builder">The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" /> to add to.</param>
    /// <param name="provider">The <see cref="T:Microsoft.Extensions.FileProviders.IFileProvider" /> to use to access the file.</param>
    /// <param name="path">Path relative to the base path stored in
    /// <see cref="P:Microsoft.Extensions.Configuration.IConfigurationBuilder.Properties" /> of <paramref name="builder" />.</param>
    /// <param name="optional">Whether the file is optional.</param>
    /// <param name="reloadOnChange">Whether the configuration should be reloaded if the file changes.</param>
    /// <returns>The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" />.</returns>
    public static IConfigurationBuilder AddJsonFile(this IConfigurationBuilder builder, IFileProvider provider, string path, bool optional, bool reloadOnChange)
    {
      if (builder == null)
        throw new ArgumentNullException("builder");
      if (string.IsNullOrEmpty(path))
        throw new ArgumentException("Resources.Error_InvalidFilePath", "path");
      JsonConfigurationSource configurationSource1 = new JsonConfigurationSource();
      IFileProvider fileProvider = provider;
      configurationSource1.FileProvider = fileProvider;
      string str = path;
      configurationSource1.Path = str;
      int num1 = optional ? 1 : 0;
      configurationSource1.Optional = num1 != 0;
      int num2 = reloadOnChange ? 1 : 0;
      configurationSource1.ReloadOnChange = num2 != 0;
      JsonConfigurationSource configurationSource2 = configurationSource1;
      configurationSource2.ResolveFileProvider();
      builder.Add((IConfigurationSource) configurationSource2);
      return builder;
    }
  }
}
