// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileSystemGlobbing.InMemoryDirectoryInfo
// Assembly: Microsoft.Extensions.FileSystemGlobbing, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 872D6D6C-5CAB-4A0C-99EB-7EE50238400F
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileSystemGlobbing.1.1.0\lib\net45\Microsoft.Extensions.FileSystemGlobbing.dll

namespace StingyBot.Configuration.MsOverride.FileSystemGlobbing
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Internal;

    /// <summary>Avoids using disk for uses like Pattern Matching.</summary>
  public class InMemoryDirectoryInfo : DirectoryInfoBase
  {
    private static readonly char[] DirectorySeparators = new char[2]{ Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };
    private readonly IEnumerable<string> _files;

    /// <inheritdoc />
    public override string FullName { get; }

    /// <inheritdoc />
    public override string Name { get; }

    /// <inheritdoc />
    public override DirectoryInfoBase ParentDirectory
    {
      get
      {
        return (DirectoryInfoBase) new InMemoryDirectoryInfo(Path.GetDirectoryName(this.FullName), this._files, true);
      }
    }

    /// <summary>
    /// Creates a new InMemoryDirectoryInfo with the root directory and files given.
    /// </summary>
    /// <param name="rootDir">The root directory that this FileSystem will use.</param>
    /// <param name="files">Collection of file names. If relative paths <paramref name="rootDir" /> will be prepended to the paths.</param>
    public InMemoryDirectoryInfo(string rootDir, IEnumerable<string> files)
      : this(rootDir, files, false)
    {
    }

    private InMemoryDirectoryInfo(string rootDir, IEnumerable<string> files, bool normalized)
    {
      if (string.IsNullOrEmpty(rootDir))
        throw new ArgumentNullException("rootDir");
      if (files == null)
        files = (IEnumerable<string>) new List<string>();
      this.Name = Path.GetFileName(rootDir);
      if (normalized)
      {
        this._files = files;
        this.FullName = rootDir;
      }
      else
      {
        List<string> stringList = new List<string>(files.Count<string>());
        foreach (string file in files)
          stringList.Add(Path.GetFullPath(file.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar)));
        this._files = (IEnumerable<string>) stringList;
        this.FullName = Path.GetFullPath(rootDir.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar));
      }
    }

    /// <inheritdoc />
    public override IEnumerable<FileSystemInfoBase> EnumerateFileSystemInfos()
    {
      Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
      foreach (string file1 in this._files)
      {
        string file = file1;
        if (this.IsRootDirectory(this.FullName, file))
        {
          int length = file.Length;
          int startIndex = this.FullName.Length + 1;
          int endSegment = file.IndexOfAny(InMemoryDirectoryInfo.DirectorySeparators, startIndex, length - startIndex);
          if (endSegment == -1)
          {
            yield return (FileSystemInfoBase) new InMemoryFileInfo(file, this);
          }
          else
          {
            string key = file.Substring(0, endSegment);
            List<string> stringList;
            if (!dict.TryGetValue(key, out stringList))
              dict[key] = new List<string>() { file };
            else
              stringList.Add(file);
          }
          file = (string) null;
        }
      }
      foreach (KeyValuePair<string, List<string>> keyValuePair in dict)
        yield return (FileSystemInfoBase) new InMemoryDirectoryInfo(keyValuePair.Key, (IEnumerable<string>) keyValuePair.Value, true);
      new Dictionary<string, List<string>>.Enumerator();
    }

    private bool IsRootDirectory(string rootDir, string filePath)
    {
      return filePath.StartsWith(rootDir, StringComparison.Ordinal) && filePath.IndexOf(Path.DirectorySeparatorChar, rootDir.Length) == rootDir.Length;
    }

    /// <inheritdoc />
    public override DirectoryInfoBase GetDirectory(string path)
    {
      if (string.Equals(path, "..", StringComparison.Ordinal))
        return (DirectoryInfoBase) new InMemoryDirectoryInfo(Path.Combine(this.FullName, path), this._files, true);
      return (DirectoryInfoBase) new InMemoryDirectoryInfo(Path.GetFullPath(path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar)), this._files, true);
    }

    /// <summary>
    /// Returns an instance of <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.Abstractions.FileInfoBase" /> that matches the <paramref name="path" /> given.
    /// </summary>
    /// <param name="path">The filename.</param>
    /// <returns>Instance of <see cref="T:StingyBot.Configuration.MsOverride.FileSystemGlobbing.Abstractions.FileInfoBase" /> if the file exists, null otherwise.</returns>
    public override FileInfoBase GetFile(string path)
    {
      string fullPath = Path.GetFullPath(path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar));
      foreach (string file in this._files)
      {
        if (string.Equals(file, fullPath))
          return (FileInfoBase) new InMemoryFileInfo(file, this);
      }
      return (FileInfoBase) null;
    }
  }
}
