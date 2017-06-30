﻿// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.FileProviders.IFileInfo
// Assembly: Microsoft.Extensions.FileProviders.Abstractions, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 5221F0E5-502E-4A57-B7FC-EECCEB80C8A4
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.FileProviders.Abstractions.1.1.0\lib\netstandard1.0\Microsoft.Extensions.FileProviders.Abstractions.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;
    using System.IO;

    /// <summary>Represents a file in the given file provider.</summary>
  public interface IFileInfo
  {
    /// <summary>
    /// True if resource exists in the underlying storage system.
    /// </summary>
    bool Exists { get; }

    /// <summary>
    /// The length of the file in bytes, or -1 for a directory or non-existing files.
    /// </summary>
    long Length { get; }

    /// <summary>
    /// The path to the file, including the file name. Return null if the file is not directly accessible.
    /// </summary>
    string PhysicalPath { get; }

    /// <summary>
    /// The name of the file or directory, not including any path.
    /// </summary>
    string Name { get; }

    /// <summary>When the file was last modified</summary>
    DateTimeOffset LastModified { get; }

    /// <summary>
    /// True for the case TryGetDirectoryContents has enumerated a sub-directory
    /// </summary>
    bool IsDirectory { get; }

    /// <summary>
    /// Return file contents as readonly stream. Caller should dispose stream when complete.
    /// </summary>
    /// <returns>The file stream</returns>
    Stream CreateReadStream();
  }
}
