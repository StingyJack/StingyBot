#load "OutputHelpers.csx"
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;



#region "util functions lifted into the global scope"

/* disabled for scripty, uncomment if rename to .cs file
 
namespace StingyBot.Common.SlackApi
{
    public static class UtilityFunctions
    {
*/

public static Type GuessType(this Newtonsoft.Json.Linq.JValue slackType, List<TypeFile> generatedTypes)
{
    //clone in ApiGen.Scripty.csx
    if (slackType == null)
    {
        return typeof(string);
    }
    //Failed to add api method : Cannot access child value on Newtonsoft.Json.Linq.JValue.
    var typeName = slackType.Value.GetType().Name;
    var matches = generatedTypes.FirstOrDefault(g => g.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase));
    if (matches != null)
    {
        return Type.GetType(matches.Name);
    }
    return typeof(object);
}

public static string GetCSharpTypeNameFromJTokenValue(string key, Newtonsoft.Json.Linq.JToken jToken, List<TypeFile> generatedTypes)
{
       
    var tokenType = jToken.Type.ToString();
    switch (tokenType)
    {
        case "Array":
            if (key.EndsWith("s"))
            {
                var typeName = key.Substring(0, key.Length - 1);

                var match = generatedTypes.FirstOrDefault(f => f.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase));
                if (match != null)
                {
                    return $"List<{match.Name.TCase()}>";
                }
            }
            return $"object[]";
        case "Integer":
            return "int";
        case "Float":
            return "decimal";
        case "String":
            return "string";
        case "Boolean":
            return "bool";
        case "Date":
            return "DateTime";
        case "Bytes":
            return "byte[]";
        case "Guid":
            return "Guid";
        case "Uri":
            return "Uri";
        case "TimeSpan":
            return "TimeSpan";
        case "Object":
            return "object";
        default:
            return "string";
    }
}

public static CompReturn GetPossibleJsonFromFile(string fileName)
{
    var returnValue = new CompReturn();
    if (System.IO.File.Exists(fileName) == false)
    {
        returnValue.Errors.Add("File doesnt exist");
        return returnValue;
    }
    try
    {
        var fileLines = System.IO.File.ReadAllLines(fileName);
        var jsonLines = new List<string>();
        foreach (var fileLine in fileLines)
        {
            var trimmed = fileLine.Trim();
            if (trimmed.StartsWith("{", StringComparison.OrdinalIgnoreCase)
            || trimmed.StartsWith("\"", StringComparison.OrdinalIgnoreCase)
            || trimmed.StartsWith("}", StringComparison.OrdinalIgnoreCase))
            {
                jsonLines.Add(trimmed.RemoveLineEndings());
            }
        }
        returnValue.Result = jsonLines;
    }
    catch (Exception ex)
    {
        returnValue.Errors.Add(ex.ToString());
    }
    return returnValue;
}

public static string CleanJunkFromJson(string json)
{
    // success raw - {ARGS},{,"ok": true,},{,"ok": true,,"no_op": true,,"already_open": true,},{ERRORS},{WARNINGS}
    // success cleaned - {,"ok": true,},{,"ok": true,,"no_op": true,,"already_open": true,}
    // success cleaned ApiTest - {"ok": true,"args": {"foo": "bar"}},{"ok": false,"error": "my_error","args": {"error": "my_error"}}
    // fail  parse ApiTest - Newtonsoft.Json.JsonReaderException: Additional text encountered after finished reading JSON content: ,. Path '', line 1, position 35.   at Newtonsoft.Json.JsonTextReader.ReadInternal()   at Newtonsoft.Json.JsonTextReader.Read()   at Newtonsoft.Json.Linq.JObject.Parse(String json)   at Submission#0.ParseJobj(String json)
    // fail  value ApiTest - {"ok": true,"args": {"foo": "bar"}},{"ok": false,"error": "my_error","args": {"error": "my_error"}}
    // tried harder and failed 
    // debug // parse retry - {"ok": true,"args": {"foo": "bar"}
    var knownJunk = new List<Tuple<string, string>>
    {
        new Tuple<string, string> ( "{ARGS},", string.Empty ),
        new Tuple<string, string> (",{ERRORS}", string.Empty),
        new Tuple<string, string> (",{WARNINGS}", string.Empty ),
        new Tuple<string, string> ("[,", "["),
        new Tuple<string, string> ("{,", "{" ),
        new Tuple<string, string> (",}", "}" ),
        new Tuple<string, string> (",,", ","),
        new Tuple<string, string> ("},}", "}]}"),
        new Tuple<string, string> ("....",string.Empty),
        new Tuple<string, string> ("\u2026", string.Empty)
    };
    var replace = json.Trim();

    foreach (var kj in knownJunk)
    {
        replace = replace.Replace(kj.Item1, kj.Item2);
    }

    if (replace.EndsWith("},}", StringComparison.OrdinalIgnoreCase))
    {
        replace = replace.Substring(0, replace.Length - 3) + "}}";
    }
    return replace;
}

