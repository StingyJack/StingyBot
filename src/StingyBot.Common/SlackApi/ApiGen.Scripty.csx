#r ".\..\..\..\packages\Newtonsoft.Json.9.0.1\lib\net45\Newtonsoft.Json.dll"
#load "OutputHelpers.csx"
#load "ScriptyHelpers.csx"
#load "ApiGenHelperClasses.csx"
// This roslyn scripting context is weird...
// it appears to be a lifted/global scope, but scripts this file content in a class scope
// so any referenced files require instances of this "class" file to in order to call functions 
// declared here. It makes any kind of common logging a pain in the ass and especially so
// since there is no debugging support, and it cant list the correct file in the errors list (I may have fixed this)
// but does manage to get the line correct.

using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System;
using System.Linq;
using System.Collections.Generic;

#region "shortcuts and exclusions"

var regenFile = ShouldRegenOccur(Context);
if (regenFile == false)
{
    KeepOutputs(false, false, false);
    return;
}
KeepOutputs(true, true, false);

// if slack is slackin and leaving problematic files
var excludedTypes = new System.Collections.Generic.List<string>() { "" };
var excludedMethods = new System.Collections.Generic.List<string>() { "" };

#endregion //#region "shortcuts and exclusions"

//TODO: This should eventually just go to the web and get them.

var projectFile = Context.ProjectFilePath;
var projectFileInfo = new System.IO.FileInfo(projectFile);
var projectRootFolder = projectFileInfo.Directory;
var pathToSlackApiGitRoot = @"..\..\..\slack-api-docs";
var typesFolder = $"{projectRootFolder}\\{pathToSlackApiGitRoot}\\types";
var methodsFolder = $"{projectRootFolder}\\{pathToSlackApiGitRoot}\\methods";

#region "/Types generation"

Owlog($"// Generating types from sources at {typesFolder}");
if (System.IO.Directory.Exists(typesFolder) == false)
{
    throw new InvalidOperationException($"types folder folder not found at {typesFolder}");
}

var typesFiles = System.IO.Directory.GetFiles(typesFolder, "*.md");
var types = new List<TypeFile>();

foreach (var typesFile in typesFiles)
{
    var fileContent = System.IO.File.ReadAllText(typesFile);
    var fileInfo = new System.IO.FileInfo(typesFile);
    if (excludedTypes.Any(f => f.Equals(fileInfo.Name, StringComparison.OrdinalIgnoreCase))) { continue; }

    Owlog($"// Starting {fileInfo.Name}");

    TypeFile.ExtractionDebug.Clear();
    var tf = TypeFile.Parse(fileInfo.Name, fileContent);

    foreach (var debugStatement in TypeFile.ExtractionDebug)
    {
        Owlog($"// \t\t {debugStatement}");
    }

    if (tf == null)
    {
        Owlog($"// Failed to parse types file - {fileInfo.FullName}");
        OwlFailGen($"Type '{fileInfo.FullName}'");
     
        continue;
    }

    if (string.IsNullOrWhiteSpace(tf.LastError) == false)
    {
        OwlFailGen($"Type '{fileInfo.FullName}'");
        Owlog($"// Failed to parse types file - {fileInfo.FullName}");
        Owlog($"// \t\t {tf.LastError}");
        Owlog($"// \t\t {tf.AllContent}");
      
        continue;
    }
    types.Add(tf);
}

//R# always subtly complains about codegen files
OwlTypes(GetGenerationHeader());
OwlTypes("// Types Generation");
OwlTypes("// ReSharper disable RedundantUsingDirective");
OwlTypes("// ReSharper disable UnusedAutoPropertyAccessor.Global");
OwlTypes("// ReSharper disable PartialTypeWithSinglePart");
OwlTypes();
OwlTypes("namespace StingyBot.Common.SlackApi.Types");
OwlTypes("{");
OwlTypes("\t using System;");
//Owl("\t using System.Collections.Specialized;");
OwlTypes("\t using Newtonsoft.Json;");
OwlTypes();

foreach (var tf in types)
{

    OwlTypes($"\t public partial class {tf.Name}");
    OwlTypes($"\t {{");

    foreach (var propName in tf.TypeProps)
    {
        if (propName.IsArray)
        {
            OwlTypes($"\t\t  [JsonProperty(\"{propName.OriginalName}\")]");
            OwlTypes($"\t\t  public string[] {propName.Name} {{get;set;}}");
        }
        else if (propName.IsCustomType)
        {
            OwlTypes($"\t\t //skipping {propName.Name} - custom type");
        }
        else
        {
            OwlTypes($"\t\t  [JsonProperty(\"{propName.OriginalName}\")]");
            OwlTypes($"\t\t  public string {propName.Name} {{get;set;}}");
        }
        OwlTypes("");
    }

    OwlTypes($"\t }}");
    OwlTypes($"");
}

OwlTypes($"}}");

#endregion //#region "/Types generation"

#region "/Methods generation"

