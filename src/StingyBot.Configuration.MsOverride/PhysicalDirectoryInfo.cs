// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileProviders.Physical.PhysicalDirectoryInfo
// Assembly: Microsoft.Extensions.FileProviders.Physical, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 770B57DD-E4BD-4C5B-8297-BE3C6E5B22A3
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileProviders.Physical.1.1.0\lib\net451\Microsoft.Extensions.FileProviders.Physical.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;
    using System.IO;

    /// <summary>Represents a directory on a physical filesystem</summary>
  public class PhysicalDirectoryInfo : IFileInfo
  {
    private readonly DirectoryInfo _info;

    /// <inheritdoc />
    public bool Exists
    {
      get
      {
        return this._info.Exists;
      }
    }

    /// <summary>Always equals -1.</summary>
    public long Length
    {
      get
      {
        return -1;
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

    /// <summary>The time when the directory was last written to.</summary>
    public DateTimeOffset LastModified
    {
      get
      {
        return (DateTimeOffset) this._info.LastWriteTimeUtc;
      }
    }

    /// <summary>Always true.</summary>
    public bool IsDirectory
    {
      get
      {
        return true;
      }
    }

    /// <summary>
    /// Initializes an instance of <see cref="T:StingyBot.Configuration.MsOverride.PhysicalDirectoryInfo" /> that wraps an instance of <see cref="T:System.IO.DirectoryInfo" />
    /// </summary>
    /// <param name="info">The directory</param>
    public PhysicalDirectoryInfo(DirectoryInfo info)
    {
      this._info = info;
    }

    /// <summary>
    /// Always throws an exception because read streams are not support on directories.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">Always thrown</exception>
    /// <returns>Never returns</returns>
    public Stream CreateReadStream()
    {
      throw new InvalidOperationException("Cannot create a stream for a directory.");
    }
  }
}
