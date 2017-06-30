// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.ConfigurationReloadToken
// Assembly: Microsoft.Extensions.Configuration, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 01735326-C363-4F22-987D-01A0C9D21466
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Configuration.1.1.0\lib\netstandard1.1\Microsoft.Extensions.Configuration.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;
    using System.Threading;
    using Primitives;

    /// <summary>
  /// Implements <see cref="T:Microsoft.Extensions.Primitives.IChangeToken" />
  /// </summary>
  public class ConfigurationReloadToken : IChangeToken
  {
    private CancellationTokenSource _cts = new CancellationTokenSource();

    /// <summary>
    /// Indicates if this token will proactively raise callbacks. Callbacks are still guaranteed to be invoked, eventually.
    /// </summary>
    public bool ActiveChangeCallbacks
    {
      get
      {
        return true;
      }
    }

    /// <summary>Gets a value that indicates if a change has occured.</summary>
    public bool HasChanged
    {
      get
      {
        return this._cts.IsCancellationRequested;
      }
    }

    /// <summary>
    /// Registers for a callback that will be invoked when the entry has changed. Microsoft.Extensions.Primitives.IChangeToken.HasChanged
    /// MUST be set before the callback is invoked.
    /// </summary>
    /// <param name="callback">The callback to invoke.</param>
    /// <param name="state">State to be passed into the callback.</param>
    /// <returns></returns>
    public IDisposable RegisterChangeCallback(Action<object> callback, object state)
    {
      return (IDisposable) this._cts.Token.Register(callback, state);
    }

    /// <summary>
    /// Used to trigger the change token when a reload occurs.
    /// </summary>
    public void OnReload()
    {
      this._cts.Cancel();
    }
  }
}
