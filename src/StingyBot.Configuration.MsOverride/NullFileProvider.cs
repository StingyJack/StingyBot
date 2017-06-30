// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileProviders.NullFileProvider
// Assembly: Microsoft.Extensions.FileProviders.Abstractions, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 5221F0E5-502E-4A57-B7FC-EECCEB80C8A4
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileProviders.Abstractions.1.1.0\lib\netstandard1.0\Microsoft.Extensions.FileProviders.Abstractions.dll

namespace StingyBot.Configuration.MsOverride
{
    using Primitives;

    /// <summary>An empty file provider with no contents.</summary>
  public class NullFileProvider : IFileProvider
  {
    /// <summary>Enumerate a non-existent directory.</summary>
    /// <param name="subpath">A path under the root directory. This parameter is ignored.</param>
    /// <returns>A <see cref="T:StingyBot.Configuration.MsOverride.IDirectoryContents" /> that does not exist and does not contain any contents.</returns>
    public IDirectoryContents GetDirectoryContents(string subpath)
    {
      return (IDirectoryContents) NotFoundDirectoryContents.Singleton;
    }

    /// <summary>Locate a non-existent file.</summary>
    /// <param name="subpath">A path under the root directory.</param>
    /// <returns>A <see cref="T:StingyBot.Configuration.MsOverride.IFileInfo" /> representing a non-existent file at the given path.</returns>
    public IFileInfo GetFileInfo(string subpath)
    {
      return (IFileInfo) new NotFoundFileInfo(subpath);
    }

    /// <summary>
    /// Returns a <see cref="T:Microsoft.Extensions.Primitives.IChangeToken" /> that monitors nothing.
    /// </summary>
    /// <param name="filter">Filter string used to determine what files or folders to monitor. This parameter is ignored.</param>
    /// <returns>A <see cref="T:Microsoft.Extensions.Primitives.IChangeToken" /> that does not register callbacks.</returns>
    public IChangeToken Watch(string filter)
    {
      return (IChangeToken) NullChangeToken.Singleton;
    }
  }
}
