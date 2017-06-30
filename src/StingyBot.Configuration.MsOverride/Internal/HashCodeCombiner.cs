// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Internal.HashCodeCombiner
// Assembly: Microsoft.Extensions.FileSystemGlobbing, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 872D6D6C-5CAB-4A0C-99EB-7EE50238400F
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileSystemGlobbing.1.1.0\lib\net45\Microsoft.Extensions.FileSystemGlobbing.dll

namespace StingyBot.Configuration.MsOverride.Internal
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal struct HashCodeCombiner
  {
    private long _combinedHash64;

    public int CombinedHash
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return this._combinedHash64.GetHashCode();
      }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private HashCodeCombiner(long seed)
    {
      this._combinedHash64 = seed;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator int(HashCodeCombiner self)
    {
      return self.CombinedHash;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(IEnumerable e)
    {
      if (e == null)
      {
        this.Add(0);
      }
      else
      {
        int i = 0;
        foreach (object o in e)
        {
          this.Add(o);
          ++i;
        }
        this.Add(i);
      }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(int i)
    {
      this._combinedHash64 = (this._combinedHash64 << 5) + this._combinedHash64 ^ (long) i;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(string s)
    {
      this.Add(s != null ? s.GetHashCode() : 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(object o)
    {
      this.Add(o != null ? o.GetHashCode() : 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add<TValue>(TValue value, IEqualityComparer<TValue> comparer)
    {
      this.Add((object) value != null ? comparer.GetHashCode(value) : 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static HashCodeCombiner Start()
    {
      return new HashCodeCombiner(5381L);
    }
  }
}
