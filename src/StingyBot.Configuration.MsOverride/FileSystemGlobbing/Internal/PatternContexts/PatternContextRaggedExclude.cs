// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts.PatternContextRaggedExclude
// Assembly: Microsoft.Extensions.FileSystemGlobbing, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 872D6D6C-5CAB-4A0C-99EB-7EE50238400F
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileSystemGlobbing.1.1.0\lib\net45\Microsoft.Extensions.FileSystemGlobbing.dll

namespace StingyBot.Configuration.MsOverride.FileSystemGlobbing.Internal.PatternContexts
{
    using System;

    public class PatternContextRaggedExclude : PatternContextRagged
  {
    public PatternContextRaggedExclude(IRaggedPattern pattern)
      : base(pattern)
    {
    }

    public override bool Test(DirectoryInfoBase directory)
    {
      if (this.IsStackEmpty())
        throw new InvalidOperationException("Can't test directory before entering a directory.");
      return !this.Frame.IsNotApplicable && (this.IsEndingGroup() && this.TestMatchingGroup((FileSystemInfoBase) directory) || this.Pattern.EndsWith.Count == 0 && this.Frame.SegmentGroupIndex == this.Pattern.Contains.Count - 1 && this.TestMatchingGroup((FileSystemInfoBase) directory));
    }
  }
}
