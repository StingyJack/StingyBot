// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.Json.JsonConfigurationProvider
// Assembly: Microsoft.Extensions.Configuration.Json, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 0EA8CCDB-7A8B-4677-AAD5-4045FDD46D90
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Configuration.Json.1.1.0\lib\net451\Microsoft.Extensions.Configuration.Json.dll

namespace StingyBot.Configuration.MsOverride.Json
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;

    /// <summary>
  /// A JSON file based <see cref="T:Microsoft.Extensions.Configuration.FileConfigurationProvider" />.
  /// </summary>
  public class JsonConfigurationProvider : FileConfigurationProvider
  {
    /// <summary>Initializes a new instance with the specified source.</summary>
    /// <param name="source">The source settings.</param>
    public JsonConfigurationProvider(JsonConfigurationSource source)
      : base((FileConfigurationSource) source)
    {
    }

    /// <summary>Loads the JSON data from a stream.</summary>
    /// <param name="stream">The stream to read.</param>
    public override void Load(Stream stream)
    {
      JsonConfigurationFileParser configurationFileParser = new JsonConfigurationFileParser();
      try
      {
        this.Data = configurationFileParser.Parse(stream);
      }
      catch (JsonReaderException ex)
      {
        string str = string.Empty;
        if (stream.CanSeek)
        {
          stream.Seek(0L, SeekOrigin.Begin);
          using (StreamReader streamReader = new StreamReader(stream))
          {
            IEnumerable<string> fileContent = JsonConfigurationProvider.ReadLines(streamReader);
            str = JsonConfigurationProvider.RetrieveErrorContext(ex, fileContent);
          }
        }
        throw new FormatException(Resources.FormatError_JSONParseError((object) ex.LineNumber, (object) str), (Exception) ex);
      }
    }

    private static string RetrieveErrorContext(JsonReaderException e, IEnumerable<string> fileContent)
    {
      string str = (string) null;
      if (e.LineNumber >= 2)
      {
        List<string> list = fileContent.Skip<string>(e.LineNumber - 2).Take<string>(2).ToList<string>();
        if (list.Count<string>() >= 2)
          str = list[0].Trim() + Environment.NewLine + list[1].Trim();
      }
      if (string.IsNullOrEmpty(str))
        str = fileContent.Skip<string>(e.LineNumber - 1).FirstOrDefault<string>() ?? string.Empty;
      return str;
    }

    private static IEnumerable<string> ReadLines(StreamReader streamReader)
    {
      string line;
      do
      {
        line = streamReader.ReadLine();
        yield return line;
      }
      while (line != null);
    }
  }
}