public static CompReturn ParseJobj(string json)
{
    try
    {
        var jobject = JObject.Parse(json);
        return new CompReturn { ParsedObject = jobject };
    }
    catch (Exception ex)
    {
        var cr = new CompReturn();
        cr.Errors.Add(ex.ToString());
        return cr;
    }
}


public static CompReturn ParseJobjButTryHarder(string json)
{
    var cr = new CompReturn();

    try
    {
        var numOpeningBraces = json.Length - json.Replace("{", string.Empty).Length;
        var numClosingBraces = json.Length - json.Replace("}", string.Empty).Length;
        string jsonBraceAdded = null;
        if (numClosingBraces < numOpeningBraces)
        {
            jsonBraceAdded = json + new string('}', numOpeningBraces - numClosingBraces);
        }
        if (numClosingBraces > numOpeningBraces)
        {
            jsonBraceAdded = json + new string('{', numClosingBraces - numOpeningBraces);
        }
        cr.DebugLog.Add($"// beginning 1st attempt with {json}");
        if (string.IsNullOrWhiteSpace(jsonBraceAdded) == false)
        {
            cr.DebugLog.Add($"added braces and attempted parse of {jsonBraceAdded}");
            var braceParse = ParseJobj(jsonBraceAdded);
            if (braceParse.ParsedObject == null)
            {
                cr.Errors.AddRange(braceParse.Errors);

            }
            else
            {
                cr.ParsedObject = braceParse.ParsedObject;
            }
        }
        else
        {
            cr.DebugLog.Add($"// no braces added");
        }

        cr.DebugLog.Add($"// beginning 2rd attempt with {json}");
        var start = 0;
        var indexOfSplit = json.IndexOf("},{");
        while (indexOfSplit >= 0)
        {
            var chunk = json.Substring(start, indexOfSplit);
            cr.DebugLog.Add($"// parse retry - {chunk}");
            var parseAttempt = ParseJobj(chunk);
            if (parseAttempt.ParsedObject != null)
            {
                cr.ParsedObject = parseAttempt.ParsedObject;
                cr.DebugLog.Add($"// parse SUCCESS");
                return cr;
            }
            foreach (var err in parseAttempt.Errors)
            {
                cr.DebugLog.Add($"// parse failed - {err}");
            }

            start = indexOfSplit;
            indexOfSplit = json.IndexOf("},{", start + 2);
        }

        int? bracePairCount = null;
        int? startingBraceIndex = null;
        int? endingBraceIndex = null;
        cr.DebugLog.Add($"// beginning 3rd sweep with {json}");
        for (int i = 0; i < json.Length; i++)
        {
            if (startingBraceIndex.HasValue && endingBraceIndex.HasValue)
            {
                var fragment = json.Substring(startingBraceIndex.Value, ((endingBraceIndex.Value - startingBraceIndex.Value) + 1));
                cr.DebugLog.Add($"// attempting fragment at index {i} {startingBraceIndex.Value} {endingBraceIndex.Value} {fragment}");
                var parseAttempt = ParseJobj(fragment);
                if (parseAttempt.ParsedObject != null)
                {
                    cr.ParsedObject = parseAttempt.ParsedObject;
                    cr.DebugLog.Add($"// parse SUCCESS");
                    return cr;
                }
                foreach (var err in parseAttempt.Errors)
                {
                    cr.DebugLog.Add($"// parse failed - {err}");
                }
            }

            var c = json[i];
            if (c == '{' && bracePairCount.HasValue == false)
            {
                startingBraceIndex = i;
                bracePairCount = 1;
                cr.DebugLog.Add($"// parse found first opening brace at {i} count {bracePairCount}");
                continue;
            }

            if (c == '{' && bracePairCount.HasValue == true)
            {
                bracePairCount++;
                cr.DebugLog.Add($"// parse found next opening brace at {i}. count {bracePairCount}");
                continue;
            }

            if (c == '}' && bracePairCount.HasValue == true)
            {
                bracePairCount--;
                cr.DebugLog.Add($"// parse found closing brace at {i}. count {bracePairCount}");
                if (bracePairCount == 0)
                {
                    cr.DebugLog.Add($"// parse found last closing brace at {i}. count {bracePairCount}");
                    endingBraceIndex = i;
                }
                continue;
            }

        }
        if (startingBraceIndex.HasValue && endingBraceIndex.HasValue)
        {
            var fragment = json.Substring(startingBraceIndex.Value, ((endingBraceIndex.Value - startingBraceIndex.Value) + 1));
            cr.DebugLog.Add($"// last attempt fragment at final index {startingBraceIndex.Value} {endingBraceIndex.Value} {fragment}");
            var parseAttempt = ParseJobj(fragment);
            if (parseAttempt.ParsedObject != null)
            {
                cr.ParsedObject = parseAttempt.ParsedObject;
                cr.DebugLog.Add($"// parse SUCCESS");
                return cr;
            }
            foreach (var err in parseAttempt.Errors)
            {
                cr.DebugLog.Add($"// parse failed - {err}");
            }
        }


    }
    catch (Exception ex)
    {
        cr.Errors.Add(ex.ToString());
    }
    return cr;
}

