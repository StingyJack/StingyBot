// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileProviders.Physical.PhysicalFilesWatcher
// Assembly: Microsoft.Extensions.FileProviders.Physical, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 770B57DD-E4BD-4C5B-8297-BE3C6E5B22A3
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileProviders.Physical.1.1.0\lib\net451\Microsoft.Extensions.FileProviders.Physical.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Security;
    using System.Threading;
    using System.Threading.Tasks;
    using FileSystemGlobbing;
    using Primitives;

    /// <summary>
    ///     <para>
    ///     A file watcher that watches a physical filesystem for changes.
    ///     </para>
    ///     <para>
    ///     Triggers events on <see cref="T:Microsoft.Extensions.Primitives.IChangeToken" /> when files are created, change, renamed, or deleted.
    ///     </para>
    /// </summary>
    public class PhysicalFilesWatcher : IDisposable
    {
        private readonly ConcurrentDictionary<string, PhysicalFilesWatcher.ChangeTokenInfo> _filePathTokenLookup = new ConcurrentDictionary<string, PhysicalFilesWatcher.ChangeTokenInfo>((IEqualityComparer<string>)StringComparer.OrdinalIgnoreCase);
        private readonly ConcurrentDictionary<string, PhysicalFilesWatcher.ChangeTokenInfo> _wildcardTokenLookup = new ConcurrentDictionary<string, PhysicalFilesWatcher.ChangeTokenInfo>((IEqualityComparer<string>)StringComparer.OrdinalIgnoreCase);
        private readonly object _fileWatcherLock = new object();
        private readonly FileSystemWatcher _fileWatcher;
        private readonly string _root;
        private readonly bool _pollForChanges;

        /// <summary>
        /// Initializes an instance of <see cref="T:StingyBot.Configuration.MsOverride.PhysicalFilesWatcher" /> that watches files in <paramref name="root" />.
        /// Wraps an instance of <see cref="T:System.IO.FileSystemWatcher" />
        /// </summary>
        /// <param name="root">Root directory for the watcher</param>
        /// <param name="fileSystemWatcher">The wrapped watcher that is watching <paramref name="root" /></param>
        /// <param name="pollForChanges">
        /// True when the watcher should use polling to trigger instances of
        /// <see cref="T:Microsoft.Extensions.Primitives.IChangeToken" /> created by <see cref="M:StingyBot.Configuration.MsOverride.PhysicalFilesWatcher.CreateFileChangeToken(System.String)" />
        /// </param>
        public PhysicalFilesWatcher(string root, FileSystemWatcher fileSystemWatcher, bool pollForChanges)
        {
            this._root = root;
            this._fileWatcher = fileSystemWatcher;
            this._fileWatcher.IncludeSubdirectories = true;
            this._fileWatcher.Created += new FileSystemEventHandler(this.OnChanged);
            this._fileWatcher.Changed += new FileSystemEventHandler(this.OnChanged);
            this._fileWatcher.Renamed += new RenamedEventHandler(this.OnRenamed);
            this._fileWatcher.Deleted += new FileSystemEventHandler(this.OnChanged);
            this._fileWatcher.Error += new ErrorEventHandler(this.OnError);
            this._pollForChanges = pollForChanges;
        }

        /// <summary>
        ///     <para>
        ///     Creates an instance of <see cref="T:Microsoft.Extensions.Primitives.IChangeToken" /> for all files and directories that match the
        ///     <paramref name="filter" />
        ///     </para>
        ///     <para>
        ///     Globbing patterns are relative to the root directory given in the constructor
        ///     <seealso cref="M:Microsoft.Extensions.FileProviders.Physical.PhysicalFilesWatcher.#ctor(System.String,System.IO.FileSystemWatcher,System.Boolean)" />. Globbing patterns
        ///     are interpreted by <seealso cref="T:Microsoft.Extensions.FileSystemGlobbing.Matcher" />.
        ///     </para>
        /// </summary>
        /// <param name="filter">A globbing pattern for files and directories to watch</param>
        /// <returns>A change token for all files that match the filter</returns>
        /// <exception cref="T:System.ArgumentNullException">When <paramref name="filter" /> is null</exception>
        public IChangeToken CreateFileChangeToken(string filter)
        {
            if (filter == null)
                throw new ArgumentNullException("filter");
            filter = PhysicalFilesWatcher.NormalizePath(filter);
            IChangeToken orAddChangeToken = this.GetOrAddChangeToken(filter);
            this.TryEnableFileSystemWatcher();
            return orAddChangeToken;
        }

        private IChangeToken GetOrAddChangeToken(string pattern)
        {
            return pattern.IndexOf('*') != -1 || PhysicalFilesWatcher.IsDirectoryPath(pattern) ? this.GetOrAddWildcardChangeToken(pattern) : this.GetOrAddFilePathChangeToken(pattern);
        }

        private IChangeToken GetOrAddFilePathChangeToken(string filePath)
        {
            PhysicalFilesWatcher.ChangeTokenInfo changeTokenInfo;
            if (!this._filePathTokenLookup.TryGetValue(filePath, out changeTokenInfo))
            {
                CancellationTokenSource tokenSource = new CancellationTokenSource();
                CancellationChangeToken changeToken = new CancellationChangeToken(tokenSource.Token);
                changeTokenInfo = new PhysicalFilesWatcher.ChangeTokenInfo(tokenSource, changeToken);
                changeTokenInfo = this._filePathTokenLookup.GetOrAdd(filePath, changeTokenInfo);
            }
            IChangeToken changeToken1 = (IChangeToken)changeTokenInfo.ChangeToken;
            if (this._pollForChanges)
                changeToken1 = (IChangeToken)new CompositeFileChangeToken((IList<IChangeToken>)new IChangeToken[2]
                {
          changeToken1,
          (IChangeToken) new PollingFileChangeToken(new FileInfo(filePath))
                });
            return changeToken1;
        }

        private IChangeToken GetOrAddWildcardChangeToken(string pattern)
        {
            PhysicalFilesWatcher.ChangeTokenInfo changeTokenInfo;
            if (!this._wildcardTokenLookup.TryGetValue(pattern, out changeTokenInfo))
            {
                CancellationTokenSource tokenSource = new CancellationTokenSource();
                CancellationChangeToken changeToken = new CancellationChangeToken(tokenSource.Token);
                Matcher matcher = new Matcher(StringComparison.OrdinalIgnoreCase);
                matcher.AddInclude(pattern);
                changeTokenInfo = new PhysicalFilesWatcher.ChangeTokenInfo(tokenSource, changeToken, matcher);
                changeTokenInfo = this._wildcardTokenLookup.GetOrAdd(pattern, changeTokenInfo);
            }
            IChangeToken changeToken1 = (IChangeToken)changeTokenInfo.ChangeToken;
            if (this._pollForChanges)
                changeToken1 = (IChangeToken)new CompositeFileChangeToken((IList<IChangeToken>)new IChangeToken[2]
                {
          changeToken1,
          (IChangeToken) new PollingWildCardChangeToken(this._root, pattern)
                });
            return changeToken1;
        }

        /// <summary>Disposes the file watcher</summary>
        public void Dispose()
        {
            this._fileWatcher.Dispose();
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            this.OnFileSystemEntryChange(e.OldFullPath);
            this.OnFileSystemEntryChange(e.FullPath);
            if (!Directory.Exists(e.FullPath))
                return;
            try
            {
                foreach (string enumerateFileSystemEntry in Directory.EnumerateFileSystemEntries(e.FullPath, "*", SearchOption.AllDirectories))
                {
                    this.OnFileSystemEntryChange(Path.Combine(e.OldFullPath, enumerateFileSystemEntry.Substring(e.FullPath.Length + 1)));
                    this.OnFileSystemEntryChange(enumerateFileSystemEntry);
                }
            }
            catch (Exception ex) when (ex is IOException || ex is SecurityException || ex is DirectoryNotFoundException || ex is UnauthorizedAccessException)
            {
            }
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            this.OnFileSystemEntryChange(e.FullPath);
        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            foreach (string key in (IEnumerable<string>)this._filePathTokenLookup.Keys)
                this.ReportChangeForMatchedEntries(key);
        }

        private void OnFileSystemEntryChange(string fullPath)
        {
            try
            {
                if (FileSystemInfoHelper.IsHiddenFile((FileSystemInfo)new FileInfo(fullPath)))
                    return;
                this.ReportChangeForMatchedEntries(fullPath.Substring(this._root.Length));
            }
            catch (Exception ex) when (ex is IOException || ex is SecurityException || ex is UnauthorizedAccessException)
            {
            }
        }

        private void ReportChangeForMatchedEntries(string path)
        {
            path = PhysicalFilesWatcher.NormalizePath(path);
            bool flag = false;
            PhysicalFilesWatcher.ChangeTokenInfo matchInfo;
            if (this._filePathTokenLookup.TryRemove(path, out matchInfo))
            {
                PhysicalFilesWatcher.CancelToken(matchInfo);
                flag = true;
            }
            foreach (KeyValuePair<string, PhysicalFilesWatcher.ChangeTokenInfo> keyValuePair in this._wildcardTokenLookup)
            {
                if (keyValuePair.Value.Matcher.Match(path).HasMatches && this._wildcardTokenLookup.TryRemove(keyValuePair.Key, out matchInfo))
                {
                    PhysicalFilesWatcher.CancelToken(matchInfo);
                    flag = true;
                }
            }
            if (!flag)
                return;
            this.TryDisableFileSystemWatcher();
        }

        private void TryDisableFileSystemWatcher()
        {
            lock (this._fileWatcherLock)
            {
                if (!this._filePathTokenLookup.IsEmpty || !this._wildcardTokenLookup.IsEmpty || !this._fileWatcher.EnableRaisingEvents)
                    return;
                this._fileWatcher.EnableRaisingEvents = false;
            }
        }

        private void TryEnableFileSystemWatcher()
        {
            lock (this._fileWatcherLock)
            {
                if (this._filePathTokenLookup.IsEmpty && this._wildcardTokenLookup.IsEmpty || this._fileWatcher.EnableRaisingEvents)
                    return;
                this._fileWatcher.EnableRaisingEvents = true;
            }
        }

        private static string NormalizePath(string filter)
        {
            return filter = filter.Replace('\\', '/');
        }

        private static bool IsDirectoryPath(string path)
        {
            if (path.Length <= 0)
                return false;
            if ((int)path[path.Length - 1] != (int)Path.DirectorySeparatorChar)
                return (int)path[path.Length - 1] == (int)Path.AltDirectorySeparatorChar;
            return true;
        }

        private static void CancelToken(PhysicalFilesWatcher.ChangeTokenInfo matchInfo)
        {
            if (matchInfo.TokenSource.IsCancellationRequested)
                return;
            Task.Run((Action)(() =>
            {
                try
                {
                    matchInfo.TokenSource.Cancel();
                }
                catch
                {
                }
            }));
        }

        private struct ChangeTokenInfo
        {
            public CancellationTokenSource TokenSource { get; }

            public CancellationChangeToken ChangeToken { get; }

            public Matcher Matcher { get; }

            public ChangeTokenInfo(CancellationTokenSource tokenSource, CancellationChangeToken changeToken)
            {
                this = new PhysicalFilesWatcher.ChangeTokenInfo(tokenSource, changeToken, (Matcher)null);
            }

            public ChangeTokenInfo(CancellationTokenSource tokenSource, CancellationChangeToken changeToken, Matcher matcher)
            {
                this.TokenSource = tokenSource;
                this.ChangeToken = changeToken;
                this.Matcher = matcher;
            }
        }
    }
}
