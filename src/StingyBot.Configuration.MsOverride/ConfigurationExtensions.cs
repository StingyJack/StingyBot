// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.ConfigurationExtensions
// Assembly: Microsoft.Extensions.Configuration.Abstractions, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 5F64D0E4-986F-4F55-A371-10812112614E
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Configuration.Abstractions.1.1.0\lib\netstandard1.0\Microsoft.Extensions.Configuration.Abstractions.dll

namespace StingyBot.Configuration.MsOverride
{
    using System.Collections.Generic;

    /// <summary>
    /// Extension methods for <see cref="T:Microsoft.Extensions.Configuration.IConfiguration" />.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>Shorthand for GetSection("ConnectionStrings")[name].</summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="name">The connection string key.</param>
        /// <returns></returns>
        public static string GetConnectionString(this IConfiguration configuration, string name)
        {
            if (configuration == null)
                return (string)null;
            IConfigurationSection section = configuration.GetSection("ConnectionStrings");
            if (section == null)
                return (string)null;
            string index = name;
            return section[index];
        }

        /// <summary>
        /// Get the enumeration of key value pairs within the <see cref="T:Microsoft.Extensions.Configuration.IConfiguration" />
        /// </summary>
        /// <param name="configuration">The <see cref="T:Microsoft.Extensions.Configuration.IConfiguration" /> to enumerate.</param>
        /// <returns>An enumeration of key value pairs.</returns>
        public static IEnumerable<KeyValuePair<string, string>> AsEnumerable(this IConfiguration configuration)
        {
            return configuration.AsEnumerable(false);
        }

        /// <summary>
        /// Get the enumeration of key value pairs within the <see cref="T:Microsoft.Extensions.Configuration.IConfiguration" />
        /// </summary>
        /// <param name="configuration">The <see cref="T:Microsoft.Extensions.Configuration.IConfiguration" /> to enumerate.</param>
        /// <param name="makePathsRelative">If true, the child keys returned will have the current configuration's Path trimmed from the front.</param>
        /// <returns>An enumeration of key value pairs.</returns>
        public static IEnumerable<KeyValuePair<string, string>> AsEnumerable(this IConfiguration configuration, bool makePathsRelative)
        {
            Stack<IConfiguration> stack = new Stack<IConfiguration>();
            stack.Push(configuration);
            IConfigurationSection configurationSection1 = configuration as IConfigurationSection;
            int prefixLength = !makePathsRelative || configurationSection1 == null ? 0 : configurationSection1.Path.Length + 1;
            while (stack.Count > 0)
            {
                IConfiguration config = stack.Pop();
                IConfigurationSection configurationSection2 = config as IConfigurationSection;
                if (configurationSection2 != null && (!makePathsRelative || config != configuration))
                    yield return new KeyValuePair<string, string>(configurationSection2.Path.Substring(prefixLength), configurationSection2.Value);
                foreach (IConfiguration child in config.GetChildren())
                    stack.Push(child);
                config = (IConfiguration)null;
            }
        }
    }
}
