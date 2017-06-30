// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileProviders.Physical.PhysicalFileInfo
// Assembly: Microsoft.Extensions.FileProviders.Physical, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 770B57DD-E4BD-4C5B-8297-BE3C6E5B22A3
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileProviders.Physical.1.1.0\lib\net451\Microsoft.Extensions.FileProviders.Physical.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;
    using System.IO;

    /// <summary>Represents a file on a physical filesystem</summary>
  public class PhysicalFileInfo : IFileInfo
  {
    private readonly FileInfo _info;

    /// <inheritdoc />
    public bool Exists
    {
      get
      {
        return this._info.Exists;
      }
    }

    /// <inheritdoc />
    public long Length
    {
      get
      {
        return this._info.Length;
      }
    }

    /// <inheritdoc />
    public string PhysicalPath
    {
      get
      {
        return this._info.FullName;
      }
    }

    /// <inheritdoc />
    public string Name
    {
      get
      {
        return this._info.Name;
      }
    }

    /// <inheritdoc />
    public DateTimeOffset LastModified
    {
      get
      {
        return (DateTimeOffset) this._info.LastWriteTimeUtc;
      }
    }

    /// <summary>Always false.</summary>
    public bool IsDirectory
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    /// Initializes an instance of <see cref="T:StingyBot.Configuration.MsOverride.PhysicalFileInfo" /> that wraps an instance of <see cref="T:System.IO.FileInfo" />
    /// </summary>
    /// <param name="info">The <see cref="T:System.IO.FileInfo" /></param>
    public PhysicalFileInfo(FileInfo info)
    {
      this._info = info;
    }

    /// <inheritdoc />
    public Stream CreateReadStream()
    {
      return (Stream) new FileStream(this.PhysicalPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 1, FileOptions.Asynchronous | FileOptions.SequentialScan);
    }
  }
}
