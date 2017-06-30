// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Primitives.Resources
// Assembly: Microsoft.Extensions.Primitives, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 4B558F24-C6D7-49A0-8D7E-92DB86F8FC53
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Primitives.1.1.0\lib\netstandard1.0\Microsoft.Extensions.Primitives.dll

namespace StingyBot.Configuration.MsOverride.Primitives
{
    using System.ComponentModel;
    using System.Globalization;
    using System.Reflection;
    using System.Resources;
    using System.Runtime.CompilerServices;

    /// <summary>
  ///    A strongly-typed resource class, for looking up localized strings, etc.
  /// </summary>
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    /// <summary>
    ///    Returns the cached ResourceManager instance used by this class.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (Resources.resourceMan == null)
          Resources.resourceMan = new ResourceManager("Microsoft.Extensions.Primitives.Resources", typeof (Resources).GetTypeInfo().Assembly);
        return Resources.resourceMan;
      }
    }

    /// <summary>
    ///    Overrides the current thread's CurrentUICulture property for all
    ///    resource lookups using this strongly typed resource class.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return Resources.resourceCulture;
      }
      set
      {
        Resources.resourceCulture = value;
      }
    }

    /// <summary>
    ///    Looks up a localized string similar to Offset and length are out of bounds for the string or length is greater than the number of characters from index to the end of the string..
    /// </summary>
    internal static string Argument_InvalidOffsetLength
    {
      get
      {
        return Resources.ResourceManager.GetString("Argument_InvalidOffsetLength", Resources.resourceCulture);
      }
    }

    internal Resources()
    {
    }
  }
}
