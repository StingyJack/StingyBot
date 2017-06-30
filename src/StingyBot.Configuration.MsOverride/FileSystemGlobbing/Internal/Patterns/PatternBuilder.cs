// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns.PatternBuilder
// Assembly: Microsoft.Extensions.FileSystemGlobbing, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 872D6D6C-5CAB-4A0C-99EB-7EE50238400F
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileSystemGlobbing.1.1.0\lib\net45\Microsoft.Extensions.FileSystemGlobbing.dll

namespace StingyBot.Configuration.MsOverride.FileSystemGlobbing.Internal.Patterns
{
    using System;
    using System.Collections.Generic;
    using PathSegments;
    using PatternContexts;

    public class PatternBuilder
  {
    private static readonly char[] _slashes = new char[2]{ '/', '\\' };
    private static readonly char[] _star = new char[1]{ '*' };

    public StringComparison ComparisonType { get; }

    public PatternBuilder()
    {
      this.ComparisonType = StringComparison.OrdinalIgnoreCase;
    }

    public PatternBuilder(StringComparison comparisonType)
    {
      this.ComparisonType = comparisonType;
    }

    public IPattern Build(string pattern)
    {
      if (pattern == null)
        throw new ArgumentNullException("pattern");
      pattern = pattern.TrimStart(PatternBuilder._slashes);
      if (pattern.TrimEnd(PatternBuilder._slashes).Length < pattern.Length)
        pattern = pattern.TrimEnd(PatternBuilder._slashes) + "/**";
      List<IPathSegment> allSegments = new List<IPathSegment>();
      bool flag = true;
      IList<IPathSegment> segmentsPatternStartsWith = (IList<IPathSegment>) null;
      IList<IList<IPathSegment>> segmentsPatternContains = (IList<IList<IPathSegment>>) null;
      IList<IPathSegment> segmentsPatternEndsWith = (IList<IPathSegment>) null;
      int length = pattern.Length;
      int endIndex1;
      for (int beginIndex1 = 0; beginIndex1 < length; beginIndex1 = endIndex1 + 1)
      {
        int index = beginIndex1;
        endIndex1 = PatternBuilder.NextIndex(pattern, PatternBuilder._slashes, beginIndex1, length);
        IPathSegment pathSegment = (IPathSegment) null;
        if (pathSegment == null && endIndex1 - index == 3 && ((int) pattern[index] == 42 && (int) pattern[index + 1] == 46) && (int) pattern[index + 2] == 42)
          index += 2;
        if (pathSegment == null && endIndex1 - index == 2)
        {
          if ((int) pattern[index] == 42 && (int) pattern[index + 1] == 42)
            pathSegment = (IPathSegment) new RecursiveWildcardSegment();
          else if ((int) pattern[index] == 46 && (int) pattern[index + 1] == 46)
          {
            if (!flag)
              throw new ArgumentException("\"..\" can be only added at the beginning of the pattern.");
            pathSegment = (IPathSegment) new ParentPathSegment();
          }
        }
        if (pathSegment == null && endIndex1 - index == 1 && (int) pattern[index] == 46)
          pathSegment = (IPathSegment) new CurrentPathSegment();
        if (pathSegment == null && endIndex1 - index > 2 && ((int) pattern[index] == 42 && (int) pattern[index + 1] == 42) && (int) pattern[index + 2] == 46)
        {
          pathSegment = (IPathSegment) new RecursiveWildcardSegment();
          endIndex1 = index;
        }
        if (pathSegment == null)
        {
          string beginsWith = string.Empty;
          List<string> contains = new List<string>();
          string endsWith = string.Empty;
          int endIndex2;
          for (int beginIndex2 = index; beginIndex2 < endIndex1; beginIndex2 = endIndex2 + 1)
          {
            int beginIndex3 = beginIndex2;
            endIndex2 = PatternBuilder.NextIndex(pattern, PatternBuilder._star, beginIndex2, endIndex1);
            if (beginIndex3 == index)
            {
              if (endIndex2 == endIndex1)
                pathSegment = (IPathSegment) new LiteralPathSegment(PatternBuilder.Portion(pattern, beginIndex3, endIndex2), this.ComparisonType);
              else
                beginsWith = PatternBuilder.Portion(pattern, beginIndex3, endIndex2);
            }
            else if (endIndex2 == endIndex1)
              endsWith = PatternBuilder.Portion(pattern, beginIndex3, endIndex2);
            else if (beginIndex3 != endIndex2)
              contains.Add(PatternBuilder.Portion(pattern, beginIndex3, endIndex2));
          }
          if (pathSegment == null)
            pathSegment = (IPathSegment) new WildcardPathSegment(beginsWith, contains, endsWith, this.ComparisonType);
        }
        if (!(pathSegment is ParentPathSegment))
          flag = false;
        if (!(pathSegment is CurrentPathSegment))
        {
          if (pathSegment is RecursiveWildcardSegment)
          {
            if (segmentsPatternStartsWith == null)
            {
              segmentsPatternStartsWith = (IList<IPathSegment>) new List<IPathSegment>((IEnumerable<IPathSegment>) allSegments);
              segmentsPatternEndsWith = (IList<IPathSegment>) new List<IPathSegment>();
              segmentsPatternContains = (IList<IList<IPathSegment>>) new List<IList<IPathSegment>>();
            }
            else if (segmentsPatternEndsWith.Count != 0)
            {
              segmentsPatternContains.Add(segmentsPatternEndsWith);
              segmentsPatternEndsWith = (IList<IPathSegment>) new List<IPathSegment>();
            }
          }
          else if (segmentsPatternEndsWith != null)
            segmentsPatternEndsWith.Add(pathSegment);
          allSegments.Add(pathSegment);
        }
      }
      if (segmentsPatternStartsWith == null)
        return (IPattern) new PatternBuilder.LinearPattern(allSegments);
      return (IPattern) new PatternBuilder.RaggedPattern(allSegments, segmentsPatternStartsWith, segmentsPatternEndsWith, segmentsPatternContains);
    }

