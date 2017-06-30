// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileProviders.IDirectoryContents
// Assembly: Microsoft.Extensions.FileProviders.Abstractions, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 5221F0E5-502E-4A57-B7FC-EECCEB80C8A4
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileProviders.Abstractions.1.1.0\lib\netstandard1.0\Microsoft.Extensions.FileProviders.Abstractions.dll

namespace StingyBot.Configuration.MsOverride
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
  /// Represents a directory's content in the file provider.
  /// </summary>
  public interface IDirectoryContents : IEnumerable<IFileInfo>, IEnumerable
  {
    /// <summary>True if a directory was located at the given path.</summary>
    bool Exists { get; }
  }
}
