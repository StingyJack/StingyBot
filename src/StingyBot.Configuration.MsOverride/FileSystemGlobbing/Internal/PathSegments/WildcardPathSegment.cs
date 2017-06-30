// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments.WildcardPathSegment
// Assembly: Microsoft.Extensions.FileSystemGlobbing, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 872D6D6C-5CAB-4A0C-99EB-7EE50238400F
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileSystemGlobbing.1.1.0\lib\net45\Microsoft.Extensions.FileSystemGlobbing.dll

namespace StingyBot.Configuration.MsOverride.FileSystemGlobbing.Internal.PathSegments
{
    using System;
    using System.Collections.Generic;

    public class WildcardPathSegment : IPathSegment
  {
    public static readonly WildcardPathSegment MatchAll = new WildcardPathSegment(string.Empty, new List<string>(), string.Empty, StringComparison.OrdinalIgnoreCase);
    private readonly StringComparison _comparisonType;

    public bool CanProduceStem
    {
      get
      {
        return true;
      }
    }

    public string BeginsWith { get; }

    public List<string> Contains { get; }

    public string EndsWith { get; }

    public WildcardPathSegment(string beginsWith, List<string> contains, string endsWith, StringComparison comparisonType)
    {
      this.BeginsWith = beginsWith;
      this.Contains = contains;
      this.EndsWith = endsWith;
      this._comparisonType = comparisonType;
    }

    public bool Match(string value)
    {
      WildcardPathSegment wildcardPathSegment = this;
      if (value.Length < wildcardPathSegment.BeginsWith.Length + wildcardPathSegment.EndsWith.Length || !value.StartsWith(wildcardPathSegment.BeginsWith, this._comparisonType) || !value.EndsWith(wildcardPathSegment.EndsWith, this._comparisonType))
        return false;
      int startIndex = wildcardPathSegment.BeginsWith.Length;
      int num1 = value.Length - wildcardPathSegment.EndsWith.Length;
      for (int index = 0; index != wildcardPathSegment.Contains.Count; ++index)
      {
        string contain = wildcardPathSegment.Contains[index];
        int num2 = value.IndexOf(contain, startIndex, num1 - startIndex, this._comparisonType);
        if (num2 == -1)
          return false;
        startIndex = num2 + contain.Length;
      }
      return true;
    }
  }
}
