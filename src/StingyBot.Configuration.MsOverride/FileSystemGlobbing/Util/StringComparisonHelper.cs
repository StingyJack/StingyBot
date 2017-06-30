// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileSystemGlobbing.Util.StringComparisonHelper
// Assembly: Microsoft.Extensions.FileSystemGlobbing, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 872D6D6C-5CAB-4A0C-99EB-7EE50238400F
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileSystemGlobbing.1.1.0\lib\net45\Microsoft.Extensions.FileSystemGlobbing.dll

namespace StingyBot.Configuration.MsOverride.FileSystemGlobbing.Util
{
    using System;

    internal static class StringComparisonHelper
  {
    public static StringComparer GetStringComparer(StringComparison comparisonType)
    {
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
          return StringComparer.CurrentCulture;
        case StringComparison.CurrentCultureIgnoreCase:
          return StringComparer.CurrentCultureIgnoreCase;
        case StringComparison.InvariantCulture:
          return StringComparer.InvariantCulture;
        case StringComparison.InvariantCultureIgnoreCase:
          return StringComparer.InvariantCultureIgnoreCase;
        case StringComparison.Ordinal:
          return StringComparer.Ordinal;
        case StringComparison.OrdinalIgnoreCase:
          return StringComparer.OrdinalIgnoreCase;
        default:
          throw new InvalidOperationException(string.Format("Unexpected StringComparison type: {0}", (object) comparisonType));
      }
    }
  }
}
