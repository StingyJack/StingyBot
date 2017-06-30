// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.FileExtensions.Resources
// Assembly: Microsoft.Extensions.Configuration.FileExtensions, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 818349F0-7064-4187-BABE-750AE8E17D6B
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Configuration.FileExtensions.1.1.0\lib\net451\Microsoft.Extensions.Configuration.FileExtensions.dll

namespace StingyBot.Configuration.MsOverride.FileExtensions
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Resources;

    internal static class Resources
  {
    private static readonly ResourceManager _resourceManager = new ResourceManager("Microsoft.Extensions.Configuration.FileExtensions.Resources", typeof (Resources).GetTypeInfo().Assembly);

    /// <summary>
    /// The configuration file '{0}' was not found and is not optional.
    /// </summary>
    internal static string Error_FileNotFound
    {
      get
      {
        return Resources.GetString("Error_FileNotFound");
      }
    }

    /// <summary>The expected physical path was '{0}'.</summary>
    internal static string Error_ExpectedPhysicalPath
    {
      get
      {
        return Resources.GetString("Error_FileNotFound");
      }
    }

    /// <summary>
    /// The configuration file '{0}' was not found and is not optional.
    /// </summary>
    internal static string FormatError_FileNotFound(object p0)
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, Resources.GetString("Error_FileNotFound"), new object[1]{ p0 });
    }

    /// <summary>The expected physical path was '{0}'.</summary>
    internal static string FormatError_ExpectedPhysicalPath(object p0)
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, Resources.GetString("Error_ExpectedPhysicalPath"), new object[1]{ p0 });
    }

    private static string GetString(string name, params string[] formatterNames)
    {
      string str = Resources._resourceManager.GetString(name);
      if (formatterNames != null)
      {
        for (int index = 0; index < formatterNames.Length; ++index)
          str = str.Replace("{" + formatterNames[index] + "}", "{" + (object) index + "}");
      }
      return str;
    }
  }
}
