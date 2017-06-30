// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileSystemGlobbing.Abstractions.DirectoryInfoBase
// Assembly: Microsoft.Extensions.FileSystemGlobbing, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 872D6D6C-5CAB-4A0C-99EB-7EE50238400F
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileSystemGlobbing.1.1.0\lib\net45\Microsoft.Extensions.FileSystemGlobbing.dll

namespace StingyBot.Configuration.MsOverride.FileSystemGlobbing.Abstractions
{
    using System.Collections.Generic;

    /// <summary>Represents a directory</summary>
  public abstract class DirectoryInfoBase : FileSystemInfoBase
  {
    /// <summary>
    /// Enumerates all files and directories in the directory.
    /// </summary>
    /// <returns>Collection of files and directories</returns>
    public abstract IEnumerable<FileSystemInfoBase> EnumerateFileSystemInfos();

    /// <summary>
    /// Returns an instance of <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.Abstractions.DirectoryInfoBase" /> that represents a subdirectory
    /// </summary>
    /// <param name="path">The directory name</param>
    /// <returns>Instance of <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.Abstractions.DirectoryInfoBase" /> even if directory does not exist</returns>
    public abstract DirectoryInfoBase GetDirectory(string path);

    /// <summary>
    /// Returns an instance of <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.Abstractions.FileInfoBase" /> that represents a file in the directory
    /// </summary>
    /// <param name="path">The file name</param>
    /// <returns>Instance of <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.Abstractions.FileInfoBase" /> even if file does not exist</returns>
    public abstract FileInfoBase GetFile(string path);
  }
}
