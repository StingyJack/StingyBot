// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts.PatternContextLinearInclude
// Assembly: Microsoft.Extensions.FileSystemGlobbing, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 872D6D6C-5CAB-4A0C-99EB-7EE50238400F
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileSystemGlobbing.1.1.0\lib\net45\Microsoft.Extensions.FileSystemGlobbing.dll

namespace StingyBot.Configuration.MsOverride.FileSystemGlobbing.Internal.PatternContexts
{
    using System;

    public class PatternContextLinearInclude : PatternContextLinear
  {
    public PatternContextLinearInclude(ILinearPattern pattern)
      : base(pattern)
    {
    }

    public override void Declare(Action<IPathSegment, bool> onDeclare)
    {
      if (this.IsStackEmpty())
        throw new InvalidOperationException("Can't declare path segment before entering a directory.");
      if (this.Frame.IsNotApplicable || this.Frame.SegmentIndex >= this.Pattern.Segments.Count)
        return;
      onDeclare(this.Pattern.Segments[this.Frame.SegmentIndex], this.IsLastSegment());
    }

    public override bool Test(DirectoryInfoBase directory)
    {
      if (this.IsStackEmpty())
        throw new InvalidOperationException("Can't test directory before entering a directory.");
      if (this.Frame.IsNotApplicable || this.IsLastSegment())
        return false;
      return this.TestMatchingSegment(directory.Name);
    }
  }
}
