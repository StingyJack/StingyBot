// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.Resources
// Assembly: Microsoft.Extensions.Configuration, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 01735326-C363-4F22-987D-01A0C9D21466
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Configuration.1.1.0\lib\netstandard1.1\Microsoft.Extensions.Configuration.dll

namespace StingyBot.Configuration.MsOverride
{
    using System.Reflection;
    using System.Resources;

    internal static class Resources
  {
    private static readonly ResourceManager _resourceManager = new ResourceManager("Microsoft.Extensions.Configuration.Resources", typeof (Resources).GetTypeInfo().Assembly);

    /// <summary>
    /// A configuration provider is not registered. Please register one before setting a value.
    /// </summary>
    internal static string Error_NoSources
    {
      get
      {
        return Resources.GetString("Error_NoSources");
      }
    }

    /// <summary>
    /// A configuration provider is not registered. Please register one before setting a value.
    /// </summary>
    internal static string FormatError_NoSources()
    {
      return Resources.GetString("Error_NoSources");
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
