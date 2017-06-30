// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileSystemGlobbing.MatcherExtensions
// Assembly: Microsoft.Extensions.FileSystemGlobbing, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 872D6D6C-5CAB-4A0C-99EB-7EE50238400F
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileSystemGlobbing.1.1.0\lib\net45\Microsoft.Extensions.FileSystemGlobbing.dll

namespace StingyBot.Configuration.MsOverride.FileSystemGlobbing
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public static class MatcherExtensions
  {
    /// <summary>
    /// Adds multiple exclude patterns to <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.Matcher" />. <seealso cref="M:StingyBot.Configuration.MsOverride.FileSystemGlobbing.Matcher.AddExclude(System.String)" />
    /// </summary>
    /// <param name="matcher">The matcher to which the exclude patterns are added</param>
    /// <param name="excludePatternsGroups">A list of globbing patterns</param>
    public static void AddExcludePatterns(this Matcher matcher, params IEnumerable<string>[] excludePatternsGroups)
    {
      foreach (IEnumerable<string> excludePatternsGroup in excludePatternsGroups)
      {
        foreach (string pattern in excludePatternsGroup)
          matcher.AddExclude(pattern);
      }
    }

    /// <summary>
    /// Adds multiple patterns to include in <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.Matcher" />. See <seealso cref="M:StingyBot.Configuration.MsOverride.FileSystemGlobbing.Matcher.AddInclude(System.String)" />
    /// </summary>
    /// <param name="matcher">The matcher to which the include patterns are added</param>
    /// <param name="includePatternsGroups">A list of globbing patterns</param>
    public static void AddIncludePatterns(this Matcher matcher, params IEnumerable<string>[] includePatternsGroups)
    {
      foreach (IEnumerable<string> includePatternsGroup in includePatternsGroups)
      {
        foreach (string pattern in includePatternsGroup)
          matcher.AddInclude(pattern);
      }
    }

    /// <summary>
    /// Searches the directory specified for all files matching patterns added to this instance of <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.Matcher" />
    /// </summary>
    /// <param name="matcher">The matcher</param>
    /// <param name="directoryPath">The root directory for the search</param>
    /// <returns>Absolute file paths of all files matched. Empty enumerable if no files matched given patterns.</returns>
    public static IEnumerable<string> GetResultsInFullPath(this Matcher matcher, string directoryPath)
    {
      return (IEnumerable<string>) matcher.Execute((DirectoryInfoBase) new DirectoryInfoWrapper(new DirectoryInfo(directoryPath), false)).Files.Select<FilePatternMatch, string>((Func<FilePatternMatch, string>) (match => Path.GetFullPath(Path.Combine(directoryPath, match.Path)))).ToArray<string>();
    }

    /// <summary>
    /// Matches the file passed in with the patterns in the matcher without going to disk.
    /// </summary>
    /// <param name="matcher">The matcher that holds the patterns and pattern matching type.</param>
    /// <param name="file">The file to run the matcher against.</param>
    /// <returns>The match results.</returns>
    public static PatternMatchingResult Match(this Matcher matcher, string file)
    {
      return matcher.Match(Directory.GetCurrentDirectory(), (IEnumerable<string>) new List<string>()
      {
        file
      });
    }

    /// <summary>
    /// Matches the file passed in with the patterns in the matcher without going to disk.
    /// </summary>
    /// <param name="matcher">The matcher that holds the patterns and pattern matching type.</param>
    /// <param name="rootDir">The root directory for the matcher to match the file from.</param>
    /// <param name="file">The file to run the matcher against.</param>
    /// <returns>The match results.</returns>
    public static PatternMatchingResult Match(this Matcher matcher, string rootDir, string file)
    {
      return matcher.Match(rootDir, (IEnumerable<string>) new List<string>()
      {
        file
      });
    }

    /// <summary>
    /// Matches the files passed in with the patterns in the matcher without going to disk.
    /// </summary>
    /// <param name="matcher">The matcher that holds the patterns and pattern matching type.</param>
    /// <param name="files">The files to run the matcher against.</param>
    /// <returns>The match results.</returns>
    public static PatternMatchingResult Match(this Matcher matcher, IEnumerable<string> files)
    {
      return matcher.Match(Directory.GetCurrentDirectory(), files);
    }

    /// <summary>
    /// Matches the files passed in with the patterns in the matcher without going to disk.
    /// </summary>
    /// <param name="matcher">The matcher that holds the patterns and pattern matching type.</param>
    /// <param name="rootDir">The root directory for the matcher to match the files from.</param>
    /// <param name="files">The files to run the matcher against.</param>
    /// <returns>The match results.</returns>
    public static PatternMatchingResult Match(this Matcher matcher, string rootDir, IEnumerable<string> files)
    {
      if (matcher == null)
        throw new ArgumentNullException("matcher");
      return matcher.Execute((DirectoryInfoBase) new InMemoryDirectoryInfo(rootDir, files));
    }
  }
}
