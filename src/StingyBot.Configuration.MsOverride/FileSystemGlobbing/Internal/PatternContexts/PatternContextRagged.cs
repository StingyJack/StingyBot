// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts.PatternContextRagged
// Assembly: Microsoft.Extensions.FileSystemGlobbing, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 872D6D6C-5CAB-4A0C-99EB-7EE50238400F
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileSystemGlobbing.1.1.0\lib\net45\Microsoft.Extensions.FileSystemGlobbing.dll

namespace StingyBot.Configuration.MsOverride.FileSystemGlobbing.Internal.PatternContexts
{
    using System;
    using System.Collections.Generic;

    public abstract class PatternContextRagged : PatternContext<PatternContextRagged.FrameData>
  {
    protected IRaggedPattern Pattern { get; }

    public PatternContextRagged(IRaggedPattern pattern)
    {
      this.Pattern = pattern;
    }

    public override PatternTestResult Test(FileInfoBase file)
    {
      if (this.IsStackEmpty())
        throw new InvalidOperationException("Can't test file before entering a directory.");
      if (!this.Frame.IsNotApplicable && this.IsEndingGroup() && this.TestMatchingGroup((FileSystemInfoBase) file))
        return PatternTestResult.Success(this.CalculateStem(file));
      return PatternTestResult.Failed;
    }

    public override sealed void PushDirectory(DirectoryInfoBase directory)
    {
      PatternContextRagged.FrameData frame = this.Frame;
      if (this.IsStackEmpty())
      {
        frame.SegmentGroupIndex = -1;
        frame.SegmentGroup = this.Pattern.StartsWith;
      }
      else if (!this.Frame.IsNotApplicable)
      {
        if (this.IsStartingGroup())
        {
          if (!this.TestMatchingSegment(directory.Name))
            frame.IsNotApplicable = true;
          else
            ++frame.SegmentIndex;
        }
        else if (!this.IsStartingGroup() && directory.Name == "..")
          frame.IsNotApplicable = true;
        else if (!this.IsStartingGroup() && !this.IsEndingGroup() && this.TestMatchingGroup((FileSystemInfoBase) directory))
        {
          frame.SegmentIndex = this.Frame.SegmentGroup.Count;
          frame.BacktrackAvailable = 0;
        }
        else
          ++frame.BacktrackAvailable;
      }
      if (frame.InStem)
        frame.StemItems.Add(directory.Name);
      while (frame.SegmentIndex == frame.SegmentGroup.Count && frame.SegmentGroupIndex != this.Pattern.Contains.Count)
      {
        ++frame.SegmentGroupIndex;
        frame.SegmentIndex = 0;
        frame.SegmentGroup = frame.SegmentGroupIndex >= this.Pattern.Contains.Count ? this.Pattern.EndsWith : this.Pattern.Contains[frame.SegmentGroupIndex];
        frame.InStem = true;
      }
      this.PushDataFrame(frame);
    }

    public override void PopDirectory()
    {
      base.PopDirectory();
      if (this.Frame.StemItems.Count <= 0)
        return;
      this.Frame.StemItems.RemoveAt(this.Frame.StemItems.Count - 1);
    }

    protected bool IsStartingGroup()
    {
      return this.Frame.SegmentGroupIndex == -1;
    }

    protected bool IsEndingGroup()
    {
      return this.Frame.SegmentGroupIndex == this.Pattern.Contains.Count;
    }

    protected bool TestMatchingSegment(string value)
    {
      if (this.Frame.SegmentIndex >= this.Frame.SegmentGroup.Count)
        return false;
      return this.Frame.SegmentGroup[this.Frame.SegmentIndex].Match(value);
    }

    protected bool TestMatchingGroup(FileSystemInfoBase value)
    {
      int count = this.Frame.SegmentGroup.Count;
      if (this.Frame.BacktrackAvailable + 1 < count)
        return false;
      FileSystemInfoBase fileSystemInfoBase = value;
      for (int index = 0; index != count; ++index)
      {
        if (!this.Frame.SegmentGroup[count - index - 1].Match(fileSystemInfoBase.Name))
          return false;
        fileSystemInfoBase = (FileSystemInfoBase) fileSystemInfoBase.ParentDirectory;
      }
      return true;
    }

    protected string CalculateStem(FileInfoBase matchedFile)
    {
      return MatcherContext.CombinePath(this.Frame.Stem, matchedFile.Name);
    }

    public struct FrameData
    {
      public bool IsNotApplicable;
      public int SegmentGroupIndex;
      public IList<IPathSegment> SegmentGroup;
      public int BacktrackAvailable;
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
