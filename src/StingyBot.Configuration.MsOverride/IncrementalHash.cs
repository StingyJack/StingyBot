// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileProviders.Physical.IncrementalHash
// Assembly: Microsoft.Extensions.FileProviders.Physical, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 770B57DD-E4BD-4C5B-8297-BE3C6E5B22A3
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileProviders.Physical.1.1.0\lib\net451\Microsoft.Extensions.FileProviders.Physical.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;
    using System.Reflection;
    using System.Security.Cryptography;

    internal class IncrementalHash : IDisposable
  {
    private static readonly byte[] EmptyArray = new byte[0];
    private readonly SHA256 _sha256 = IncrementalHash.CreateSHA256();
    private bool _initialized;

    public void AppendData(byte[] data, int offset, int count)
    {
      this.EnsureInitialized();
      this._sha256.TransformBlock(data, offset, count, (byte[]) null, 0);
    }

    public byte[] GetHashAndReset()
    {
      this._sha256.TransformFinalBlock(IncrementalHash.EmptyArray, 0, 0);
      return this._sha256.Hash;
    }

    public void Dispose()
    {
      this._sha256.Dispose();
    }

    private void EnsureInitialized()
    {
      if (this._initialized)
        return;
      this._sha256.Initialize();
      this._initialized = true;
    }

    private static SHA256 CreateSHA256()
    {
      try
      {
        return SHA256.Create();
      }
      catch (TargetInvocationException )
      {
        return (SHA256) new SHA256CryptoServiceProvider();
      }
    }
  }
}
