// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileProviders.NotFoundFileInfo
// Assembly: Microsoft.Extensions.FileProviders.Abstractions, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 5221F0E5-502E-4A57-B7FC-EECCEB80C8A4
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileProviders.Abstractions.1.1.0\lib\netstandard1.0\Microsoft.Extensions.FileProviders.Abstractions.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;
    using System.IO;

    /// <summary>Represents a non-existing file.</summary>
  public class NotFoundFileInfo : IFileInfo
  {
    /// <summary>Always false.</summary>
    public bool Exists
    {
      get
      {
        return false;
      }
    }

    /// <summary>Always false.</summary>
    public bool IsDirectory
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    /// Returns <see cref="F:System.DateTimeOffset.MinValue" />.
    /// </summary>
    public DateTimeOffset LastModified
    {
      get
      {
        return DateTimeOffset.MinValue;
      }
    }

    /// <summary>Always equals -1.</summary>
    public long Length
    {
      get
      {
        return -1;
      }
    }

    /// <inheritdoc />
    public string Name { get; }

    /// <summary>Always null.</summary>
    public string PhysicalPath
    {
      get
      {
        return (string) null;
      }
    }

    /// <summary>
    /// Initializes an instance of <see cref="T:StingyBot.Configuration.MsOverride.NotFoundFileInfo" />.
    /// </summary>
    /// <param name="name">The name of the file that could not be found</param>
    public NotFoundFileInfo(string name)
    {
      this.Name = name;
    }

    /// <summary>
    /// Always throws. A stream cannot be created for non-existing file.
    /// </summary>
    /// <exception cref="T:System.IO.FileNotFoundException">Always thrown.</exception>
    /// <returns>Does not return</returns>
    public Stream CreateReadStream()
    {
      throw new FileNotFoundException(string.Format("The file {0} does not exist.", new object[1]{ (object) this.Name }));
    }
  }
}
