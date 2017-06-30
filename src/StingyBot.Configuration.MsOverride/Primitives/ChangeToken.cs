// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Primitives.ChangeToken
// Assembly: Microsoft.Extensions.Primitives, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 4B558F24-C6D7-49A0-8D7E-92DB86F8FC53
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Primitives.1.1.0\lib\netstandard1.0\Microsoft.Extensions.Primitives.dll

namespace StingyBot.Configuration.MsOverride.Primitives
{
    using System;

    /// <summary>Propagates notifications that a change has occured.</summary>
  public static class ChangeToken
  {
    public static IDisposable OnChange(Func<IChangeToken> changeTokenProducer, Action changeTokenConsumer)
    {
      if (changeTokenProducer == null)
        throw new ArgumentNullException("changeTokenProducer");
      if (changeTokenConsumer == null)
        throw new ArgumentNullException("changeTokenConsumer");
      Action<object> callback = (Action<object>) null;
      callback = (Action<object>) (s =>
      {
        IChangeToken changeToken = changeTokenProducer();
        try
        {
          changeTokenConsumer();
        }
        finally
        {
          changeToken.RegisterChangeCallback(callback, (object) null);
        }
      });
      return changeTokenProducer().RegisterChangeCallback(callback, (object) null);
    }

    public static IDisposable OnChange<TState>(Func<IChangeToken> changeTokenProducer, Action<TState> changeTokenConsumer, TState state)
    {
      if (changeTokenProducer == null)
        throw new ArgumentNullException("changeTokenProducer");
      if (changeTokenConsumer == null)
        throw new ArgumentNullException("changeTokenConsumer");
      Action<object> callback = (Action<object>) null;
      callback = (Action<object>) (s =>
      {
        IChangeToken changeToken = changeTokenProducer();
        try
        {
          changeTokenConsumer((TState) s);
        }
        finally
        {
          changeToken.RegisterChangeCallback(callback, s);
        }
      });
      return changeTokenProducer().RegisterChangeCallback(callback, (object) state);
    }
  }
}
