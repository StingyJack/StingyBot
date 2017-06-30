// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileProviders.NullChangeToken
// Assembly: Microsoft.Extensions.FileProviders.Abstractions, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 5221F0E5-502E-4A57-B7FC-EECCEB80C8A4
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileProviders.Abstractions.1.1.0\lib\netstandard1.0\Microsoft.Extensions.FileProviders.Abstractions.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;
    using Primitives;

    /// <summary>
  /// An empty change token that doesn't raise any change callbacks.
  /// </summary>
  public class NullChangeToken : IChangeToken
  {
    /// <summary>
    /// A singleton instance of <see cref="T:StingyBot.Configuration.MsOverride.NullChangeToken" />
    /// </summary>
    public static NullChangeToken Singleton { get; } = new NullChangeToken();

    /// <summary>Always false.</summary>
    public bool HasChanged
    {
      get
      {
        return false;
      }
    }

    /// <summary>Always false.</summary>
    public bool ActiveChangeCallbacks
    {
      get
      {
        return false;
      }
    }

    private NullChangeToken()
    {
    }

    public IDisposable RegisterChangeCallback(Action<object> callback, object state)
    {
      return (IDisposable) EmptyDisposable.Instance;
    }
  }
}
