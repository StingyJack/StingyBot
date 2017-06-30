using System.Linq;

#region "Output Helpers"

/// <summary>
///     Shortcut to Output.WriteLine
/// </summary>
/// <param name="content"></param>
//public void Owl(string content = "") { Output.WriteLine(content); }
public void OwlTypes(string content = "") { Output["ApiGen.Scripty.Types.cs"].WriteLine(content); }
public void OwlMethods(string content = "") { Output["ApiGen.Scripty.Methods.cs"].WriteLine(content); }
public void OwlMethodExec(string content = "") { Output["ApiGen.Scripty.MethodExec.cs"].WriteLine(content); }
public void Owlog(string content = "") { Output["ApiGen.Scripty.debuglog"].WriteLine(content); }
public void OwlFailGen(string content = "") { Output["ApiGen.Scripty.failedGenLog"].WriteLine(content); }

public void KeepOutputs(bool codeFiles, bool logFiles, bool other)
{
    Output["ApiGen.Scripty.Types.cs"].KeepOutput = codeFiles;
    Output["ApiGen.Scripty.Methods.cs"].KeepOutput = codeFiles;
    Output["ApiGen.Scripty.MethodExec.cs"].KeepOutput = codeFiles;
    Output["ApiGen.Scripty.debuglog"].KeepOutput = logFiles;
    Output["ApiGen.Scripty.failedGenLog"].KeepOutput = logFiles;
    Output.KeepOutput = other;
}

private static System.Globalization.TextInfo _TextInfo = System.Globalization.CultureInfo.CurrentCulture.TextInfo;

/// <summary>
///     Makes the string TitleCase
/// </summary>
/// <param name="value"></param>
/// <returns></returns>
public static string TCase(this string value)
{
    return _TextInfo.ToTitleCase(value);
}

public static string ToSingleLine(this System.Collections.Generic.List<string> value)
{
    return string.Join(",", value.Select(e => e.RemoveLineEndings()).ToList());
}

public static string RemoveLineEndings(this string value)
{
    if (String.IsNullOrEmpty(value))
    {
        return value;
    }
    string lineSeparator = ((char)0x2028).ToString();
    string paragraphSeparator = ((char)0x2029).ToString();

    return
        value.Replace("\r\n", string.Empty)
            .Replace("\n", string.Empty)
            .Replace("\r", string.Empty)
            .Replace(lineSeparator, string.Empty)
            .Replace(paragraphSeparator, string.Empty);
}

#endregion //#region "Output Helpers"