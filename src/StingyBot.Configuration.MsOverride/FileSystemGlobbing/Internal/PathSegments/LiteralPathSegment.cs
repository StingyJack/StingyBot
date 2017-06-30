// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments.LiteralPathSegment
// Assembly: Microsoft.Extensions.FileSystemGlobbing, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 872D6D6C-5CAB-4A0C-99EB-7EE50238400F
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileSystemGlobbing.1.1.0\lib\net45\Microsoft.Extensions.FileSystemGlobbing.dll

namespace StingyBot.Configuration.MsOverride.FileSystemGlobbing.Internal.PathSegments
{
    using System;
    using Util;

    public class LiteralPathSegment : IPathSegment
  {
    private readonly StringComparison _comparisonType;

    public bool CanProduceStem
    {
      get
      {
        return false;
      }
    }

    public string Value { get; }

    public LiteralPathSegment(string value, StringComparison comparisonType)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      this.Value = value;
      this._comparisonType = comparisonType;
    }

    public bool Match(string value)
    {
      return string.Equals(this.Value, value, this._comparisonType);
    }

    public override bool Equals(object obj)
    {
      LiteralPathSegment literalPathSegment = obj as LiteralPathSegment;
      if (literalPathSegment != null && this._comparisonType == literalPathSegment._comparisonType)
        return string.Equals(literalPathSegment.Value, this.Value, this._comparisonType);
      return false;
    }

    public override int GetHashCode()
    {
      return StringComparisonHelper.GetStringComparer(this._comparisonType).GetHashCode(this.Value);
    }
  }
}
