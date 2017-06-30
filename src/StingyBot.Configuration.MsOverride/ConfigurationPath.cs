// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.ConfigurationPath
// Assembly: Microsoft.Extensions.Configuration.Abstractions, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 5F64D0E4-986F-4F55-A371-10812112614E
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Configuration.Abstractions.1.1.0\lib\netstandard1.0\Microsoft.Extensions.Configuration.Abstractions.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Utility methods and constants for manipulating Configuration paths
    /// </summary>
    public static class ConfigurationPath
    {
        /// <summary>
        /// The delimiter ":" used to separate individual keys in a path.
        /// </summary>
        public static readonly string KeyDelimiter = ":";

        /// <summary>Combines path segments into one path.</summary>
        /// <param name="pathSegments">The path segments to combine.</param>
        /// <returns>The combined path.</returns>
        public static string Combine(params string[] pathSegments)
        {
            if (pathSegments == null)
                throw new ArgumentNullException("pathSegments");
            return string.Join(ConfigurationPath.KeyDelimiter, pathSegments);
        }

        /// <summary>Combines path segments into one path.</summary>
        /// <param name="pathSegments">The path segments to combine.</param>
        /// <returns>The combined path.</returns>
        public static string Combine(IEnumerable<string> pathSegments)
        {
            if (pathSegments == null)
                throw new ArgumentNullException("pathSegments");
            return string.Join(ConfigurationPath.KeyDelimiter, pathSegments);
        }

        /// <summary>Extracts the last path segment from the path.</summary>
        /// <param name="path">The path.</param>
        /// <returns>The last path segment of the path.</returns>
        public static string GetSectionKey(string path)
        {
            if (string.IsNullOrEmpty(path))
                return path;
            int num = path.LastIndexOf(ConfigurationPath.KeyDelimiter, StringComparison.OrdinalIgnoreCase);
            if (num != -1)
                return path.Substring(num + 1);
            return path;
        }

        /// <summary>
        /// Extracts the path corresponding to the parent node for a given path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The original path minus the last individual segment found in it. Null if the original path corresponds to a top level node.</returns>
        public static string GetParentPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return (string)null;
            int length = path.LastIndexOf(ConfigurationPath.KeyDelimiter, StringComparison.OrdinalIgnoreCase);
            if (length != -1)
                return path.Substring(0, length);
            return (string)null;
        }
    }
}
