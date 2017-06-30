// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Primitives.IChangeToken
// Assembly: Microsoft.Extensions.Primitives, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 4B558F24-C6D7-49A0-8D7E-92DB86F8FC53
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Primitives.1.1.0\lib\netstandard1.0\Microsoft.Extensions.Primitives.dll

namespace StingyBot.Configuration.MsOverride.Primitives
{
    using System;

    /// <summary>Propagates notifications that a change has occured.</summary>
  public interface IChangeToken
  {
    /// <summary>Gets a value that indicates if a change has occured.</summary>
    bool HasChanged { get; }

    /// <summary>
    /// Indicates if this token will pro-actively raise callbacks. Callbacks are still guaranteed to fire, eventually.
    /// </summary>
    bool ActiveChangeCallbacks { get; }

    IDisposable RegisterChangeCallback(Action<object> callback, object state);
  }
}
