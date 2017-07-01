Param([string] $BuildConfiguration = "NotSet")

Set-StrictMode -Version Latest
$ErrorActionPreference="Stop"


$targetDirectory = "$PSScriptRoot\ConsoleHostZipTemp"
$targetArchive = "StingyBot.ConsoleHost.Zip"
$slnPath = ".\StingyBot.sln"
$isBuilding = ($BuildConfiguration -ne "NotSet")
$projFilePaths = (
	".\src\ConsoleHost\StingyBot.ConsoleHost.csproj" `
	, ".\src\StingyBot.Bot\StingyBot.Bot.csproj" `
	, ".\src\StingyBot.Common\StingyBot.Common.csproj" `
	, ".\src\StingyBot.Handlers\StingyBot.Handlers.csproj" `
	, ".\src\StingyBot.SalesForce\StingyBot.SalesForce.csproj" `
	, ".\src\StingyBot.Tfs\StingyBot.Tfs.csproj" `
)


if ((Test-Path -Path $targetDirectory) -eq $false)
{
	New-Item -ItemType Directory -Path $targetDirectory | Out-Null
}
else
{
    Remove-Item -Force -Path $targetDirectory -Recurse
    New-Item -ItemType Directory -Path $targetDirectory | Out-Null
}

if ($isBuilding -eq $true )
{
	Write-Host "Building solution $slnPath with configuration $BuildConfiguration"
}
else
{
	Write-Host "Not building, just file movements. Checking the last build config paths for projects"
}


function Get-LastOutputPath([string] $ProjFile)
{
	Write-Verbose "Getting last output path for $ProjFile"
	
	$projContent = [xml] (Get-Content $ProjFile)
	Write-Verbose "Got content $projContent"

    #going after 0 based indicies isnt quite right
    #$propExists =  [bool]($propGroup.PSobject.Properties.name -match "Configuration")
	$lastBuildConfig = $projContent.Project.PropertyGroup[0].Configuration.InnerText
	Write-Verbose "Last build config was $lastBuildConfig"

    #$propExists =  [bool]($propGroup.PSobject.Properties.name -match "Platform")
	$lastPlatform = $projContent.Project.PropertyGroup[0].Platform.InnerText
	Write-Verbose "Last platform was $lastPlatform"
	
	$propertyGroupSearchString = [string]::Format(" '`$(Configuration)|`$(Platform)' == '{0}|{1}'", $lastBuildConfig,$lastPlatform).Trim()
	Write-Verbose "Locating property group to match $propertyGroupSearchString"    
    
    $targetOutputPath = "bin\NotFound"    

    foreach($propGroup in $projContent.Project.PropertyGroup)
    {
        $attrExists = [bool]($propGroup.Attributes["Condition"])        

        if ($attrExists)
        {
            $attr = $propGroup.Attributes["Condition"].Value.Trim()
            Write-Verbose "PropGroup has $attr"
            Write-Verbose "Looking for   $propertyGroupSearchString"
            if ($attr -Contains $propertyGroupSearchString)
            {
                $targetOutputPath = $propGroup.OutputPath
                Break
            }
        }
        else
        {
            Write-Verbose "prop group doesnt have a condition attribute"
            Continue
        }
    }
	
	Write-Verbose "Found $targetOutputPath"
    
    $projFileAbsolutePath = Resolve-Path $ProjFile
    Write-Verbose "Proj file abs path $projFileAbsolutePath "

    $projFileFolder = (Get-Item -Path $projFileAbsolutePath).Directory.FullName
    $projOutputFolder = Join-Path -Path $projFileFolder -ChildPath $targetOutputPath

    return $projOutputFolder 
}

foreach ($projFilePath in $projFilePaths)
{
    $projFileAbsolutePath = Resolve-Path $projFilePath
    $projName = (Get-Item -Path $projFileAbsolutePath).Name
	$projOutputFolder = Get-LastOutputPath -ProjFile $projFileAbsolutePath 
	Write-Host "$projName output files are located at $projOutputFolder"
    Copy-Item -Recurse -Path "$projOutputFolder\*.*" -Destination $targetDirectory
}

Compress-Archive -Path $targetDirectory -DestinationPath $targetArchive -Force
Remove-Item -Force -Path $targetDirectory -Recurse
