// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileSystemGlobbing.Abstractions.FileInfoWrapper
// Assembly: Microsoft.Extensions.FileSystemGlobbing, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 872D6D6C-5CAB-4A0C-99EB-7EE50238400F
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileSystemGlobbing.1.1.0\lib\net45\Microsoft.Extensions.FileSystemGlobbing.dll

namespace StingyBot.Configuration.MsOverride.FileSystemGlobbing.Abstractions
{
    using System.IO;

    /// <summary>
  /// Wraps an instance of <see cref="T:System.IO.FileInfo" /> to provide implementation of <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.Abstractions.FileInfoBase" />.
  /// </summary>
  public class FileInfoWrapper : FileInfoBase
  {
    private readonly FileInfo _fileInfo;

    /// <summary>
    /// The file name. (Overrides <see cref="P:StingyBot.Configuration.MsOverride.FileSystemGlobbing.Abstractions.FileSystemInfoBase.Name" />).
    /// </summary>
    /// <remarks>
    /// Equals the value of <see cref="P:System.IO.FileInfo.Name" />.
    /// </remarks>
    public override string Name
    {
      get
      {
        return this._fileInfo.Name;
      }
    }

    /// <summary>
    /// The full path of the file. (Overrides <see cref="P:StingyBot.Configuration.MsOverride.FileSystemGlobbing.Abstractions.FileSystemInfoBase.FullName" />).
    /// </summary>
    /// <remarks>
    /// Equals the value of <see cref="P:System.IO.FileSystemInfo.Name" />.
    /// </remarks>
    public override string FullName
    {
      get
      {
        return this._fileInfo.FullName;
      }
    }

    /// <summary>
    /// The directory containing the file. (Overrides <see cref="P:StingyBot.Configuration.MsOverride.FileSystemGlobbing.Abstractions.FileSystemInfoBase.ParentDirectory" />).
    /// </summary>
    /// <remarks>
    /// Equals the value of <see cref="P:System.IO.FileInfo.Directory" />.
    /// </remarks>
    public override DirectoryInfoBase ParentDirectory
    {
      get
      {
        return (DirectoryInfoBase) new DirectoryInfoWrapper(this._fileInfo.Directory, false);
      }
    }

    /// <summary>
    /// Initializes instance of <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.Abstractions.FileInfoWrapper" /> to wrap the specified object <see cref="T:System.IO.FileInfo" />.
    /// </summary>
    /// <param name="fileInfo">The <see cref="T:System.IO.FileInfo" /></param>
    public FileInfoWrapper(FileInfo fileInfo)
    {
      this._fileInfo = fileInfo;
    }
  }
}
