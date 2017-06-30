// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.IConfigurationRoot
// Assembly: Microsoft.Extensions.Configuration.Abstractions, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 5F64D0E4-986F-4F55-A371-10812112614E
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Configuration.Abstractions.1.1.0\lib\netstandard1.0\Microsoft.Extensions.Configuration.Abstractions.dll

namespace StingyBot.Configuration.MsOverride
{
    /// <summary>
    /// Represents the root of an <see cref="T:Microsoft.Extensions.Configuration.IConfiguration" /> hierarchy.
    /// </summary>
    public interface IConfigurationRoot : IConfiguration
    {
        /// <summary>
        /// Force the configuration values to be reloaded from the underlying <see cref="T:Microsoft.Extensions.Configuration.IConfigurationProvider" />s.
        /// </summary>
        void Reload();
    }
}
