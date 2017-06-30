// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileSystemGlobbing.Abstractions.DirectoryInfoWrapper
// Assembly: Microsoft.Extensions.FileSystemGlobbing, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 872D6D6C-5CAB-4A0C-99EB-7EE50238400F
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileSystemGlobbing.1.1.0\lib\net45\Microsoft.Extensions.FileSystemGlobbing.dll

namespace StingyBot.Configuration.MsOverride.FileSystemGlobbing.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
  /// Wraps an instance of <see cref="T:System.IO.DirectoryInfo" /> and provides implementation of
  /// <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.Abstractions.DirectoryInfoBase" />.
  /// </summary>
  public class DirectoryInfoWrapper : DirectoryInfoBase
  {
    private readonly DirectoryInfo _directoryInfo;
    private readonly bool _isParentPath;

    /// <inheritdoc />
    public override string Name
    {
      get
      {
        if (!this._isParentPath)
          return this._directoryInfo.Name;
        return "..";
      }
    }

    /// <summary>Returns the full path to the directory.</summary>
    /// <remarks>
    /// Equals the value of <seealso cref="P:System.IO.FileSystemInfo.FullName" />.
    /// </remarks>
    public override string FullName
    {
      get
      {
        return this._directoryInfo.FullName;
      }
    }

    /// <summary>Returns the parent directory.</summary>
    /// <remarks>
    /// Equals the value of <seealso cref="P:System.IO.DirectoryInfo.Parent" />.
    /// </remarks>
    public override DirectoryInfoBase ParentDirectory
    {
      get
      {
        return (DirectoryInfoBase) new DirectoryInfoWrapper(this._directoryInfo.Parent, false);
      }
    }

    /// <summary>
    /// Initializes an instance of <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.Abstractions.DirectoryInfoWrapper" />.
    /// </summary>
    /// <param name="directoryInfo">The <see cref="T:System.IO.DirectoryInfo" /></param>
    /// <param name="isParentPath">
    /// <c>true</c> when the <paramref name="directoryInfo" /> should be represented as the parent
    /// directory with '..'
    /// </param>
    public DirectoryInfoWrapper(DirectoryInfo directoryInfo, bool isParentPath = false)
    {
      this._directoryInfo = directoryInfo;
      this._isParentPath = isParentPath;
    }

    /// <inheritdoc />
    public override IEnumerable<FileSystemInfoBase> EnumerateFileSystemInfos()
    {
      if (this._directoryInfo.Exists)
      {
        foreach (FileSystemInfo enumerateFileSystemInfo in this._directoryInfo.EnumerateFileSystemInfos("*", SearchOption.TopDirectoryOnly))
        {
          FileSystemInfo fileSystemInfo = enumerateFileSystemInfo;
          DirectoryInfo directoryInfo = fileSystemInfo as DirectoryInfo;
          if (directoryInfo != null)
            yield return (FileSystemInfoBase) new DirectoryInfoWrapper(directoryInfo, false);
          else
            yield return (FileSystemInfoBase) new FileInfoWrapper((FileInfo) fileSystemInfo);
          fileSystemInfo = (FileSystemInfo) null;
        }
      }
    }

    /// <summary>
    /// Returns an instance of <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.Abstractions.DirectoryInfoBase" /> that represents a subdirectory.
    /// </summary>
    /// <remarks>
    /// If <paramref name="name" /> equals '..', this returns the parent directory.
    /// </remarks>
    /// <param name="name">The directory name</param>
    /// <returns>The directory</returns>
    public override DirectoryInfoBase GetDirectory(string name)
    {
      bool isParentPath = string.Equals(name, "..", StringComparison.Ordinal);
      if (isParentPath)
        return (DirectoryInfoBase) new DirectoryInfoWrapper(new DirectoryInfo(Path.Combine(this._directoryInfo.FullName, name)), isParentPath);
      DirectoryInfo[] directories = this._directoryInfo.GetDirectories(name);
      if (directories.Length == 1)
        return (DirectoryInfoBase) new DirectoryInfoWrapper(directories[0], isParentPath);
      if (directories.Length == 0)
        return (DirectoryInfoBase) null;
      throw new InvalidOperationException(string.Format("More than one sub directories are found under {0} with name {1}.", (object) this._directoryInfo.FullName, (object) name));
    }

    /// <inheritdoc />
    public override FileInfoBase GetFile(string name)
    {
      return (FileInfoBase) new FileInfoWrapper(new FileInfo(Path.Combine(this._directoryInfo.FullName, name)));
    }
  }
}