public class CompReturn
{
    public List<string> Errors { get; set; } = new List<string>();
    public List<string> Result { get; set; } = new List<string>();
    public JObject ParsedObject { get; set; }
    public List<string> DebugLog { get; set; } = new List<string>();
}


//}


#endregion // #region "util functions lifted into the global scope"

#region "api method call classes"

public abstract class ApiBase
{
    public CompReturn GenerationErrs { get; set; } = new CompReturn();
    public static List<string> ExtractDebug { get; set; } = new List<string>();
}

public class ApiMethod : ApiBase
{

    public string MethodName { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public List<ApiProp> Args { get; set; } = new List<ApiProp>();
    public List<Err> Errors { get; set; } = new List<Err>();
    public ApiResponse Response { get; set; }

    public ApiMethod()
    {

    }

    public ApiMethod(string fileName, string json, List<TypeFile> generatedTypes)
    {
        var methodName = fileName.Replace(".json", string.Empty);
        MethodName = methodName;

        Name = methodName.TCase().Replace(".", string.Empty);

        var jobject = JObject.Parse(json);
        var descrNode = jobject["desc"];
        if (descrNode != null)
        {
            Desc = descrNode.Value<string>();
        }

        var argsNode = jobject["args"];
        if (argsNode != null)
        {
            foreach (var gtype in generatedTypes)
            {
                ExtractDebug.Add($"available generated type {gtype.Name}");
            }
            foreach (var arg in argsNode)
            {
                ExtractDebug.Add($" - arg {arg}");
                var jprop = ((JProperty)arg);
                var ApiProp = new ApiProp();
                ApiProp.OriginalName = jprop.Name;
                ApiProp.Name = jprop.Name.TCase().Replace("_", string.Empty);
                ApiProp.Desc = (string)jprop.Value["desc"];
                ApiProp.Example = (string)jprop.Value["example"];
                ExtractDebug.Add($" - arg name {ApiProp.Name}, argDesc {ApiProp.Desc}");
                if (jprop.Value["default"] != null)
                {
                    ApiProp.DefaultValue = (string)jprop.Value["default"];
                }
                if (jprop.Value["required"] != null)
                {
                    ApiProp.Required = (bool)jprop.Value["required"];
                }
                ExtractDebug.Add($" - arg default {ApiProp.DefaultValue}, required {ApiProp.Required}");
                ExtractDebug.Add($" - arg type {jprop.Value["type"]}");
                if (jprop.Value["type"] != null)
                {
                    ApiProp.Type = ((JValue)jprop.Value["type"]).GuessType(generatedTypes);
                }
                else
                {
                    ApiProp.Type = typeof(string);
                }
                Args.Add(ApiProp);

            }
        }

        var errsNode = jobject["errors"];
        if (errsNode != null)
        {
            foreach (var child in errsNode)
            {
                var err = new Err();
                Errors.Add(err);
            }
        }
    }

    public override string ToString()
    {
        var errs = "none";
        if (Errors != null && Errors.Count > 0)
        {
            var errahs = Errors.Select(e => $"{e.Code}:{e.Message}").ToList();
            errs = string.Join(",", errahs);
        }
        return $"{Desc} - {errs}";
    }
}

public class ApiResponse : ApiBase
{
    public List<ApiProp> Properties { get; set; } = new List<ApiProp>();
}

public class ApiProp : ApiBase
{
    public string Name { get; set; }
    public System.Type Type { get; set; }
    public string TypeName { get; set; }
    public bool? Required { get; set; }
    public string Desc { get; set; }
    public string Example { get; set; }
    public string OriginalName { get; set; }
    public string DefaultValue { get; set; }

