// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.Json.Resources
// Assembly: Microsoft.Extensions.Configuration.Json, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 0EA8CCDB-7A8B-4677-AAD5-4045FDD46D90
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Configuration.Json.1.1.0\lib\net451\Microsoft.Extensions.Configuration.Json.dll

namespace StingyBot.Configuration.MsOverride.Json
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Resources;

    internal static class Resources
  {
    private static readonly ResourceManager _resourceManager = new ResourceManager("Microsoft.Extensions.Configuration.Json.Resources", typeof (Resources).GetTypeInfo().Assembly);

    /// <summary>File path must be a non-empty string.</summary>
    internal static string Error_InvalidFilePath
    {
      get
      {
        return Resources.GetString("Error_InvalidFilePath");
      }
    }

    /// <summary>
    /// Could not parse the JSON file. Error on line number '{0}': '{1}'.
    /// </summary>
    internal static string Error_JSONParseError
    {
      get
      {
        return Resources.GetString("Error_JSONParseError");
      }
    }

    /// <summary>A duplicate key '{0}' was found.</summary>
    internal static string Error_KeyIsDuplicated
    {
      get
      {
        return Resources.GetString("Error_KeyIsDuplicated");
      }
    }

    /// <summary>
    /// Unsupported JSON token '{0}' was found. Path '{1}', line {2} position {3}.
    /// </summary>
    internal static string Error_UnsupportedJSONToken
    {
      get
      {
        return Resources.GetString("Error_UnsupportedJSONToken");
      }
    }

    /// <summary>File path must be a non-empty string.</summary>
    internal static string FormatError_InvalidFilePath()
    {
      return Resources.GetString("Error_InvalidFilePath");
    }

    /// <summary>
    /// Could not parse the JSON file. Error on line number '{0}': '{1}'.
    /// </summary>
    internal static string FormatError_JSONParseError(object p0, object p1)
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, Resources.GetString("Error_JSONParseError"), new object[2]{ p0, p1 });
    }

    /// <summary>A duplicate key '{0}' was found.</summary>
    internal static string FormatError_KeyIsDuplicated(object p0)
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, Resources.GetString("Error_KeyIsDuplicated"), new object[1]{ p0 });
    }

    /// <summary>
    /// Unsupported JSON token '{0}' was found. Path '{1}', line {2} position {3}.
    /// </summary>
    internal static string FormatError_UnsupportedJSONToken(object p0, object p1, object p2, object p3)
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, Resources.GetString("Error_UnsupportedJSONToken"), p0, p1, p2, p3);
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
