# accepts platform and scripting backend 
param(
    [Parameter(Mandatory)][string]$Platform = "StandaloneWindows64",
    [Parameter(Mandatory)][string]$ScriptingBackend = "IL2CPP",
    [float]$threshold = 0.02
)

$results = "TestResults/${Platform}_${ScriptingBackend}"
$baseline = "TestResults/${Platform}_${ScriptingBackend}_baseline.xml"

UnityPerformanceBenchmarkReporter/UnityPerformanceBenchmarkReporter.exe --results="$results" --baselinexmlsource:"$baseline" -t="$threshold" --openreport --report="UnityPerformanceBenchmarkReports"
