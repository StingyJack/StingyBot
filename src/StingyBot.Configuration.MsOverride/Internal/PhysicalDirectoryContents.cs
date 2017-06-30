// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileProviders.Internal.PhysicalDirectoryContents
// Assembly: Microsoft.Extensions.FileProviders.Physical, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 770B57DD-E4BD-4C5B-8297-BE3C6E5B22A3
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileProviders.Physical.1.1.0\lib\net451\Microsoft.Extensions.FileProviders.Physical.dll

namespace StingyBot.Configuration.MsOverride.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>Represents the contents of a physical file directory</summary>
  public class PhysicalDirectoryContents : IDirectoryContents, IEnumerable<IFileInfo>, IEnumerable
  {
    private IEnumerable<IFileInfo> _entries;
    private readonly string _directory;

    /// <inheritdoc />
    public bool Exists
    {
      get
      {
        return Directory.Exists(this._directory);
      }
    }

    /// <summary>
    /// Initializes an instance of <see cref="T:StingyBot.Configuration.MsOverride.Internal.PhysicalDirectoryContents" />
    /// </summary>
    /// <param name="directory">The directory</param>
    public PhysicalDirectoryContents(string directory)
    {
      if (directory == null)
        throw new ArgumentNullException("directory");
      this._directory = directory;
    }

    /// <inheritdoc />
    public IEnumerator<IFileInfo> GetEnumerator()
    {
      this.EnsureInitialized();
      return this._entries.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      this.EnsureInitialized();
      return (IEnumerator) this._entries.GetEnumerator();
    }

    private void EnsureInitialized()
    {
      try
      {
        this._entries = new DirectoryInfo(this._directory).EnumerateFileSystemInfos().Where<FileSystemInfo>((Func<FileSystemInfo, bool>) (info => !FileSystemInfoHelper.IsHiddenFile(info))).Select<FileSystemInfo, IFileInfo>((Func<FileSystemInfo, IFileInfo>) (info =>
        {
          if (info is FileInfo)
            return (IFileInfo) new PhysicalFileInfo((FileInfo) info);
          if (info is DirectoryInfo)
            return (IFileInfo) new PhysicalDirectoryInfo((DirectoryInfo) info);
          throw new InvalidOperationException("Unexpected type of FileSystemInfo");
        }));
      }
      catch (Exception ex) when (ex is DirectoryNotFoundException || ex is IOException)
      {
        this._entries = Enumerable.Empty<IFileInfo>();
      }
    }
  }
}
