// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Primitives.StringTokenizer
// Assembly: Microsoft.Extensions.Primitives, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 4B558F24-C6D7-49A0-8D7E-92DB86F8FC53
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Primitives.1.1.0\lib\netstandard1.0\Microsoft.Extensions.Primitives.dll

namespace StingyBot.Configuration.MsOverride.Primitives
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
  /// Tokenizes a <c>string</c> into <see cref="T:StingyBot.Configuration.MsOverride.Primitives.StringSegment" />s.
  /// </summary>
  public struct StringTokenizer : IEnumerable<StringSegment>, IEnumerable
  {
    private readonly string _value;
    private readonly char[] _separators;

    /// <summary>
    /// Initializes a new instance of <see cref="T:StingyBot.Configuration.MsOverride.Primitives.StringTokenizer" />.
    /// </summary>
    /// <param name="value">The <c>string</c> to tokenize.</param>
    /// <param name="separators">The characters to tokenize by.</param>
    public StringTokenizer(string value, char[] separators)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (separators == null)
        throw new ArgumentNullException("separators");
      this._value = value;
      this._separators = separators;
    }

    public StringTokenizer.Enumerator GetEnumerator()
    {
      return new StringTokenizer.Enumerator(ref this);
    }

    IEnumerator<StringSegment> IEnumerable<StringSegment>.GetEnumerator()
    {
      return (IEnumerator<StringSegment>) this.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public struct Enumerator : IEnumerator<StringSegment>, IEnumerator, IDisposable
    {
      private readonly string _value;
      private readonly char[] _separators;
      private int _index;

      public StringSegment Current { get; private set; }

      object IEnumerator.Current
      {
        get
        {
          return (object) this.Current;
        }
      }

      public Enumerator(ref StringTokenizer tokenizer)
      {
        this._value = tokenizer._value;
        this._separators = tokenizer._separators;
        this.Current = new StringSegment();
        this._index = 0;
      }

      public void Dispose()
      {
      }

      public bool MoveNext()
      {
        if (this._value == null || this._index > this._value.Length)
        {
          this.Current = new StringSegment();
          return false;
        }
        int num = this._value.IndexOfAny(this._separators, this._index);
        if (num == -1)
          num = this._value.Length;
        this.Current = new StringSegment(this._value, this._index, num - this._index);
        this._index = num + 1;
        return true;
      }

      public void Reset()
      {
        this.Current = new StringSegment();
        this._index = 0;
      }
    }
  }
}
