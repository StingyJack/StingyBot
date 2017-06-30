// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileSystemGlobbing.Abstractions.FileSystemInfoBase
// Assembly: Microsoft.Extensions.FileSystemGlobbing, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 872D6D6C-5CAB-4A0C-99EB-7EE50238400F
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileSystemGlobbing.1.1.0\lib\net45\Microsoft.Extensions.FileSystemGlobbing.dll

namespace StingyBot.Configuration.MsOverride.FileSystemGlobbing.Abstractions
{
  /// <summary>Shared abstraction for files and directories</summary>
  public abstract class FileSystemInfoBase
  {
    /// <summary>A string containing the name of the file or directory</summary>
    public abstract string Name { get; }

    /// <summary>
    /// A string containing the full path of the file or directory
    /// </summary>
    public abstract string FullName { get; }

    /// <summary>
    /// The parent directory for the current file or directory
    /// </summary>
    public abstract DirectoryInfoBase ParentDirectory { get; }
  }
}
