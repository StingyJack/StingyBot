using System.Linq;

#region "regeneration helpers"

public const string BUILD_CONFIG_NEEDED_TO_RUN_CODE_GEN = "RegenCodeFiles";

public bool ShouldRegenOccur(ScriptContext context)
{
    try
    {
        var activeProjectConfiguration = GetActiveProjectConfiguration(context);
        return BUILD_CONFIG_NEEDED_TO_RUN_CODE_GEN.Equals(activeProjectConfiguration, StringComparison.OrdinalIgnoreCase);
    }
    catch (System.Exception)
    {
        //ignoring for now
    }
    return false;
}

public string GetActiveProjectConfiguration(ScriptContext context)
{
    var projectCollection = Microsoft.Build.Evaluation.ProjectCollection.GlobalProjectCollection;
    var thisProj = projectCollection.LoadProject(context.Project.FilePath);
    var currentSlnCfg = thisProj.GlobalProperties["CurrentSolutionConfigurationContents"];
    var projectConfigs = System.Xml.Linq.XDocument.Parse(currentSlnCfg);
    var thisProjectNode = projectConfigs.Root.Descendants("ProjectConfiguration").FirstOrDefault(p => p.Attribute("AbsolutePath").Value == Project.FilePath);
    var projBuildValue = thisProjectNode.Value;
    var splits = projBuildValue.Split('|');
    var win = splits[0];
    return win;
}

public void WriteExistingOutputFileBackToItself(ScriptContext context)
{
    var fileName = System.IO.Path.ChangeExtension(context.ScriptFilePath, ".cs");
    context.Output.Write(System.IO.File.ReadAllText(fileName));
}

public string GetGenerationHeader()
{
    return $"// Generated at {DateTime.UtcNow} UTC by {Environment.UserName}";
}

#endregion //#region "regeneration helpers"

#region "DebuggingHelpers"


private static int _MsgBoxMaxDisplayPerSave = 1;
private static int _MsgBoxDisplayCount = 0;

private static void ShowMsg(string msg)
{
    _MsgBoxDisplayCount++;
    if (_MsgBoxDisplayCount < _MsgBoxMaxDisplayPerSave)
    {
        System.Windows.Forms.MessageBox.Show(msg);
    }
}

#endregion //#region "DebuggingHelpers"
