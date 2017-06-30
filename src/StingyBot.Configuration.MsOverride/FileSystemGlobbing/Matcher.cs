// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileSystemGlobbing.Matcher
// Assembly: Microsoft.Extensions.FileSystemGlobbing, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 872D6D6C-5CAB-4A0C-99EB-7EE50238400F
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileSystemGlobbing.1.1.0\lib\net45\Microsoft.Extensions.FileSystemGlobbing.dll

namespace StingyBot.Configuration.MsOverride.FileSystemGlobbing
{
    using System;
    using System.Collections.Generic;
    using Internal;
    using Internal.Patterns;

    /// <summary>
  /// Searches the file system for files with names that match specified patterns.
  /// </summary>
  /// <remarks>
  ///     <para>
  ///     Patterns specified in <seealso cref="M:Microsoft.Extensions.FileSystemGlobbing.Matcher.AddInclude(System.String)" /> and <seealso cref="M:Microsoft.Extensions.FileSystemGlobbing.Matcher.AddExclude(System.String)" /> can use
  ///     the following formats to match multiple files or directories.
  ///     </para>
  ///     <list type="bullet">
  ///         <item>
  ///             <term>
  ///             exact directory and file name
  ///             </term>
  ///             <description>
  ///                 <list type="bullet">
  ///                     <item>
  ///                         <term>"one.txt"</term>
  ///                     </item>
  ///                     <item>
  ///                         <term>"dir/two.txt"</term>
  ///                     </item>
  ///                 </list>
  ///             </description>
  ///         </item>
  ///         <item>
  ///             <term>
  ///             wildcards (*) in file and directory names that represent zero to many characters not including
  ///             directory separators characters
  ///             </term>
  ///             <description>
  ///                 <list type="bullet">
  ///                 <item>
  ///                     <term>"*.txt"</term><description>all files with .txt file extension</description>
  ///                 </item>
  ///                 <item>
  ///                     <term>"*.*"</term><description>all files with an extension</description>
  ///                 </item>
  ///                 <item>
  ///                     <term>"*"</term><description>all files in top level directory</description>
  ///                 </item>
  ///                 <item>
  ///                     <term>".*"</term><description>filenames beginning with '.'</description>
  ///                 </item>
  ///                 - "*word* - all files with 'word' in the filename
  ///                 <item>
  ///                     <term>"readme.*"</term>
  ///                     <description>all files named 'readme' with any file extension</description>
  ///                 </item>
  ///                 <item>
  ///                     <term>"styles/*.css"</term>
  ///                     <description>all files with extension '.css' in the directory 'styles/'</description>
  ///                 </item>
  ///                 <item>
  ///                     <term>"scripts/*/*"</term>
  ///                     <description>all files in 'scripts/' or one level of subdirectory under 'scripts/'</description>
  ///                 </item>
  ///                 <item>
  ///                     <term>"images*/*"</term>
  ///                     <description>all files in a folder with name that is or begins with 'images'</description>
  ///                 </item>
  ///                 </list>
  ///             </description>
  ///         </item>
  ///         <item>
  ///             <term>arbitrary directory depth ("/**/")</term>
  ///             <description>
  ///                 <list type="bullet">
  ///                     <item>
  ///                         <term>"**/*"</term><description>all files in any subdirectory</description>
  ///                     </item>
  ///                     <item>
  ///                         <term>"dir/**/*"</term><description>all files in any subdirectory under 'dir/'</description>
  ///                     </item>
  ///                 </list>
  ///             </description>
  ///         </item>
  ///         <item>
  ///             <term>relative paths</term>
  ///             <description>
  ///             '../shared/*' - all files in a diretory named 'shared' at the sibling level to the base directory given
  ///             to <see cref="M:Microsoft.Extensions.FileSystemGlobbing.Matcher.Execute(Microsoft.Extensions.FileSystemGlobbing.Abstractions.DirectoryInfoBase)" />
  ///             </description>
  ///         </item>
  ///     </list>
  /// </remarks>
  public class Matcher
  {
    private readonly IList<IPattern> _includePatterns = (IList<IPattern>) new List<IPattern>();
    private readonly IList<IPattern> _excludePatterns = (IList<IPattern>) new List<IPattern>();
    private readonly PatternBuilder _builder;
    private readonly StringComparison _comparison;

    /// <summary>
    /// Initializes an instance of <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.Matcher" /> using case-insensitive matching
    /// </summary>
    public Matcher()
      : this(StringComparison.OrdinalIgnoreCase)
    {
    }

    /// <summary>
    /// Initializes an instance of <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.Matcher" /> using the string comparsion method specified
    /// </summary>
    /// <param name="comparisonType">The <see cref="T:System.StringComparison" /> to use</param>
    public Matcher(StringComparison comparisonType)
    {
      this._comparison = comparisonType;
      this._builder = new PatternBuilder(comparisonType);
    }

    /// <summary>
    ///     <para>
    ///     Add a file name pattern that the matcher should use to discover files. Patterns are relative to the root
    ///     directory given when <see cref="M:Microsoft.Extensions.FileSystemGlobbing.Matcher.Execute(Microsoft.Extensions.FileSystemGlobbing.Abstractions.DirectoryInfoBase)" /> is called.
    ///     </para>
    ///     <para>
    ///     Use the forward slash '/' to represent directory separator. Use '*' to represent wildcards in file and
    ///     directory names. Use '**' to represent arbitrary directory depth. Use '..' to represent a parent directory.
    ///     </para>
    /// </summary>
    /// <param name="pattern">The globbing pattern</param>
    /// <returns>The matcher</returns>
    public virtual Matcher AddInclude(string pattern)
    {
      this._includePatterns.Add(this._builder.Build(pattern));
      return this;
    }

    /// <summary>
    ///     <para>
    ///     Add a file name pattern for files the matcher should exclude from the results. Patterns are relative to the
    ///     root directory given when <see cref="M:Microsoft.Extensions.FileSystemGlobbing.Matcher.Execute(Microsoft.Extensions.FileSystemGlobbing.Abstractions.DirectoryInfoBase)" /> is called.
    ///     </para>
    ///     <para>
    ///     Use the forward slash '/' to represent directory separator. Use '*' to represent wildcards in file and
    ///     directory names. Use '**' to represent arbitrary directory depth. Use '..' to represent a parent directory.
    ///     </para>
    /// </summary>
    /// <param name="pattern">The globbing pattern</param>
    /// <returns>The matcher</returns>
    public virtual Matcher AddExclude(string pattern)
    {
      this._excludePatterns.Add(this._builder.Build(pattern));
      return this;
    }

    /// <summary>
    /// Searches the directory specified for all files matching patterns added to this instance of <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.Matcher" />
    /// </summary>
    /// <param name="directoryInfo">The root directory for the search</param>
    /// <returns>Always returns instance of <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.PatternMatchingResult" />, even if not files were matched</returns>
    public virtual PatternMatchingResult Execute(DirectoryInfoBase directoryInfo)
    {
      return new MatcherContext((IEnumerable<IPattern>) this._includePatterns, (IEnumerable<IPattern>) this._excludePatterns, directoryInfo, this._comparison).Execute();
    }
  }
}
