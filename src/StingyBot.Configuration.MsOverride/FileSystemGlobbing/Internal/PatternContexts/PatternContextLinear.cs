// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts.PatternContextLinear
// Assembly: Microsoft.Extensions.FileSystemGlobbing, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 872D6D6C-5CAB-4A0C-99EB-7EE50238400F
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileSystemGlobbing.1.1.0\lib\net45\Microsoft.Extensions.FileSystemGlobbing.dll

namespace StingyBot.Configuration.MsOverride.FileSystemGlobbing.Internal.PatternContexts
{
    using System;
    using System.Collections.Generic;

    public abstract class PatternContextLinear : PatternContext<PatternContextLinear.FrameData>
  {
    protected ILinearPattern Pattern { get; }

    public PatternContextLinear(ILinearPattern pattern)
    {
      this.Pattern = pattern;
    }

    public override PatternTestResult Test(FileInfoBase file)
    {
      if (this.IsStackEmpty())
        throw new InvalidOperationException("Can't test file before entering a directory.");
      if (!this.Frame.IsNotApplicable && this.IsLastSegment() && this.TestMatchingSegment(file.Name))
        return PatternTestResult.Success(this.CalculateStem(file));
      return PatternTestResult.Failed;
    }

    public override void PushDirectory(DirectoryInfoBase directory)
    {
      PatternContextLinear.FrameData frame = this.Frame;
      if (!this.IsStackEmpty() && !this.Frame.IsNotApplicable)
      {
        if (!this.TestMatchingSegment(directory.Name))
        {
          frame.IsNotApplicable = true;
        }
        else
        {
          IPathSegment segment = this.Pattern.Segments[this.Frame.SegmentIndex];
          if (frame.InStem || segment.CanProduceStem)
          {
            frame.InStem = true;
            frame.StemItems.Add(directory.Name);
          }
          frame.SegmentIndex = frame.SegmentIndex + 1;
        }
      }
      this.PushDataFrame(frame);
    }

    protected bool IsLastSegment()
    {
      return this.Frame.SegmentIndex == this.Pattern.Segments.Count - 1;
    }

    protected bool TestMatchingSegment(string value)
    {
      if (this.Frame.SegmentIndex >= this.Pattern.Segments.Count)
        return false;
      return this.Pattern.Segments[this.Frame.SegmentIndex].Match(value);
    }

    protected string CalculateStem(FileInfoBase matchedFile)
    {
      return MatcherContext.CombinePath(this.Frame.Stem, matchedFile.Name);
    }

    public struct FrameData
    {
      public bool IsNotApplicable;
      public int SegmentIndex;
      public bool InStem;
      private IList<string> _stemItems;

      public IList<string> StemItems
      {
        get
        {
          return this._stemItems ?? (this._stemItems = (IList<string>) new List<string>());
        }
      }

      public string Stem
      {
        get
        {
          if (this._stemItems != null)
            return string.Join("/", (IEnumerable<string>) this._stemItems);
          return (string) null;
        }
      }
    }
  }
}
