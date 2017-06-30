// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileSystemGlobbing.Internal.MatcherContext
// Assembly: Microsoft.Extensions.FileSystemGlobbing, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 872D6D6C-5CAB-4A0C-99EB-7EE50238400F
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileSystemGlobbing.1.1.0\lib\net45\Microsoft.Extensions.FileSystemGlobbing.dll

namespace StingyBot.Configuration.MsOverride.FileSystemGlobbing.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PathSegments;
    using Util;

    public class MatcherContext
  {
    private readonly HashSet<LiteralPathSegment> _declaredLiteralFolderSegments = new HashSet<LiteralPathSegment>();
    private readonly HashSet<LiteralPathSegment> _declaredLiteralFileSegments = new HashSet<LiteralPathSegment>();
    private readonly DirectoryInfoBase _root;
    private readonly List<IPatternContext> _includePatternContexts;
    private readonly List<IPatternContext> _excludePatternContexts;
    private readonly List<FilePatternMatch> _files;
    private readonly HashSet<string> _declaredLiteralFolderSegmentInString;
    private bool _declaredParentPathSegment;
    private bool _declaredWildcardPathSegment;
    private readonly StringComparison _comparisonType;

    public MatcherContext(IEnumerable<IPattern> includePatterns, IEnumerable<IPattern> excludePatterns, DirectoryInfoBase directoryInfo, StringComparison comparison)
    {
      this._root = directoryInfo;
      this._files = new List<FilePatternMatch>();
      this._comparisonType = comparison;
      this._includePatternContexts = includePatterns.Select<IPattern, IPatternContext>((Func<IPattern, IPatternContext>) (pattern => pattern.CreatePatternContextForInclude())).ToList<IPatternContext>();
      this._excludePatternContexts = excludePatterns.Select<IPattern, IPatternContext>((Func<IPattern, IPatternContext>) (pattern => pattern.CreatePatternContextForExclude())).ToList<IPatternContext>();
      this._declaredLiteralFolderSegmentInString = new HashSet<string>((IEqualityComparer<string>) StringComparisonHelper.GetStringComparer(comparison));
    }

    public PatternMatchingResult Execute()
    {
      this._files.Clear();
      this.Match(this._root, (string) null);
      return new PatternMatchingResult((IEnumerable<FilePatternMatch>) this._files, this._files.Count > 0);
    }

    private void Match(DirectoryInfoBase directory, string parentRelativePath)
    {
      this.PushDirectory(directory);
      this.Declare();
      List<FileSystemInfoBase> fileSystemInfoBaseList = new List<FileSystemInfoBase>();
      if (this._declaredWildcardPathSegment || this._declaredLiteralFileSegments.Any<LiteralPathSegment>())
      {
        fileSystemInfoBaseList.AddRange(directory.EnumerateFileSystemInfos());
      }
      else
      {
        foreach (DirectoryInfoBase directoryInfoBase in directory.EnumerateFileSystemInfos().OfType<DirectoryInfoBase>())
        {
          if (this._declaredLiteralFolderSegmentInString.Contains(directoryInfoBase.Name))
            fileSystemInfoBaseList.Add((FileSystemInfoBase) directoryInfoBase);
        }
      }
      if (this._declaredParentPathSegment)
        fileSystemInfoBaseList.Add((FileSystemInfoBase) directory.GetDirectory(".."));
      List<DirectoryInfoBase> directoryInfoBaseList = new List<DirectoryInfoBase>();
      foreach (FileSystemInfoBase fileSystemInfoBase in fileSystemInfoBaseList)
      {
        FileInfoBase fileinfo1 = fileSystemInfoBase as FileInfoBase;
        if (fileinfo1 != null)
        {
          PatternTestResult patternTestResult = this.MatchPatternContexts<FileInfoBase>(fileinfo1, (Func<IPatternContext, FileInfoBase, PatternTestResult>) ((pattern, file) => pattern.Test(file)));
          if (patternTestResult.IsSuccessful)
            this._files.Add(new FilePatternMatch(MatcherContext.CombinePath(parentRelativePath, fileinfo1.Name), patternTestResult.Stem));
        }
        else
        {
          DirectoryInfoBase directoryInfoBase = fileSystemInfoBase as DirectoryInfoBase;
          if (directoryInfoBase != null)
          {
            DirectoryInfoBase fileinfo2 = directoryInfoBase;
            Func<IPatternContext, DirectoryInfoBase, bool> func = (Func<IPatternContext, DirectoryInfoBase, bool>) ((pattern, dir) => pattern.Test(dir));
            Func<IPatternContext, DirectoryInfoBase, bool> test = null;
            if (this.MatchPatternContexts<DirectoryInfoBase>(fileinfo2, test))
              directoryInfoBaseList.Add(directoryInfoBase);
          }
        }
      }
      foreach (DirectoryInfoBase directory1 in directoryInfoBaseList)
      {
        string parentRelativePath1 = MatcherContext.CombinePath(parentRelativePath, directory1.Name);
        this.Match(directory1, parentRelativePath1);
      }
      this.PopDirectory();
    }

    private void Declare()
    {
      this._declaredLiteralFileSegments.Clear();
      this._declaredLiteralFolderSegments.Clear();
      this._declaredParentPathSegment = false;
      this._declaredWildcardPathSegment = false;
      foreach (IPatternContext includePatternContext in this._includePatternContexts)
        includePatternContext.Declare(new Action<IPathSegment, bool>(this.DeclareInclude));
    }

    private void DeclareInclude(IPathSegment patternSegment, bool isLastSegment)
    {
      LiteralPathSegment literalPathSegment = patternSegment as LiteralPathSegment;
      if (literalPathSegment != null)
      {
        if (isLastSegment)
        {
          this._declaredLiteralFileSegments.Add(literalPathSegment);
        }
        else
        {
          this._declaredLiteralFolderSegments.Add(literalPathSegment);
          this._declaredLiteralFolderSegmentInString.Add(literalPathSegment.Value);
        }
      }
      else if (patternSegment is ParentPathSegment)
      {
        this._declaredParentPathSegment = true;
      }
      else
      {
        if (!(patternSegment is WildcardPathSegment))
          return;
        this._declaredWildcardPathSegment = true;
      }
    }

    internal static string CombinePath(string left, string right)
    {
      if (string.IsNullOrEmpty(left))
        return right;
      return string.Format("{0}/{1}", (object) left, (object) right);
    }

    private bool MatchPatternContexts<TFileInfoBase>(TFileInfoBase fileinfo, Func<IPatternContext, TFileInfoBase, bool> test)
    {
      return this.MatchPatternContexts<TFileInfoBase>(fileinfo, (Func<IPatternContext, TFileInfoBase, PatternTestResult>) ((ctx, file) =>
      {
        if (test(ctx, file))
          return PatternTestResult.Success(string.Empty);
        return PatternTestResult.Failed;
      })).IsSuccessful;
    }

    private PatternTestResult MatchPatternContexts<TFileInfoBase>(TFileInfoBase fileinfo, Func<IPatternContext, TFileInfoBase, PatternTestResult> test)
    {
      PatternTestResult patternTestResult1 = PatternTestResult.Failed;
      foreach (IPatternContext includePatternContext in this._includePatternContexts)
      {
        PatternTestResult patternTestResult2 = test(includePatternContext, fileinfo);
        if (patternTestResult2.IsSuccessful)
        {
          patternTestResult1 = patternTestResult2;
          break;
        }
      }
      if (!patternTestResult1.IsSuccessful)
        return PatternTestResult.Failed;
      foreach (IPatternContext excludePatternContext in this._excludePatternContexts)
      {
        if (test(excludePatternContext, fileinfo).IsSuccessful)
          return PatternTestResult.Failed;
      }
      return patternTestResult1;
    }

    private void PopDirectory()
    {
      foreach (IPatternContext excludePatternContext in this._excludePatternContexts)
        excludePatternContext.PopDirectory();
      foreach (IPatternContext includePatternContext in this._includePatternContexts)
        includePatternContext.PopDirectory();
    }

    private void PushDirectory(DirectoryInfoBase directory)
    {
      foreach (IPatternContext includePatternContext in this._includePatternContexts)
        includePatternContext.PushDirectory(directory);
      foreach (IPatternContext excludePatternContext in this._excludePatternContexts)
        excludePatternContext.PushDirectory(directory);
    }
  }
}
