// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileProviders.FileSystemInfoHelper
// Assembly: Microsoft.Extensions.FileProviders.Physical, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 770B57DD-E4BD-4C5B-8297-BE3C6E5B22A3
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileProviders.Physical.1.1.0\lib\net451\Microsoft.Extensions.FileProviders.Physical.dll

namespace StingyBot.Configuration.MsOverride
{
    using System.IO;

    /// <summary>Helper functions for working with the filesystem</summary>
  public class FileSystemInfoHelper
  {
    internal static bool IsHiddenFile(FileSystemInfo fileSystemInfo)
    {
      return fileSystemInfo.Name.StartsWith(".") || fileSystemInfo.Exists && ((fileSystemInfo.Attributes & FileAttributes.Hidden) != (FileAttributes) 0 || (fileSystemInfo.Attributes & FileAttributes.System) != (FileAttributes) 0);
    }
  }
}
