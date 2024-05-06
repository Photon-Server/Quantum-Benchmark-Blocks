param (
    [Parameter(Mandatory=$true)]
    [string]$PackagePath,
    [string]$UnityVersion = "2021",
    [string]$ScriptingBackend = "IL2CPP",
    [string]$Platform = "StandaloneWindows64",
    [string]$TestFilter,
    [switch]$IsBaseline,
    [switch]$AddPragmaMaxComponents512    
)

$UnityDefaultRoot = "${env:ProgramFiles}\Unity\Hub\Editor"

if (!$UnityVersion) {
    $UnityVersion = $MyInvocation.MyCommand.Name -replace ".*_"
}

if (!$env:UNITY_ROOT) {
    Write-Host "UNITY_ROOT is not set. Using default installation folder: $UnityDefaultRoot"
    $env:UNITY_ROOT = $UnityDefaultRoot
}

$UnityPath = Get-ChildItem -Path "$env:UNITY_ROOT\$UnityVersion*" -Directory | ForEach-Object { "$_\Editor\Unity.exe" } | Select-Object -Last 1

if (!$UnityPath) {
    Write-Host "Unity $UnityVersion is not installed in $env:UNITY_ROOT. To change the installation folder, set UNITY_ROOT environment variable."
    exit 2
}

Write-Host "Using Unity: $UnityPath"

$PackageFolder = Split-Path -Parent $PackagePath
Push-Location $PackageFolder
$PackageBranch = git branch --show-current
Pop-Location
$PackageBranch = $PackageBranch -replace "/", "_"
Write-Host "Package branch: $PackageBranch"

$ProjectPath = Join-Path $PSScriptRoot "quantum_unity"
$RootFolder = $PSScriptRoot
$Prefix = "${PackageBranch}_$(Get-Date -Format "yyyyMMdd_HHmmss")"
$TestTempFolder = Join-Path $ProjectPath "Assets\TempTestData"


if ($IsBaseline) {
    $TestResultsPath = Join-Path $RootFolder "TestResults\${Platform}_${ScriptingBackend}_baseline.xml"
} else {
    $TestResultsPath = Join-Path $RootFolder "TestResults\${Platform}_${ScriptingBackend}\$Prefix.xml"
}

function Start-UnityProcess {
    param (
        [string]$UnityPath,
        [string]$Arguments
    )

    $process = Start-Process -Wait -PassThru -FilePath $UnityPath -ArgumentList $Arguments

    if ($process.ExitCode -ne 0) {
        Write-Error "Process failed with exit code $($process.ExitCode)"
        exit $process.ExitCode
    }
}

# delete everything in TempTestFolder except for .gitkeep
Get-ChildItem -Path $TestTempFolder -Exclude ".gitkeep" | Remove-Item -Recurse -Force

Write-Host "Importing package $PackagePath"
Start-UnityProcess -UnityPath $UnityPath -Arguments "-projectPath `"$ProjectPath`" -buildTarget $Platform  -importPackage `"$PackagePath`" -logFile `"$Prefix.log`" -ignorecompilererrors -batchmode -quit"

if ($AddPragmaMaxComponents512) {
    # add a .qtn file with #pragma max_components 512
    $PragmaMaxComponentsPath = Join-Path $TestTempFolder "MaxComponents512.qtn"
    Set-Content -Path $PragmaMaxComponentsPath -Value "#pragma max_components 512"
}

Write-Host "Running codegen"
Start-UnityProcess -UnityPath $UnityPath -Arguments "-projectPath `"$ProjectPath`" -executeMethod Quantum.Editor.QuantumCodeGenQtn.Run -logFile `"$Prefix.log`" -ignorecompilererrors -batchmode -quit"

Write-Host "Run playmode tests (results: $TestResultsPath)"
Start-UnityProcess -UnityPath $UnityPath -Arguments "-projectPath `"$ProjectPath`" -runTests -testResults `"$TestResultsPath`" -testPlatform $Platform -scriptingBackend $ScriptingBackend -logFile `"$Prefix.log`" -batchmode"