    private static int NextIndex(string pattern, char[] anyOf, int beginIndex, int endIndex)
    {
      int num = pattern.IndexOfAny(anyOf, beginIndex, endIndex - beginIndex);
      if (num != -1)
        return num;
      return endIndex;
    }

    private static string Portion(string pattern, int beginIndex, int endIndex)
    {
      return pattern.Substring(beginIndex, endIndex - beginIndex);
    }

    private class LinearPattern : ILinearPattern, IPattern
    {
      public IList<IPathSegment> Segments { get; }

      public LinearPattern(List<IPathSegment> allSegments)
      {
        this.Segments = (IList<IPathSegment>) allSegments;
      }

      public IPatternContext CreatePatternContextForInclude()
      {
        return (IPatternContext) new PatternContextLinearInclude((ILinearPattern) this);
      }

      public IPatternContext CreatePatternContextForExclude()
      {
        return (IPatternContext) new PatternContextLinearExclude((ILinearPattern) this);
      }
    }

    private class RaggedPattern : IRaggedPattern, IPattern
    {
      public IList<IList<IPathSegment>> Contains { get; }

      public IList<IPathSegment> EndsWith { get; }

      public IList<IPathSegment> Segments { get; }

      public IList<IPathSegment> StartsWith { get; }

      public RaggedPattern(List<IPathSegment> allSegments, IList<IPathSegment> segmentsPatternStartsWith, IList<IPathSegment> segmentsPatternEndsWith, IList<IList<IPathSegment>> segmentsPatternContains)
      {
        this.Segments = (IList<IPathSegment>) allSegments;
        this.StartsWith = segmentsPatternStartsWith;
        this.Contains = segmentsPatternContains;
        this.EndsWith = segmentsPatternEndsWith;
      }

      public IPatternContext CreatePatternContextForInclude()
      {
        return (IPatternContext) new PatternContextRaggedInclude((IRaggedPattern) this);
      }

      public IPatternContext CreatePatternContextForExclude()
      {
        return (IPatternContext) new PatternContextRaggedExclude((IRaggedPattern) this);
      }
    }
  }
}
