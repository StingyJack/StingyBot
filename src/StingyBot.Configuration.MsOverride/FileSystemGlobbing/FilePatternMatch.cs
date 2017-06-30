// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileSystemGlobbing.FilePatternMatch
// Assembly: Microsoft.Extensions.FileSystemGlobbing, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 872D6D6C-5CAB-4A0C-99EB-7EE50238400F
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileSystemGlobbing.1.1.0\lib\net45\Microsoft.Extensions.FileSystemGlobbing.dll

namespace StingyBot.Configuration.MsOverride.FileSystemGlobbing
{
    using System;
    using System.Collections.Generic;
    using MsOverride.Internal;

    /// <summary>
  /// Represents a file that was matched by searching using a globbing pattern
  /// </summary>
  public struct FilePatternMatch : IEquatable<FilePatternMatch>
  {
    /// <summary>The path to the file matched</summary>
    /// <remarks>
    /// If the matcher searched for "**/*.cs" using "src/Project" as the directory base and the pattern matcher found
    /// "src/Project/Interfaces/IFile.cs", then Stem = "Interfaces/IFile.cs" and Path = "src/Project/Interfaces/IFile.cs".
    /// </remarks>
    public string Path { get; }

    /// <summary>
    /// The subpath to the matched file under the base directory searched
    /// </summary>
    /// <remarks>
    /// If the matcher searched for "**/*.cs" using "src/Project" as the directory base and the pattern matcher found
    /// "src/Project/Interfaces/IFile.cs",
    /// then Stem = "Interfaces/IFile.cs" and Path = "src/Project/Interfaces/IFile.cs".
    /// </remarks>
    public string Stem { get; }

    /// <summary>
    /// Initializes new instance of <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.FilePatternMatch" />
    /// </summary>
    /// <param name="path">The path to the matched file</param>
    /// <param name="stem">The stem</param>
    public FilePatternMatch(string path, string stem)
    {
      this.Path = path;
      this.Stem = stem;
    }

    /// <summary>
    /// Determines if the specified match is equivalent to the current match using a case-insensitive comparison.
    /// </summary>
    /// <param name="other">The other match to be compared</param>
    /// <returns>True if <see cref="P:StingyBot.Configuration.MsOverride.FileSystemGlobbing.FilePatternMatch.Path" /> and <see cref="P:StingyBot.Configuration.MsOverride.FileSystemGlobbing.FilePatternMatch.Stem" /> are equal using case-insensitive comparison</returns>
    public bool Equals(FilePatternMatch other)
    {
      if (string.Equals(other.Path, this.Path, StringComparison.OrdinalIgnoreCase))
        return string.Equals(other.Stem, this.Stem, StringComparison.OrdinalIgnoreCase);
      return false;
    }

    /// <summary>
    /// Determines if the specified object is equivalent to the current match using a case-insensitive comparison.
    /// </summary>
    /// <param name="obj">The object to be compared</param>
    /// <returns>True when <see cref="M:StingyBot.Configuration.MsOverride.FileSystemGlobbing.FilePatternMatch.Equals(StingyBot.Configuration.MsOverride.FileSystemGlobbing.FilePatternMatch)" /></returns>
    public override bool Equals(object obj)
    {
      return this.Equals((FilePatternMatch) obj);
    }

    /// <summary>Gets a hash for the file pattern match.</summary>
    /// <returns>Some number</returns>
    public override int GetHashCode()
    {
      HashCodeCombiner hashCodeCombiner = HashCodeCombiner.Start();
      hashCodeCombiner.Add<string>(this.Path, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      hashCodeCombiner.Add<string>(this.Stem, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      return (int) hashCodeCombiner;
    }
  }
}
