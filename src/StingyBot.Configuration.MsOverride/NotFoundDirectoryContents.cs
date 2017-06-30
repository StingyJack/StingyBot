// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileProviders.NotFoundDirectoryContents
// Assembly: Microsoft.Extensions.FileProviders.Abstractions, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 5221F0E5-502E-4A57-B7FC-EECCEB80C8A4
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileProviders.Abstractions.1.1.0\lib\netstandard1.0\Microsoft.Extensions.FileProviders.Abstractions.dll

namespace StingyBot.Configuration.MsOverride
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>Represents a non-existing directory</summary>
  public class NotFoundDirectoryContents : IDirectoryContents, IEnumerable<IFileInfo>, IEnumerable
  {
    /// <summary>
    /// A shared instance of <see cref="T:StingyBot.Configuration.MsOverride.NotFoundDirectoryContents" />
    /// </summary>
    public static NotFoundDirectoryContents Singleton { get; } = new NotFoundDirectoryContents();

    /// <summary>Always false.</summary>
    public bool Exists
    {
      get
      {
        return false;
      }
    }

    /// <summary>Returns an enumerator that iterates through the collection.</summary>
    /// <returns>An enumerator to an empty collection.</returns>
    public IEnumerator<IFileInfo> GetEnumerator()
    {
      return Enumerable.Empty<IFileInfo>().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }
  }
}
