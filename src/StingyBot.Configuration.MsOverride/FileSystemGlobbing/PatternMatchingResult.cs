// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileSystemGlobbing.PatternMatchingResult
// Assembly: Microsoft.Extensions.FileSystemGlobbing, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 872D6D6C-5CAB-4A0C-99EB-7EE50238400F
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileSystemGlobbing.1.1.0\lib\net45\Microsoft.Extensions.FileSystemGlobbing.dll

namespace StingyBot.Configuration.MsOverride.FileSystemGlobbing
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
  /// Represents a collection of <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.FilePatternMatch" />
  /// </summary>
  public class PatternMatchingResult
  {
    /// <summary>
    /// A collection of <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.FilePatternMatch" />
    /// </summary>
    public IEnumerable<FilePatternMatch> Files { get; set; }

    /// <summary>
    /// Gets a value that determines if this instance of <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.PatternMatchingResult" /> has any matches.
    /// </summary>
    public bool HasMatches { get; }

    /// <summary>
    /// Initializes the result with a collection of <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.FilePatternMatch" />
    /// </summary>
    /// <param name="files">A collection of <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.FilePatternMatch" /></param>
    public PatternMatchingResult(IEnumerable<FilePatternMatch> files)
      : this(files, files.Any<FilePatternMatch>())
    {
      this.Files = files;
    }

    /// <summary>
    /// Initializes the result with a collection of <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.FilePatternMatch" />
    /// </summary>
    /// <param name="files">A collection of <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.FilePatternMatch" /></param>
    /// <param name="hasMatches">A value that determines if <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.PatternMatchingResult" /> has any matches.</param>
    public PatternMatchingResult(IEnumerable<FilePatternMatch> files, bool hasMatches)
    {
      this.Files = files;
      this.HasMatches = hasMatches;
    }
  }
}
