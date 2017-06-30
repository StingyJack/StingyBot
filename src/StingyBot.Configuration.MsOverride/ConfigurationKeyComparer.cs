// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.ConfigurationKeyComparer
// Assembly: Microsoft.Extensions.Configuration, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 01735326-C363-4F22-987D-01A0C9D21466
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Configuration.1.1.0\lib\netstandard1.1\Microsoft.Extensions.Configuration.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;
    using System.Collections.Generic;

    /// <summary>
  /// IComparer implementation used to order configuration keys.
  /// </summary>
  public class ConfigurationKeyComparer : IComparer<string>
  {
    private static readonly string[] _keyDelimiterArray = new string[1]{ ConfigurationPath.KeyDelimiter };

    /// <summary>The default instance.</summary>
    public static ConfigurationKeyComparer Instance { get; } = new ConfigurationKeyComparer();

    /// <summary>Compares two strings.</summary>
    /// <param name="x">First string.</param>
    /// <param name="y">Second string.</param>
    /// <returns></returns>
    public int Compare(string x, string y)
    {
      string[] strArray1 = (x != null ? x.Split(ConfigurationKeyComparer._keyDelimiterArray, StringSplitOptions.RemoveEmptyEntries) : (string[]) null) ?? new string[0];
      string[] strArray2 = (y != null ? y.Split(ConfigurationKeyComparer._keyDelimiterArray, StringSplitOptions.RemoveEmptyEntries) : (string[]) null) ?? new string[0];
      for (int index = 0; index < Math.Min(strArray1.Length, strArray2.Length); ++index)
      {
        x = strArray1[index];
        y = strArray2[index];
        int result1 = 0;
        int result2 = 0;
        bool flag1 = x != null && int.TryParse(x, out result1);
        bool flag2 = y != null && int.TryParse(y, out result2);
        int num = flag1 || flag2 ? (!(flag1 & flag2) ? (flag1 ? -1 : 1) : result1 - result2) : string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
        if (num != 0)
          return num;
      }
      return strArray1.Length - strArray2.Length;
    }
  }
}
