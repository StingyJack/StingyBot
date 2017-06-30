// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.FileConfigurationExtensions
// Assembly: Microsoft.Extensions.Configuration.FileExtensions, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 818349F0-7064-4187-BABE-750AE8E17D6B
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Configuration.FileExtensions.1.1.0\lib\net451\Microsoft.Extensions.Configuration.FileExtensions.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;

    /// <summary>
  /// Extension methods for <see cref="T:Microsoft.Extensions.Configuration.FileConfigurationProvider" />.
  /// </summary>
  public static class FileConfigurationExtensions
  {
    private static string FileProviderKey = "FileProvider";
    private static string FileLoadExceptionHandlerKey = "FileLoadExceptionHandler";

    /// <summary>
    /// Sets the default <see cref="T:Microsoft.Extensions.FileProviders.IFileProvider" /> to be used for file-based providers.
    /// </summary>
    /// <param name="builder">The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" /> to add to.</param>
    /// <param name="fileProvider">The default file provider instance.</param>
    /// <returns>The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" />.</returns>
    public static IConfigurationBuilder SetFileProvider(this IConfigurationBuilder builder, IFileProvider fileProvider)
    {
      if (builder == null)
        throw new ArgumentNullException("builder");
      if (fileProvider == null)
        throw new ArgumentNullException("fileProvider");
      builder.Properties[FileConfigurationExtensions.FileProviderKey] = (object) fileProvider;
      return builder;
    }

    /// <summary>
    /// Gets the default <see cref="T:Microsoft.Extensions.FileProviders.IFileProvider" /> to be used for file-based providers.
    /// </summary>
    /// <param name="builder">The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" />.</param>
    /// <returns>The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" />.</returns>
    public static IFileProvider GetFileProvider(this IConfigurationBuilder builder)
    {
      if (builder == null)
        throw new ArgumentNullException("builder");
      object obj;
      if (builder.Properties.TryGetValue(FileConfigurationExtensions.FileProviderKey, out obj))
        return builder.Properties[FileConfigurationExtensions.FileProviderKey] as IFileProvider;
      return (IFileProvider) new PhysicalFileProvider(AppDomain.CurrentDomain.GetData("APP_CONTEXT_BASE_DIRECTORY") as string ?? AppDomain.CurrentDomain.BaseDirectory ?? string.Empty);
    }

    /// <summary>
    /// Sets the FileProvider for file-based providers to a PhysicalFileProvider with the base path.
    /// </summary>
    /// <param name="builder">The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" /> to add to.</param>
    /// <param name="basePath">The absolute path of file-based providers.</param>
    /// <returns>The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" />.</returns>
    public static IConfigurationBuilder SetBasePath(this IConfigurationBuilder builder, string basePath)
    {
      if (builder == null)
        throw new ArgumentNullException("builder");
      if (basePath == null)
        throw new ArgumentNullException("basePath");
      return builder.SetFileProvider((IFileProvider) new PhysicalFileProvider(basePath));
    }

    /// <summary>
    /// Sets a default action to be invoked for file-based providers when an error occurs.
    /// </summary>
    /// <param name="builder">The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" /> to add to.</param>
    /// <param name="handler">The Action to be invoked on a file load exception.</param>
    /// <returns>The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" />.</returns>
    public static IConfigurationBuilder SetFileLoadExceptionHandler(this IConfigurationBuilder builder, Action<FileLoadExceptionContext> handler)
    {
      if (builder == null)
        throw new ArgumentNullException("builder");
      builder.Properties[FileConfigurationExtensions.FileLoadExceptionHandlerKey] = (object) handler;
      return builder;
    }

    /// <summary>
    /// Gets the default <see cref="T:Microsoft.Extensions.FileProviders.IFileProvider" /> to be used for file-based providers.
    /// </summary>
    /// <param name="builder">The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" />.</param>
    /// <returns>The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" />.</returns>
    public static Action<FileLoadExceptionContext> GetFileLoadExceptionHandler(this IConfigurationBuilder builder)
    {
      if (builder == null)
        throw new ArgumentNullException("builder");
      object obj;
      if (builder.Properties.TryGetValue(FileConfigurationExtensions.FileLoadExceptionHandlerKey, out obj))
        return builder.Properties[FileConfigurationExtensions.FileLoadExceptionHandlerKey] as Action<FileLoadExceptionContext>;
      return (Action<FileLoadExceptionContext>) null;
    }
  }
}
