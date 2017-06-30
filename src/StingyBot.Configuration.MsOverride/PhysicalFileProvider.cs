// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileProviders.PhysicalFileProvider
// Assembly: Microsoft.Extensions.FileProviders.Physical, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 770B57DD-E4BD-4C5B-8297-BE3C6E5B22A3
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileProviders.Physical.1.1.0\lib\net451\Microsoft.Extensions.FileProviders.Physical.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Internal;
    using Primitives;

    /// <summary>Looks up files using the on-disk file system</summary>
  /// <remarks>
  /// When the environment variable "DOTNET_USE_POLLING_FILE_WATCHER" is set to "1" or "true", calls to
  /// <see cref="M:Microsoft.Extensions.FileProviders.PhysicalFileProvider.Watch(System.String)" /> will use <see cref="T:Microsoft.Extensions.FileProviders.Physical.PollingFileChangeToken" />.
  /// </remarks>
  public class PhysicalFileProvider : IFileProvider, IDisposable
  {
    private static readonly char[] _invalidFileNameChars = ((IEnumerable<char>) Path.GetInvalidFileNameChars()).ToArray();
    private static readonly char[] _invalidFilterChars = ((IEnumerable<char>) PhysicalFileProvider._invalidFileNameChars).ToArray<char>();
    private static readonly char[] _pathSeparators = new char[2]
    {
      Path.DirectorySeparatorChar,
      Path.AltDirectorySeparatorChar
    };
    private const string PollingEnvironmentKey = "DOTNET_USE_POLLING_FILE_WATCHER";
    private readonly PhysicalFilesWatcher _filesWatcher;

    /// <summary>The root directory for this instance.</summary>
    public string Root { get; }

    /// <summary>
    /// Initializes a new instance of a PhysicalFileProvider at the given root directory.
    /// </summary>
    /// <param name="root">The root directory. This should be an absolute path.</param>
    public PhysicalFileProvider(string root)
      : this(root, PhysicalFileProvider.CreateFileWatcher(root))
    {
    }

    internal PhysicalFileProvider(string root, PhysicalFilesWatcher physicalFilesWatcher)
    {
      if (!Path.IsPathRooted(root))
        throw new ArgumentException("The path must be absolute.", "root");
      this.Root = PhysicalFileProvider.EnsureTrailingSlash(Path.GetFullPath(root));
      if (!Directory.Exists(this.Root))
        throw new DirectoryNotFoundException(this.Root);
      this._filesWatcher = physicalFilesWatcher;
    }

    private static PhysicalFilesWatcher CreateFileWatcher(string root)
    {
      string environmentVariable = Environment.GetEnvironmentVariable("DOTNET_USE_POLLING_FILE_WATCHER");
      bool pollForChanges = string.Equals(environmentVariable, "1", StringComparison.Ordinal) || string.Equals(environmentVariable, "true", StringComparison.OrdinalIgnoreCase);
      root = PhysicalFileProvider.EnsureTrailingSlash(Path.GetFullPath(root));
      return new PhysicalFilesWatcher(root, new FileSystemWatcher(root), pollForChanges);
    }

    /// <summary>
    /// Disposes the provider. Change tokens may not trigger after the provider is disposed.
    /// </summary>
    public void Dispose()
    {
      this._filesWatcher.Dispose();
    }

    private string GetFullPath(string path)
    {
      if (this.PathNavigatesAboveRoot(path))
        return (string) null;
      string fullPath;
      try
      {
        fullPath = Path.GetFullPath(Path.Combine(this.Root, path));
      }
      catch
      {
        return (string) null;
      }
      if (!this.IsUnderneathRoot(fullPath))
        return (string) null;
      return fullPath;
    }

    private bool PathNavigatesAboveRoot(string path)
    {
      StringTokenizer stringTokenizer = new StringTokenizer(path, PhysicalFileProvider._pathSeparators);
      int num = 0;
      foreach (StringSegment stringSegment in stringTokenizer)
      {
        if (!stringSegment.Equals(".") && !stringSegment.Equals(""))
        {
          if (stringSegment.Equals(".."))
          {
            --num;
            if (num == -1)
              return true;
          }
          else
            ++num;
        }
      }
      return false;
    }

    private bool IsUnderneathRoot(string fullPath)
    {
      return fullPath.StartsWith(this.Root, StringComparison.OrdinalIgnoreCase);
    }

    private static string EnsureTrailingSlash(string path)
    {
      if (!string.IsNullOrEmpty(path) && (int) path[path.Length - 1] != (int) Path.DirectorySeparatorChar)
        return path + Path.DirectorySeparatorChar.ToString();
      return path;
    }

    private static bool HasInvalidPathChars(string path)
    {
      return path.IndexOfAny(PhysicalFileProvider._invalidFileNameChars) != -1;
    }

    private static bool HasInvalidFilterChars(string path)
    {
      return path.IndexOfAny(PhysicalFileProvider._invalidFilterChars) != -1;
    }

    /// <summary>
    /// Locate a file at the given path by directly mapping path segments to physical directories.
    /// </summary>
    /// <param name="subpath">A path under the root directory</param>
    /// <returns>The file information. Caller must check Exists property. </returns>
    public IFileInfo GetFileInfo(string subpath)
    {
      if (string.IsNullOrEmpty(subpath) || PhysicalFileProvider.HasInvalidPathChars(subpath))
        return (IFileInfo) new NotFoundFileInfo(subpath);
      subpath = subpath.TrimStart(PhysicalFileProvider._pathSeparators);
      if (Path.IsPathRooted(subpath))
        return (IFileInfo) new NotFoundFileInfo(subpath);
      string fullPath = this.GetFullPath(subpath);
      if (fullPath == null)
        return (IFileInfo) new NotFoundFileInfo(subpath);
      FileInfo info = new FileInfo(fullPath);
      if (FileSystemInfoHelper.IsHiddenFile((FileSystemInfo) info))
        return (IFileInfo) new NotFoundFileInfo(subpath);
      return (IFileInfo) new PhysicalFileInfo(info);
    }

    /// <summary>Enumerate a directory at the given path, if any.</summary>
    /// <param name="subpath">A path under the root directory. Leading slashes are ignored.</param>
    /// <returns>
    /// Contents of the directory. Caller must check Exists property. <see cref="T:Microsoft.Extensions.FileProviders.NotFoundDirectoryContents" /> if
    /// <paramref name="subpath" /> is absolute, if the directory does not exist, or <paramref name="subpath" /> has invalid
    /// characters.
    /// </returns>
    public IDirectoryContents GetDirectoryContents(string subpath)
    {
      try
      {
        if (subpath == null || PhysicalFileProvider.HasInvalidPathChars(subpath))
          return (IDirectoryContents) NotFoundDirectoryContents.Singleton;
        subpath = subpath.TrimStart(PhysicalFileProvider._pathSeparators);
        if (Path.IsPathRooted(subpath))
          return (IDirectoryContents) NotFoundDirectoryContents.Singleton;
        string fullPath = this.GetFullPath(subpath);
        if (fullPath == null || !Directory.Exists(fullPath))
          return (IDirectoryContents) NotFoundDirectoryContents.Singleton;
        return (IDirectoryContents) new PhysicalDirectoryContents(fullPath);
      }
      catch (DirectoryNotFoundException )
      {
      }
      catch (IOException )
      {
      }
      return (IDirectoryContents) NotFoundDirectoryContents.Singleton;
    }

    /// <summary>
    ///     <para>Creates a <see cref="T:Microsoft.Extensions.Primitives.IChangeToken" /> for the specified <paramref name="filter" />.</para>
    ///     <para>Globbing patterns are interpreted by <seealso cref="T:Microsoft.Extensions.FileSystemGlobbing.Matcher" />.</para>
    /// </summary>
    /// <param name="filter">
    /// Filter string used to determine what files or folders to monitor. Example: **/*.cs, *.*,
    /// subFolder/**/*.cshtml.
    /// </param>
    /// <returns>
    /// An <see cref="T:Microsoft.Extensions.Primitives.IChangeToken" /> that is notified when a file matching <paramref name="filter" /> is added,
    /// modified or deleted. Returns a <see cref="T:Microsoft.Extensions.FileProviders.NullChangeToken" /> if <paramref name="filter" /> has invalid filter
    /// characters or if <paramref name="filter" /> is an absolute path or outside the root directory specified in the
    /// constructor <seealso cref="M:Microsoft.Extensions.FileProviders.PhysicalFileProvider.#ctor(System.String)" />.
    /// </returns>
    public IChangeToken Watch(string filter)
    {
      if (filter == null || PhysicalFileProvider.HasInvalidFilterChars(filter))
        return (IChangeToken) NullChangeToken.Singleton;
      filter = filter.TrimStart(PhysicalFileProvider._pathSeparators);
      if (Path.IsPathRooted(filter) || this.PathNavigatesAboveRoot(filter))
        return (IChangeToken) NullChangeToken.Singleton;
      return this._filesWatcher.CreateFileChangeToken(filter);
    }
  }
}
