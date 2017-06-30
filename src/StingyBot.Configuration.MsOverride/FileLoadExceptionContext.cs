// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.FileLoadExceptionContext
// Assembly: Microsoft.Extensions.Configuration.FileExtensions, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 818349F0-7064-4187-BABE-750AE8E17D6B
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Configuration.FileExtensions.1.1.0\lib\net451\Microsoft.Extensions.Configuration.FileExtensions.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;

    /// <summary>Contains information about a file load exception.</summary>
  public class FileLoadExceptionContext
  {
    /// <summary>
    /// The <see cref="T:Microsoft.Extensions.Configuration.FileConfigurationProvider" /> that caused the exception.
    /// </summary>
    public FileConfigurationProvider Provider { get; set; }

    /// <summary>The exception that occured in Load.</summary>
    public Exception Exception { get; set; }

    /// <summary>If true, the exception will not be rethrown.</summary>
    public bool Ignore { get; set; }
  }
}
