// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Primitives.CancellationChangeToken
// Assembly: Microsoft.Extensions.Primitives, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 4B558F24-C6D7-49A0-8D7E-92DB86F8FC53
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Primitives.1.1.0\lib\netstandard1.0\Microsoft.Extensions.Primitives.dll

namespace StingyBot.Configuration.MsOverride.Primitives
{
    using System;
    using System.Threading;

    /// <summary>
  /// A <see cref="T:StingyBot.Configuration.MsOverride.Primitives.IChangeToken" /> implementation using <see cref="T:System.Threading.CancellationToken" />.
  /// </summary>
  public class CancellationChangeToken : IChangeToken
  {
    /// <inheritdoc />
    public bool ActiveChangeCallbacks
    {
      get
      {
        return true;
      }
    }

    /// <inheritdoc />
    public bool HasChanged
    {
      get
      {
        return this.Token.IsCancellationRequested;
      }
    }

    private CancellationToken Token { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="T:StingyBot.Configuration.MsOverride.Primitives.CancellationChangeToken" />.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" />.</param>
    public CancellationChangeToken(CancellationToken cancellationToken)
    {
      this.Token = cancellationToken;
    }

    public IDisposable RegisterChangeCallback(Action<object> callback, object state)
    {
      return (IDisposable) this.Token.Register(callback, state);
    }
  }
}