    public ApiProp()
    {

    }

    public ApiProp(JProperty jprop, List<TypeFile> generatedTypes)
    {
        OriginalName = jprop.Name;
        Name = jprop.Name.TCase().Replace("_", string.Empty);
        Desc = (string)jprop.Value["desc"];
        Example = (string)jprop.Value["example"];
        ExtractDebug.Add($" - arg name {Name}, argDesc {Desc}");
        if (jprop.Value["default"] != null)
        {
            DefaultValue = (string)jprop.Value["default"];
        }
        if (jprop.Value["required"] != null)
        {
            Required = (bool)jprop.Value["required"];
        }
        ExtractDebug.Add($" - arg default {DefaultValue}, required {Required}");
        ExtractDebug.Add($" - arg type {jprop.Value["type"]}");
        if (jprop.Value["type"] != null)
        {
            Type = ((JValue)jprop.Value["type"]).GuessType(generatedTypes);
        }
        else
        {
            Type = typeof(string);
        }
    }

}

public class Err
{
    public string Code { get; set; }
    public string Message { get; set; }
}



#endregion //#region "api method call classes"

#region "types"

public class TypeFile
{
    #region "ctor"

    public TypeFile()
    {

    }

    public TypeFile(string fileName)
    {
        Name = Name;
    }

    #endregion //#region "ctor"

    #region "props"

    public string AllContent { get; set; }
    public string Name { get; set; }
    public string LastError { get; set; }
    public List<TypeProp> TypeProps { get; set; } = new List<TypeProp>();
    public static List<string> ExtractionDebug { get; set; } = new List<string>();

    #endregion //#region "props"

    public static TypeFile Parse(string fileName, string rawFileContent)
    {
        string extractedContent;

        try
        {
            extractedContent = GetContentFromFile(rawFileContent);
            if (string.IsNullOrWhiteSpace(extractedContent))
            {
                throw new Exception("Failed to deserialize");
            }
        }
        catch (Exception ex)
        {
            return new TypeFile
            {
                Name = fileName,
                LastError = ex.ToString().RemoveLineEndings(),
                AllContent = $"Raw: {rawFileContent.RemoveLineEndings()}"
            };
        }

        try
        {
            var jobject = Newtonsoft.Json.Linq.JObject.Parse(extractedContent);
            return ConstructFromJobj(fileName, jobject);
        }
        catch (Exception ex)
        {
            return new TypeFile
            {
                Name = fileName,
                LastError = ex.ToString().RemoveLineEndings(),
                AllContent = $"Extract: {extractedContent.RemoveLineEndings()}"
            };
        }
    }

