// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts.PatternContext`1
// Assembly: Microsoft.Extensions.FileSystemGlobbing, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 872D6D6C-5CAB-4A0C-99EB-7EE50238400F
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileSystemGlobbing.1.1.0\lib\net45\Microsoft.Extensions.FileSystemGlobbing.dll

namespace StingyBot.Configuration.MsOverride.FileSystemGlobbing.Internal.PatternContexts
{
    using System;
    using System.Collections.Generic;

    public abstract class PatternContext<TFrame> : IPatternContext
  {
    private Stack<TFrame> _stack = new Stack<TFrame>();
    protected TFrame Frame;

    public virtual void Declare(Action<IPathSegment, bool> declare)
    {
    }

    public abstract PatternTestResult Test(FileInfoBase file);

    public abstract bool Test(DirectoryInfoBase directory);

    public abstract void PushDirectory(DirectoryInfoBase directory);

    public virtual void PopDirectory()
    {
      this.Frame = this._stack.Pop();
    }

    protected void PushDataFrame(TFrame frame)
    {
      this._stack.Push(this.Frame);
      this.Frame = frame;
    }

    protected bool IsStackEmpty()
    {
      return this._stack.Count == 0;
    }
  }
}
