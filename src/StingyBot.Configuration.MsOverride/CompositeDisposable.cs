// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileProviders.CompositeDisposable
// Assembly: Microsoft.Extensions.FileProviders.Physical, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 770B57DD-E4BD-4C5B-8297-BE3C6E5B22A3
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileProviders.Physical.1.1.0\lib\net451\Microsoft.Extensions.FileProviders.Physical.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;
    using System.Collections.Generic;

    /// <summary>
  /// Represents a composition of <see cref="T:System.IDisposable" />.
  /// </summary>
  internal class CompositeDisposable : IDisposable
  {
    private readonly IList<IDisposable> _disposables;

    /// <summary>
    /// Creates a new instance of <see cref="T:StingyBot.Configuration.MsOverride.CompositeDisposable" />.
    /// </summary>
    /// <param name="disposables">The list of <see cref="T:System.IDisposable" /> to compose.</param>
    public CompositeDisposable(IList<IDisposable> disposables)
    {
      if (disposables == null)
        throw new ArgumentNullException("disposables");
      this._disposables = disposables;
    }

    public void Dispose()
    {
      for (int index = 0; index < this._disposables.Count; ++index)
        this._disposables[index].Dispose();
    }
  }
}
