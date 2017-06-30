// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.FileConfigurationProvider
// Assembly: Microsoft.Extensions.Configuration.FileExtensions, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 818349F0-7064-4187-BABE-750AE8E17D6B
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Configuration.FileExtensions.1.1.0\lib\net451\Microsoft.Extensions.Configuration.FileExtensions.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading;
    using Primitives;

    /// <summary>
  /// Base class for file based <see cref="T:Microsoft.Extensions.Configuration.ConfigurationProvider" />.
  /// </summary>
  public abstract class FileConfigurationProvider : ConfigurationProvider
  {
    /// <summary>The source settings for this provider.</summary>
    public FileConfigurationSource Source { get; }

    /// <summary>Initializes a new instance with the specified source.</summary>
    /// <param name="source">The source settings.</param>
    public FileConfigurationProvider(FileConfigurationSource source)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      this.Source = source;
      if (!this.Source.ReloadOnChange || this.Source.FileProvider == null)
        return;
      ChangeToken.OnChange((Func<IChangeToken>) (() => this.Source.FileProvider.Watch(this.Source.Path)), (Action) (() =>
      {
        Thread.Sleep(this.Source.ReloadDelay);
        this.Load(true);
      }));
    }

    private void Load(bool reload)
    {
      IFileProvider fileProvider = this.Source.FileProvider;
      IFileInfo fileInfo1;
      if (fileProvider == null)
      {
        fileInfo1 = (IFileInfo) null;
      }
      else
      {
        string path = this.Source.Path;
        fileInfo1 = fileProvider.GetFileInfo(path);
      }
      IFileInfo fileInfo2 = fileInfo1;
      if (fileInfo2 == null || !fileInfo2.Exists)
      {
        if (this.Source.Optional | reload)
        {
          this.Data = (IDictionary<string, string>) new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        }
        else
        {
          StringBuilder stringBuilder = new StringBuilder(string.Format("The configuration file '{0}' was not found and is not optional.", (object) this.Source.Path));
          if (!string.IsNullOrEmpty(fileInfo2 != null ? fileInfo2.PhysicalPath : (string) null))
            stringBuilder.Append(string.Format(" The physical path is '{0}'.", (object) fileInfo2.PhysicalPath));
          throw new FileNotFoundException(stringBuilder.ToString());
        }
      }
      else
      {
        if (reload)
          this.Data = (IDictionary<string, string>) new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        using (Stream readStream = fileInfo2.CreateReadStream())
        {
          try
          {
            this.Load(readStream);
          }
          catch (Exception ex)
          {
            bool flag = false;
            if (this.Source.OnLoadException != null)
            {
              FileLoadExceptionContext exceptionContext = new FileLoadExceptionContext() { Provider = this, Exception = ex };
              this.Source.OnLoadException(exceptionContext);
              flag = exceptionContext.Ignore;
            }
            if (!flag)
              throw ex;
          }
        }
      }
      this.OnReload();
    }

    /// <summary>
    /// Loads the contents of the file at <see cref="T:System.IO.Path" />.
    /// </summary>
    /// <exception cref="T:System.IO.FileNotFoundException">If Optional is <c>false</c> on the source and a
    /// file does not exist at specified Path.</exception>
    public override void Load()
    {
      this.Load(false);
    }

    /// <summary>Loads this provider's data from a stream.</summary>
    /// <param name="stream">The stream to read.</param>
    public abstract void Load(Stream stream);
  }
}
