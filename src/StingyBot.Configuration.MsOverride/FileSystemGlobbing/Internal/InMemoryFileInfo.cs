// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileSystemGlobbing.Internal.InMemoryFileInfo
// Assembly: Microsoft.Extensions.FileSystemGlobbing, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 872D6D6C-5CAB-4A0C-99EB-7EE50238400F
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileSystemGlobbing.1.1.0\lib\net45\Microsoft.Extensions.FileSystemGlobbing.dll

namespace StingyBot.Configuration.MsOverride.FileSystemGlobbing.Internal
{
    using System.IO;

    internal class InMemoryFileInfo : FileInfoBase
  {
    private InMemoryDirectoryInfo _parent;

    public override string FullName { get; }

    public override string Name { get; }

    public override DirectoryInfoBase ParentDirectory
    {
      get
      {
        return (DirectoryInfoBase) this._parent;
      }
    }

    public InMemoryFileInfo(string file, InMemoryDirectoryInfo parent)
    {
      this.FullName = file;
      this.Name = Path.GetFileName(file);
      this._parent = parent;
    }
  }
}
