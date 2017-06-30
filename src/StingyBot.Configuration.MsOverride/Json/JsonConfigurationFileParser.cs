// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.Json.JsonConfigurationFileParser
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
    using Newtonsoft.Json.Linq;

    internal class JsonConfigurationFileParser
  {
    private readonly IDictionary<string, string> _data = (IDictionary<string, string>) new SortedDictionary<string, string>((IComparer<string>) StringComparer.OrdinalIgnoreCase);
    private readonly Stack<string> _context = new Stack<string>();
    private string _currentPath;
    private JsonTextReader _reader;

    public IDictionary<string, string> Parse(Stream input)
    {
      this._data.Clear();
      this._reader = new JsonTextReader((TextReader) new StreamReader(input));
      this._reader.DateParseHandling = DateParseHandling.None;
      this.VisitJObject(JObject.Load((JsonReader) this._reader));
      return this._data;
    }

    private void VisitJObject(JObject jObject)
    {
      foreach (JProperty property in jObject.Properties())
      {
        this.EnterContext(property.Name);
        this.VisitProperty(property);
        this.ExitContext();
      }
    }

    private void VisitProperty(JProperty property)
    {
      this.VisitToken(property.Value);
    }

    private void VisitToken(JToken token)
    {
      switch (token.Type)
      {
        case JTokenType.Object:
          this.VisitJObject(token.Value<JObject>());
          break;
        case JTokenType.Array:
          this.VisitArray(token.Value<JArray>());
          break;
        case JTokenType.Integer:
        case JTokenType.Float:
        case JTokenType.String:
        case JTokenType.Boolean:
        case JTokenType.Null:
        case JTokenType.Raw:
        case JTokenType.Bytes:
          this.VisitPrimitive(token);
          break;
        default:
          throw new FormatException(Resources.FormatError_UnsupportedJSONToken((object) this._reader.TokenType, (object) this._reader.Path, (object) this._reader.LineNumber, (object) this._reader.LinePosition));
      }
    }

    private void VisitArray(JArray array)
    {
      for (int index = 0; index < array.Count; ++index)
      {
        this.EnterContext(index.ToString());
        this.VisitToken(array[index]);
        this.ExitContext();
      }
    }

    private void VisitPrimitive(JToken data)
    {
      string currentPath = this._currentPath;
      if (this._data.ContainsKey(currentPath))
        throw new FormatException(Resources.FormatError_KeyIsDuplicated((object) currentPath));
      this._data[currentPath] = data.ToString();
    }

    private void EnterContext(string context)
    {
      this._context.Push(context);
      this._currentPath = ConfigurationPath.Combine(this._context.Reverse<string>());
    }

    private void ExitContext()
    {
      this._context.Pop();
      this._currentPath = ConfigurationPath.Combine(this._context.Reverse<string>());
    }
  }
}