Owlog($"// Generating methods from sources at {methodsFolder}");
if (System.IO.Directory.Exists(methodsFolder) == false)
{
    throw new InvalidOperationException($"methods folder not found at {methodsFolder}");
}

var jsonFiles = System.IO.Directory.GetFiles(methodsFolder, "*.json");
var apiMethods = new System.Collections.Generic.List<ApiMethod>();

foreach (var jsonFile in jsonFiles)
{
    try
    {
        //hydrating the class in another script file was hard.
        var fileInfo = new System.IO.FileInfo(jsonFile);
        if (excludedMethods.Any(f => f.Equals(fileInfo.Name, StringComparison.OrdinalIgnoreCase))) { continue; }
        var fileContent = System.IO.File.ReadAllText(jsonFile);
        ApiMethod.ExtractDebug.Clear();
        var apiMethod = new ApiMethod(fileInfo.Name, fileContent, types);

        //this isnt as complex, but maybe its easier to have a giant file.
        var responseMdFile = System.IO.Path.ChangeExtension(jsonFile, "md");
        var responseContent = GetPossibleJsonFromFile(responseMdFile);
        if (responseContent.Errors.Count > 0)
        {
            OwlFailGen($"ApiResponse'{responseMdFile}'");
            Owlog($"// {apiMethod.Name} - fail -{responseContent.Errors.ToSingleLine()}");
            continue;
        }
        var jsonString = string.Join(",", responseContent.Result);
        Owlog($"// {apiMethod.Name} - Starting with {jsonString}");
        var cleanedJson = CleanJunkFromJson(jsonString);
        Owlog($"// {apiMethod.Name} - cleaned junk {cleanedJson}");
        var parseResult = ParseJobj(cleanedJson);
        if (parseResult.Errors.Count > 0)
        {
            Owlog($"// {apiMethod.Name} - fail parse - {string.Join(",", parseResult.Errors.Select(e => e.RemoveLineEndings()).ToList())}");
            Owlog($"// {apiMethod.Name} - trying harder with value - {cleanedJson}");
            parseResult = ParseJobjButTryHarder(cleanedJson);
            if (parseResult.ParsedObject == null)
            {
                foreach (var dl in parseResult.DebugLog)
                {
                    Owlog($"// {apiMethod.Name} - debug {dl.RemoveLineEndings()}");
                }
                Owlog($"// {apiMethod.Name} - !!!FAILED after trying harder {parseResult.Errors.ToSingleLine()}");
                OwlFailGen($"ApiMethod '{apiMethod.Name}'");
                continue;
            }
        }

        Owlog($"// {apiMethod.Name} - SUCCESS parse  - {parseResult.ParsedObject.ToString().RemoveLineEndings()}");

        apiMethod.Response = new ApiResponse();
        Owlog($"// {apiMethod.Name}  result object has {parseResult.ParsedObject.Count} children");
        foreach (var child in parseResult.ParsedObject)
        {
            var csharpType = GetCSharpTypeNameFromJTokenValue(child.Key, child.Value, types);
            Owlog($"// {apiMethod.Name} - key '{child.Key}' - type '{child.Value.Type}' c#type '{csharpType}' - value '{child.Value.ToString().RemoveLineEndings()}'");

            if (child.Key.Equals("ok", StringComparison.OrdinalIgnoreCase))
            {
                continue; // already on the base object
            }
            apiMethod.Response.Properties.Add(new ApiProp()
            {
                Name = child.Key.TCase(),
                TypeName = csharpType,
                OriginalName = child.Key                
            });
        }
        //apiMethod.Response.Properties.Add(new ApiProp() { Name = "name", Type = typeof(string) });
        apiMethods.Add(apiMethod);
    }
    catch (Exception ex)
    {
        OwlFailGen($"ApiMethod '{jsonFile}'");
        Owlog($"// !!!FAILED to add api method for '{jsonFile}': {ex.ToString().RemoveLineEndings()}");
        foreach (var ed in ApiMethod.ExtractDebug)
        {
            Owlog($"// extractDebug - {ed.RemoveLineEndings()}");
        }
    }
}

//R# always subtly complains about codegen files
OwlMethods(GetGenerationHeader());
OwlTypes("// Methods Generation");
OwlMethods("// ReSharper disable RedundantUsingDirective");
OwlMethods("// ReSharper disable UnusedAutoPropertyAccessor.Global");
OwlMethods("// ReSharper disable PartialTypeWithSinglePart");
OwlMethods();
OwlMethods("namespace StingyBot.Common.SlackApi.Methods");
OwlMethods("{");
OwlMethods("\t using System;");
OwlMethods("\t using System.Collections.Generic;");
OwlMethods("\t using System.Collections.Specialized;");
OwlMethods("\t using Newtonsoft.Json;");
OwlMethods("\t using Types;");
OwlMethods();

var requestAndResponseNames = new List<Tuple<string, string, string>>();

