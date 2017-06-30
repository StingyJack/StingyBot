// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileProviders.Physical.PollingWildCardChangeToken
// Assembly: Microsoft.Extensions.FileProviders.Physical, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 770B57DD-E4BD-4C5B-8297-BE3C6E5B22A3
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileProviders.Physical.1.1.0\lib\net451\Microsoft.Extensions.FileProviders.Physical.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using FileSystemGlobbing;
    using Primitives;

    /// <summary>
  /// A polling based <see cref="T:Microsoft.Extensions.Primitives.IChangeToken" /> for wildcard patterns.
  /// </summary>
  public class PollingWildCardChangeToken : IChangeToken
  {
    internal static readonly TimeSpan DefaultPollingInterval = TimeSpan.FromSeconds(4.0);
    private static readonly byte[] Separator = Encoding.Unicode.GetBytes("|");
    private readonly object _enumerationLock = new object();
    private readonly DirectoryInfoBase _directoryInfo;
    private readonly Matcher _matcher;
    private bool _changed;
    private DateTime? _lastScanTimeUtc;
    private byte[] _byteBuffer;
    private byte[] _previousHash;

    internal TimeSpan PollingInterval { get; set; } = PollingWildCardChangeToken.DefaultPollingInterval;

    /// <inheritdoc />
    public bool ActiveChangeCallbacks
    {
      get
      {
        return false;
      }
    }

    private IClock Clock { get; }

    /// <inheritdoc />
    public bool HasChanged
    {
      get
      {
        if (this._changed)
          return this._changed;
        DateTime utcNow = this.Clock.UtcNow;
        DateTime? lastScanTimeUtc = this._lastScanTimeUtc;
        TimeSpan? nullable = lastScanTimeUtc.HasValue ? new TimeSpan?(utcNow - lastScanTimeUtc.GetValueOrDefault()) : new TimeSpan?();
        TimeSpan pollingInterval = this.PollingInterval;
        if ((nullable.HasValue ? (nullable.GetValueOrDefault() >= pollingInterval ? 1 : 0) : 0) != 0)
        {
          lock (this._enumerationLock)
            this._changed = this.CalculateChanges();
        }
        return this._changed;
      }
    }

    /// <summary>
    /// Initializes a new instance of <see cref="T:StingyBot.Configuration.MsOverride.PollingWildCardChangeToken" />.
    /// </summary>
    /// <param name="root">The root of the file system.</param>
    /// <param name="pattern">The pattern to watch.</param>
    public PollingWildCardChangeToken(string root, string pattern)
      : this((DirectoryInfoBase) new DirectoryInfoWrapper(new DirectoryInfo(root), false), pattern, (IClock) MsOverride.Clock.Instance)
    {
    }

    internal PollingWildCardChangeToken(DirectoryInfoBase directoryInfo, string pattern, IClock clock)
    {
      this._directoryInfo = directoryInfo;
      this.Clock = clock;
      this._matcher = new Matcher(StringComparison.OrdinalIgnoreCase);
      this._matcher.AddInclude(pattern);
      this.CalculateChanges();
    }

    private bool CalculateChanges()
    {
      IEnumerable<FilePatternMatch> files = this._matcher.Execute(this._directoryInfo).Files;
      StringComparer ordinal = StringComparer.Ordinal;
      IOrderedEnumerable<FilePatternMatch> orderedEnumerable = files.OrderBy<FilePatternMatch, string>((Func<FilePatternMatch, string>) (f => f.Path), (IComparer<string>) ordinal);
      using (IncrementalHash sha256 = new IncrementalHash())
      {
        foreach (FilePatternMatch filePatternMatch in (IEnumerable<FilePatternMatch>) orderedEnumerable)
        {
          DateTime lastWriteUtc = this.GetLastWriteUtc(filePatternMatch.Path);
          if (this._lastScanTimeUtc.HasValue)
          {
            DateTime? lastScanTimeUtc = this._lastScanTimeUtc;
            DateTime dateTime = lastWriteUtc;
            if ((lastScanTimeUtc.HasValue ? (lastScanTimeUtc.GetValueOrDefault() < dateTime ? 1 : 0) : 0) != 0)
              return true;
          }
          this.ComputeHash(sha256, filePatternMatch.Path, lastWriteUtc);
        }
        byte[] hashAndReset = sha256.GetHashAndReset();
        if (!PollingWildCardChangeToken.ArrayEquals(this._previousHash, hashAndReset))
          return true;
        this._previousHash = hashAndReset;
        this._lastScanTimeUtc = new DateTime?(this.Clock.UtcNow);
      }
      return false;
    }

    /// <summary>
    /// Gets the last write time of the file at the specified <paramref name="path" />.
    /// </summary>
    /// <param name="path">The root relative path.</param>
    /// <returns>The <see cref="T:System.DateTime" /> that the file was last modified.</returns>
    protected virtual DateTime GetLastWriteUtc(string path)
    {
      return File.GetLastWriteTimeUtc(Path.Combine(this._directoryInfo.FullName, path));
    }

    private static bool ArrayEquals(byte[] previousHash, byte[] currentHash)
    {
      if (previousHash == null)
        return true;
      for (int index = 0; index < previousHash.Length; ++index)
      {
        if ((int) previousHash[index] != (int) currentHash[index])
          return false;
      }
      return true;
    }

    private unsafe void ComputeHash(IncrementalHash sha256, string path, DateTime lastChangedUtc)
    {
      int byteCount = Encoding.Unicode.GetByteCount(path);
      if (this._byteBuffer == null || byteCount > this._byteBuffer.Length)
        this._byteBuffer = new byte[Math.Max(byteCount, 256)];
      int bytes = Encoding.Unicode.GetBytes(path, 0, path.Length, this._byteBuffer, 0);
      sha256.AppendData(this._byteBuffer, 0, bytes);
      sha256.AppendData(PollingWildCardChangeToken.Separator, 0, PollingWildCardChangeToken.Separator.Length);
      fixed (byte* numPtr = this._byteBuffer)
        *(long*) numPtr = lastChangedUtc.Ticks;
      sha256.AppendData(this._byteBuffer, 0, 8);
      sha256.AppendData(PollingWildCardChangeToken.Separator, 0, PollingWildCardChangeToken.Separator.Length);
    }

    IDisposable IChangeToken.RegisterChangeCallback(Action<object> callback, object state)
    {
      return (IDisposable) EmptyDisposable.Instance;
    }
  }
}
