// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.Json.JsonConfigurationSource
// Assembly: Microsoft.Extensions.Configuration.Json, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 0EA8CCDB-7A8B-4677-AAD5-4045FDD46D90
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Configuration.Json.1.1.0\lib\net451\Microsoft.Extensions.Configuration.Json.dll

namespace StingyBot.Configuration.MsOverride.Json
{
  /// <summary>
  /// Represents a JSON file as an <see cref="T:Microsoft.Extensions.Configuration.IConfigurationSource" />.
  /// </summary>
  public class JsonConfigurationSource : FileConfigurationSource
  {
    /// <summary>
    /// Builds the <see cref="T:Microsoft.Extensions.Configuration.Json.JsonConfigurationProvider" /> for this source.
    /// </summary>
    /// <param name="builder">The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" />.</param>
    /// <returns>A <see cref="T:Microsoft.Extensions.Configuration.Json.JsonConfigurationProvider" /></returns>
    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
      this.EnsureDefaults(builder);
      return (IConfigurationProvider) new JsonConfigurationProvider(this);
    }
  }
}
