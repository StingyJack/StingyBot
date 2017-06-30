// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.IConfigurationSection
// Assembly: Microsoft.Extensions.Configuration.Abstractions, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 5F64D0E4-986F-4F55-A371-10812112614E
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Configuration.Abstractions.1.1.0\lib\netstandard1.0\Microsoft.Extensions.Configuration.Abstractions.dll

namespace StingyBot.Configuration.MsOverride
{
    /// <summary>
    /// Represents a section of application configuration values.
    /// </summary>
    public interface IConfigurationSection : IConfiguration
    {
        /// <summary>Gets the key this section occupies in its parent.</summary>
        string Key { get; }

        /// <summary>
        /// Gets the full path to this section within the <see cref="T:Microsoft.Extensions.Configuration.IConfiguration" />.
        /// </summary>
        string Path { get; }

        /// <summary>Gets or sets the section value.</summary>
        string Value { get; set; }
    }
}
