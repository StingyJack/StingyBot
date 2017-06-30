// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileProviders.CompositeFileChangeToken
// Assembly: Microsoft.Extensions.FileProviders.Physical, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 770B57DD-E4BD-4C5B-8297-BE3C6E5B22A3
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileProviders.Physical.1.1.0\lib\net451\Microsoft.Extensions.FileProviders.Physical.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;
    using System.Collections.Generic;
    using Primitives;

    /// <summary>
  /// Represents a composition of <see cref="T:Microsoft.Extensions.Primitives.IChangeToken" />.
  /// </summary>
  internal class CompositeFileChangeToken : IChangeToken
  {
    public IList<IChangeToken> ChangeTokens { get; }

    public bool HasChanged
    {
      get
      {
        for (int index = 0; index < this.ChangeTokens.Count; ++index)
        {
          if (this.ChangeTokens[index].HasChanged)
            return true;
        }
        return false;
      }
    }

    public bool ActiveChangeCallbacks
    {
      get
      {
        for (int index = 0; index < this.ChangeTokens.Count; ++index)
        {
          if (this.ChangeTokens[index].ActiveChangeCallbacks)
            return true;
        }
        return false;
      }
    }

    /// <summary>
    /// Creates a new instance of <see cref="T:StingyBot.Configuration.MsOverride.CompositeFileChangeToken" />.
    /// </summary>
    /// <param name="changeTokens">The list of <see cref="T:Microsoft.Extensions.Primitives.IChangeToken" /> to compose.</param>
    public CompositeFileChangeToken(IList<IChangeToken> changeTokens)
    {
      if (changeTokens == null)
        throw new ArgumentNullException("changeTokens");
      this.ChangeTokens = changeTokens;
    }

    public IDisposable RegisterChangeCallback(Action<object> callback, object state)
    {
      List<IDisposable> disposableList = new List<IDisposable>(this.ChangeTokens.Count);
      for (int index = 0; index < this.ChangeTokens.Count; ++index)
      {
        IDisposable disposable = this.ChangeTokens[index].RegisterChangeCallback(callback, state);
        disposableList.Add(disposable);
      }
      return (IDisposable) new CompositeDisposable((IList<IDisposable>) disposableList);
    }
  }
}