foreach (var apiMethod in apiMethods)
{
    var apiParamTypeName = $"{apiMethod.Name}RequestParams";
    var apiResponseTypeName = $"{apiMethod.Name}Response";
    requestAndResponseNames.Add(new Tuple<string, string, string>(apiParamTypeName, apiResponseTypeName, apiMethod.Name));

    var apiMethodText = new StringBuilder();
    apiMethodText.AppendLine($"\t /// <summary>");
    apiMethodText.AppendLine($"\t /// \t{apiMethod.Desc}");
    apiMethodText.AppendLine($"\t /// </summary>");
    apiMethodText.AppendLine($"\t public partial class {apiParamTypeName} : ApiRequestParams");
    apiMethodText.AppendLine("\t {");
    apiMethodText.AppendLine("");
    foreach (var arg in apiMethod.Args)
    {
        apiMethodText.AppendLine($"\t\t /// <summary>");
        var lines = arg.Desc.Split('\n');
        foreach (var line in lines)
        {
            apiMethodText.AppendLine($"\t\t /// \t{line}");
        }
        apiMethodText.AppendLine($"\t\t /// </summary>");
        if (string.IsNullOrWhiteSpace(arg.Example) == false)
        {
            apiMethodText.AppendLine($"\t\t /// <example>");
            apiMethodText.AppendLine($"\t\t /// \t\"{arg.Example}\"");
            apiMethodText.AppendLine($"\t\t /// </example>");
        }
        apiMethodText.AppendLine($"\t\t [JsonProperty(\"{arg.OriginalName}\")]");
        apiMethodText.AppendLine($"\t\t public {arg.Type.Name.ToLower()} {arg.Name} {{get; set;}}");
    }
    apiMethodText.AppendLine();
    apiMethodText.AppendLine($"\t\t public override string GetMethodName() {{return \"{apiMethod.MethodName}\"; }}");
    apiMethodText.AppendLine();
    apiMethodText.AppendLine($"\t\t public override NameValueCollection GetParams()");
    apiMethodText.AppendLine($"\t\t {{");
    apiMethodText.AppendLine($"\t\t\t var returnValue = new NameValueCollection();");
    apiMethodText.AppendLine($"\t\t\t returnValue.Add(\"token\", Convert.ToString(ApiToken));");
    foreach (var arg in apiMethod.Args)
    {
        apiMethodText.AppendLine($"\t\t\t if ({arg.Name} != null)");
        apiMethodText.AppendLine($"\t\t\t {{");
        apiMethodText.AppendLine($"\t\t\t\t returnValue.Add(\"{arg.OriginalName}\", Convert.ToString({arg.Name}));");
        apiMethodText.AppendLine($"\t\t\t }}");
    }
    apiMethodText.AppendLine($"\t\t\t return returnValue;");
    apiMethodText.AppendLine($"\t\t }}");

    apiMethodText.AppendLine("\t }");
    OwlMethods(apiMethodText.ToString());

    var apiResponseText = new System.Text.StringBuilder();
    apiResponseText.AppendLine($"\t /// <summary>");
    apiResponseText.AppendLine($"\t /// \tThe response to {apiParamTypeName}");
    apiResponseText.AppendLine($"\t /// </summary>");
    apiResponseText.AppendLine($"\t public partial class {apiResponseTypeName} : MethodResponseBase ");
    apiResponseText.AppendLine($"\t {{");
    foreach (var prop in apiMethod.Response.Properties)
    {
        apiResponseText.AppendLine($"\t\t [JsonProperty(\"{prop.OriginalName}\")]");
        apiResponseText.AppendLine($"\t\t public {prop.TypeName} {prop.Name} {{get; set;}}");
    }

    apiResponseText.AppendLine($"\t }}");
    OwlMethods(apiResponseText.ToString());
}
OwlMethods($"}}");
OwlMethods();

//R# always subtly complains about codegen files
OwlMethodExec(GetGenerationHeader());
OwlTypes("// Method Exec Generation");
OwlMethodExec("// ReSharper disable RedundantUsingDirective");
OwlMethodExec("// ReSharper disable UnusedAutoPropertyAccessor.Global");
OwlMethodExec("// ReSharper disable PartialTypeWithSinglePart");
OwlMethodExec();
OwlMethodExec("namespace StingyBot.Common.SlackApi");
OwlMethodExec("{");

OwlMethodExec("\t using Methods;");
OwlMethodExec();

OwlMethodExec($"\t public partial class MethodExecutor");
OwlMethodExec($"\t {{");

foreach (var apiMeth in requestAndResponseNames)
{
    OwlMethodExec($"\t\t public {apiMeth.Item2} Exec{apiMeth.Item3}({apiMeth.Item1} apiParam)");
    OwlMethodExec($"\t\t {{");
    OwlMethodExec($"\t\t\t return Execute<{apiMeth.Item2}>(apiParam);");
    OwlMethodExec($"\t\t }}");
}

OwlMethodExec($"\t }}");

OwlMethodExec("}");

#endregion //#region "/Methods generation"
