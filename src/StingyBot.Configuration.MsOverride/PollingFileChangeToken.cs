// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileProviders.Physical.PollingFileChangeToken
// Assembly: Microsoft.Extensions.FileProviders.Physical, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 770B57DD-E4BD-4C5B-8297-BE3C6E5B22A3
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileProviders.Physical.1.1.0\lib\net451\Microsoft.Extensions.FileProviders.Physical.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;
    using System.IO;
    using Primitives;

    /// <summary>
  ///     <para>
  ///     A change token that polls for file system changes.
  ///     </para>
  ///     <para>
  ///     This change token does not raise any change callbacks. Callers should watch for <see cref="P:StingyBot.Configuration.MsOverride.PollingFileChangeToken.HasChanged" /> to turn
  ///     from false to true
  ///     and dispose the token after this happens.
  ///     </para>
  /// </summary>
  /// <remarks>Polling occurs every 4 seconds.</remarks>
  public class PollingFileChangeToken : IChangeToken
  {
    private readonly FileInfo _fileInfo;
    private DateTime _previousWriteTimeUtc;
    private DateTime _lastCheckedTimeUtc;
    private bool _hasChanged;

    internal static TimeSpan PollingInterval { get; set; } = TimeSpan.FromSeconds(4.0);

    /// <summary>Always false.</summary>
    public bool ActiveChangeCallbacks
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    /// True when the file has changed since the change token was created. Once the file changes, this value is always true
    /// </summary>
    /// <remarks>
    /// Once true, the value will always be true. Change tokens should not re-used once expired. The caller should discard this
    /// instance once it sees <see cref="P:StingyBot.Configuration.MsOverride.PollingFileChangeToken.HasChanged" /> is true.
    /// </remarks>
    public bool HasChanged
    {
      get
      {
        if (this._hasChanged)
          return this._hasChanged;
        DateTime utcNow = DateTime.UtcNow;
        if (utcNow - this._lastCheckedTimeUtc < PollingFileChangeToken.PollingInterval)
          return this._hasChanged;
        DateTime lastWriteTimeUtc = this.GetLastWriteTimeUtc();
        if (this._previousWriteTimeUtc != lastWriteTimeUtc)
        {
          this._previousWriteTimeUtc = lastWriteTimeUtc;
          this._hasChanged = true;
        }
        this._lastCheckedTimeUtc = utcNow;
        return this._hasChanged;
      }
    }

    /// <summary>
    /// Initializes a new instance of <see cref="T:StingyBot.Configuration.MsOverride.PollingFileChangeToken" /> that polls the specified file for changes as
    /// determined by <see cref="P:System.IO.FileSystemInfo.LastWriteTimeUtc" />.
    /// </summary>
    /// <param name="fileInfo">The <see cref="T:System.IO.FileInfo" /> to poll</param>
    public PollingFileChangeToken(FileInfo fileInfo)
    {
      this._fileInfo = fileInfo;
      this._previousWriteTimeUtc = this.GetLastWriteTimeUtc();
    }

    private DateTime GetLastWriteTimeUtc()
    {
      this._fileInfo.Refresh();
      if (!this._fileInfo.Exists)
        return DateTime.MinValue;
      return this._fileInfo.LastWriteTimeUtc;
    }

    /// <summary>Does not actually register callbacks.</summary>
    /// <param name="callback">This parameter is ignored</param>
    /// <param name="state">This parameter is ignored</param>
    /// <returns>A disposable object that noops when disposed</returns>
    public IDisposable RegisterChangeCallback(Action<object> callback, object state)
    {
      return (IDisposable) EmptyDisposable.Instance;
    }
  }
}
