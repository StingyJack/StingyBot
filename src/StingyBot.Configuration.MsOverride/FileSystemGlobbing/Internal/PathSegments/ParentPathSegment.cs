﻿// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments.ParentPathSegment
// Assembly: Microsoft.Extensions.FileSystemGlobbing, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 872D6D6C-5CAB-4A0C-99EB-7EE50238400F
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileSystemGlobbing.1.1.0\lib\net45\Microsoft.Extensions.FileSystemGlobbing.dll

namespace StingyBot.Configuration.MsOverride.FileSystemGlobbing.Internal.PathSegments
{
    using System;

    public class ParentPathSegment : IPathSegment
  {
    private static readonly string LiteralParent = "..";

    public bool CanProduceStem
    {
      get
      {
        return false;
      }
    }

    public bool Match(string value)
    {
      return string.Equals(ParentPathSegment.LiteralParent, value, StringComparison.Ordinal);
    }
  }
}
