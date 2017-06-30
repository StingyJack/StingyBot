// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Primitives.InplaceStringBuilder
// Assembly: Microsoft.Extensions.Primitives, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 4B558F24-C6D7-49A0-8D7E-92DB86F8FC53
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Primitives.1.1.0\lib\netstandard1.0\Microsoft.Extensions.Primitives.dll

#pragma warning disable 649
namespace StingyBot.Configuration.MsOverride.Primitives
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("Value = {_value}")]
  public struct InplaceStringBuilder
  {
    private int _capacity;
    private int _offset;
    private bool _writing;
    private string _value;

    public int Capacity
    {
      get
      {
        return this._capacity;
      }
      set
      {
        if (value < 0)
          throw new ArgumentOutOfRangeException("value");
        if (this._writing)
          throw new InvalidOperationException("Cannot change capacity after write started.");
        this._capacity = value;
      }
    }

    public InplaceStringBuilder(int capacity)
    {
      this = new InplaceStringBuilder();
      this._capacity = capacity;
    }
/*
    public unsafe void Append(string s)
    {
      this.EnsureCapacity(s.Length);
      string str1 = this._value;
      char* chPtr1 = (char*) str1;
      if ((IntPtr) chPtr1 != IntPtr.Zero)
        chPtr1 += RuntimeHelpers.OffsetToStringData;
      string str2 = s;
      char* chPtr2 = (char*) str2;
      if ((IntPtr) chPtr2 != IntPtr.Zero)
        chPtr2 += RuntimeHelpers.OffsetToStringData;
      Unsafe.CopyBlock((void*) (chPtr1 + this._offset), (void*) chPtr2, (uint) (s.Length * 2));
      this._offset = this._offset + s.Length;
      str2 = (string) null;
      str1 = (string) null;
    }

    public unsafe void Append(char c)
    {
      this.EnsureCapacity(1);
      string str = this._value;
      char* chPtr1 = (char*) str;
      if ((IntPtr) chPtr1 != IntPtr.Zero)
        chPtr1 += RuntimeHelpers.OffsetToStringData;
      char* chPtr2 = chPtr1;
      int offset = this._offset;
      this._offset = offset + 1;
      IntPtr num = (IntPtr) offset * 2;
      *(short*) ((IntPtr) chPtr2 + num) = (short) c;
      str = (string) null;
    }
*/
    private void EnsureCapacity(int length)
    {
      if (this._value == null)
      {
        this._writing = true;
        this._value = new string(char.MinValue, this._capacity);
      }
      if (this._offset + length > this._capacity)
        throw new InvalidOperationException(string.Format("Not enough capacity to write '{0}' characters, only '{1}' left.", new object[2]{ (object) length, (object) (this._capacity - this._offset) }));
    }

    public override string ToString()
    {
      if (this._offset != this._capacity)
        throw new InvalidOperationException(string.Format("Entire reserved capacity was not used. Capacity: '{0}', written '{1}'.", new object[2]{ (object) this._capacity, (object) this._offset }));
      return this._value;
    }
  }
}
