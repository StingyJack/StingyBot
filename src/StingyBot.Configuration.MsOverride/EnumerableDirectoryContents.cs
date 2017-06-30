// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileProviders.EnumerableDirectoryContents
// Assembly: Microsoft.Extensions.FileProviders.Physical, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 770B57DD-E4BD-4C5B-8297-BE3C6E5B22A3
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileProviders.Physical.1.1.0\lib\net451\Microsoft.Extensions.FileProviders.Physical.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class EnumerableDirectoryContents : IDirectoryContents, IEnumerable<IFileInfo>, IEnumerable
  {
    private readonly IEnumerable<IFileInfo> _entries;

    public bool Exists
    {
      get
      {
        return true;
      }
    }

    public EnumerableDirectoryContents(IEnumerable<IFileInfo> entries)
    {
      if (entries == null)
        throw new ArgumentNullException("entries");
      this._entries = entries;
    }

    public IEnumerator<IFileInfo> GetEnumerator()
    {
      return this._entries.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this._entries.GetEnumerator();
    }
  }
}
