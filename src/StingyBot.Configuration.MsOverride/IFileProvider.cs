// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileProviders.IFileProvider
// Assembly: Microsoft.Extensions.FileProviders.Abstractions, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 5221F0E5-502E-4A57-B7FC-EECCEB80C8A4
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileProviders.Abstractions.1.1.0\lib\netstandard1.0\Microsoft.Extensions.FileProviders.Abstractions.dll

namespace StingyBot.Configuration.MsOverride
{
    using Primitives;

    /// <summary>A read-only file provider abstraction.</summary>
  public interface IFileProvider
  {
    /// <summary>Locate a file at the given path.</summary>
    /// <param name="subpath">Relative path that identifies the file.</param>
    /// <returns>The file information. Caller must check Exists property.</returns>
    IFileInfo GetFileInfo(string subpath);

    /// <summary>Enumerate a directory at the given path, if any.</summary>
    /// <param name="subpath">Relative path that identifies the directory.</param>
    /// <returns>Returns the contents of the directory.</returns>
    IDirectoryContents GetDirectoryContents(string subpath);

    /// <summary>
    /// Creates a <see cref="T:Microsoft.Extensions.Primitives.IChangeToken" /> for the specified <paramref name="filter" />.
    /// </summary>
    /// <param name="filter">Filter string used to determine what files or folders to monitor. Example: **/*.cs, *.*, subFolder/**/*.cshtml.</param>
    /// <returns>An <see cref="T:Microsoft.Extensions.Primitives.IChangeToken" /> that is notified when a file matching <paramref name="filter" /> is added, modified or deleted.</returns>
    IChangeToken Watch(string filter);
  }
}