    private static string GetContentFromFile(string rawFileContent)
    {
        var lines = rawFileContent.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        var indexOfLastBrace = -1;
        bool extractLines = false;
        var newLines = new List<string>();

        ExtractionDebug.Add($"- starting line ident");
        for (int i = 0; i < lines.Length; i++)
        {
            var trimmed = lines[i].Trim();
            if (extractLines == false && trimmed.Equals("{"))
            {
                extractLines = true;
                ExtractionDebug.Add($"{i} - extractLines == true");
            }

            if (extractLines == true && trimmed.IndexOf("}") >= 0)
            {
                indexOfLastBrace = newLines.Count;
                ExtractionDebug.Add($"{i} - closingBraceDetect at index {indexOfLastBrace}");
            }

            if (extractLines == true)
            {
                newLines.Add(trimmed);
                ExtractionDebug.Add($"{i} - '{trimmed}'");
            }
        }

        var importantLines = new System.Text.StringBuilder();
        ExtractionDebug.Add($"- starting line append");
        for (int i = 0; i < newLines.Count; i++)
        {
            var line = newLines[i];
            if (i > indexOfLastBrace)
            {
                continue;
            }

            var trimmed = line.Trim();
            if (string.IsNullOrWhiteSpace(trimmed))
            {
                continue;
            }

            importantLines.Append($"{trimmed}");
            ExtractionDebug.Add($"{i} -  important line added '{trimmed}'");
        }

        var endOfLine = importantLines.ToString().Substring(importantLines.Length - 10, 10);
        ExtractionDebug.Add($"- end of line {endOfLine}");

        if (endOfLine.LastIndexOf("},") >= 0)
        {
            ExtractionDebug.Add("Slack cant Json");
            importantLines.Length--;
            endOfLine = importantLines.ToString().Substring(importantLines.Length - 10, 10);
            ExtractionDebug.Add($"- new end of line {endOfLine}");
        }

        var importantLine = importantLines.ToString().RemoveLineEndings();

        //slack leaves a lot of crap in the files.
        return importantLine.Replace("{ … }", " {},").Replace(",…", string.Empty)
        .Replace("}\"is_member", "},\"is_member")
        /*from File.md - dirty files*/
        .Replace("&lt;", "<").Replace("&gt;", ">")
        .Replace("<div clas", "\"<div clas").Replace("\\n", string.Empty)
        .Replace(">\"<div", "><div ").Replace(", ...]", "]").Replace("{...}", "{ }")
        .Replace("\\/", "/");
    }


    public static TypeFile ConstructFromJobj(string fileName, Newtonsoft.Json.Linq.JObject jobj)
    {
        ExtractionDebug.Add($"- starting typeFile construction");
        var tf = new TypeFile();
        tf.Name = fileName.Replace(".md", string.Empty).TCase();
        foreach (var jtok in jobj)
        {
            var typeProp = new TypeProp();
            var name = jtok.Key.ToString();
            ExtractionDebug.Add($"\t\t  prop: {name}");

            typeProp.OriginalName = name;
            typeProp.Name = name.TCase().Replace("_", string.Empty);
            var trim = jtok.Value.ToString().RemoveLineEndings();
            ExtractionDebug.Add($"\t\t  name: {typeProp.Name} value: {trim}");
            if (trim.IndexOf("{") >= 0)
            {
                typeProp.IsCustomType = true;
            }
            if (trim.IndexOf("[") >= 0)
            {
                typeProp.IsArray = true;
            }
            typeProp.Raw = trim;
            ExtractionDebug.Add($"\t\t  isCustom: {typeProp.IsCustomType}  isArray: {typeProp.IsArray}");

            tf.TypeProps.Add(typeProp);

        }
        return tf;
    }
}

public class TypeProp
{
    public string OriginalName { get; set; }
    public string Name { get; set; }
    public bool IsArray { get; set; }
    public bool IsCustomType { get; set; }
    public string Raw { get; set; }

}

#endregion //#region "types"




//} //disabled for scripty
