// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.FileConfigurationSource
// Assembly: Microsoft.Extensions.Configuration.FileExtensions, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 818349F0-7064-4187-BABE-750AE8E17D6B
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Configuration.FileExtensions.1.1.0\lib\net451\Microsoft.Extensions.Configuration.FileExtensions.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;
    using System.IO;

    /// <summary>
  /// Represents a base class for file based <see cref="T:Microsoft.Extensions.Configuration.IConfigurationSource" />.
  /// </summary>
  public abstract class FileConfigurationSource : IConfigurationSource
  {
    /// <summary>
    /// Number of milliseconds that reload will wait before calling Load.  This helps
    /// avoid triggering reload before a file is completely written. Default is 250.
    /// </summary>
    public int ReloadDelay { get; set; } = 250;

    /// <summary>Used to access the contents of the file.</summary>
    public IFileProvider FileProvider { get; set; }

    /// <summary>The path to the file.</summary>
    public string Path { get; set; }

    /// <summary>Determines if loading the file is optional.</summary>
    public bool Optional { get; set; }

    /// <summary>
    /// Determines whether the source will be loaded if the underlying file changes.
    /// </summary>
    public bool ReloadOnChange { get; set; }

    /// <summary>
    /// Will be called if an uncaught exception occurs in FileConfigurationProvider.Load.
    /// </summary>
    public Action<FileLoadExceptionContext> OnLoadException { get; set; }

    /// <summary>
    /// Builds the <see cref="T:Microsoft.Extensions.Configuration.IConfigurationProvider" /> for this source.
    /// </summary>
    /// <param name="builder">The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" />.</param>
    /// <returns>A <see cref="T:Microsoft.Extensions.Configuration.IConfigurationProvider" /></returns>
    public abstract IConfigurationProvider Build(IConfigurationBuilder builder);

    /// <summary>
    /// Called to use any default settings on the builder like the FileProvider or FileLoadExceptionHandler.
    /// </summary>
    /// <param name="builder">The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" />.</param>
    public void EnsureDefaults(IConfigurationBuilder builder)
    {
      this.FileProvider = this.FileProvider ?? builder.GetFileProvider();
      this.OnLoadException = this.OnLoadException ?? builder.GetFileLoadExceptionHandler();
    }

    /// <summary>
    /// If no file provider has been set, for absolute Path, this will creates a physical file provider
    /// for the nearest existing directory.
    /// </summary>
    public void ResolveFileProvider()
    {
      if (this.FileProvider != null || string.IsNullOrEmpty(this.Path) || !System.IO.Path.IsPathRooted(this.Path))
        return;
      string directoryName = System.IO.Path.GetDirectoryName(this.Path);
      string path2 = System.IO.Path.GetFileName(this.Path);
      for (; !string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName); directoryName = System.IO.Path.GetDirectoryName(directoryName))
        path2 = System.IO.Path.Combine(System.IO.Path.GetFileName(directoryName), path2);
      if (!Directory.Exists(directoryName))
        return;
      this.FileProvider = (IFileProvider) new PhysicalFileProvider(directoryName);
      this.Path = path2;
    }
  }
}
