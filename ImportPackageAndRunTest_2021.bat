@echo off
set UnityDefaultRoot=%ProgramFiles%\Unity\Hub\Editor

rem needs to be called with a package path as parameter
if "%~1"=="" (
	echo Usage: %0 packagePath [unityVersion]
	exit /b 1
)

rem Unity version comes either from second argument or the suffix of the script name itself
set UnityVersion=%~n0
set UnityVersion=%UnityVersion:*_=%
if not "%~2"=="" set UnityVersion=%~2%

rem check if there is UNITY_ROOT env variable; set to default otherwise
if not defined UNITY_ROOT (
	echo UNITY_ROOT is not set. Using default installation folder: %UnityDefaultRoot%
	set UNITY_ROOT=%UnityDefaultRoot%
)

rem Find the folder starting with Suffix in UNITY_ROOT
for /d %%d in ("%UNITY_ROOT%\%UnityVersion%*") do set UnityPath=%%d\Editor\Unity.exe
if not defined UnityPath (
	echo Unity %UnityVersion% is not installed in %UNITY_ROOT%. To change the installation folder, set UNITY_ROOT environment variable.
	exit /b 2
)

echo Using Unity: %UnityPath%

rem Check what's the git branch at the package path
set PackageBranch=unknown
set PackageFolder=%~dp1
pushd %PackageFolder%
rem use '/' as separator to avoid escaping issues
for /f %%i in ('git branch --show-current') do (
	set PackageBranch=%%i
)
popd
rem replace slashes with underscores
set PackageBranch=%PackageBranch:/=_%
echo %PackageBranch%

rem Choose scripting backends
set /p ScriptingBackend=Choose scripting backend (IL2CPP):
if "%ScriptingBackend%"=="" set ScriptingBackend=IL2CPP

rem Choose platform
set /p Platform=Choose platform (StandaloneWindows64):
if "%Platform%"=="" set Platform=StandaloneWindows64

rem Is this test going to be a new baseline?
set /p IsBaseline=Is this a new baseline (no):
if "%IsBaseline%"=="yes" set IsBaseline=1
if "%IsBaseline%"=="y" set IsBaseline=1


rem Script's directory, quantum_unity folder
set ProjectPath=%~dp0quantum_unity
set RootFolder=%~dp0

set PackagePath=%1
set Prefix=%PackageBranch%_%date:~-4,4%%date:~-10,2%%date:~-7,2%_%time:~0,2%%time:~3,2%%time:~6,2%

rem change output path depending on the baseline
set TestResultsPath=%RootFolder%TestResults\%Platform%_%ScriptingBackend%\%Prefix%.xml
if "%IsBaseline%"=="1" set TestResultsPath=%RootFolder%TestResults\%Platform%_%ScriptingBackend%_baseline.xml

rem Wait after each step
echo Importing package %PackagePath%
start /wait "" "%UnityPath%" -projectPath "%ProjectPath%" -importPackage "%PackagePath%" -logFile "%Prefix%.log" -ignorecompilererrors -batchmode -quit || exit /b 3
echo Running codegen
start /wait "" "%UnityPath%" -projectPath "%ProjectPath%" -executeMethod Quantum.Editor.QuantumCodeGenQtn.Run -logFile "%Prefix%.log" -ignorecompilererrors -batchmode -quit || exit /b 4
echo Run playmode tests (results: %TestResultsPath%)
start /wait "" "%UnityPath%" -projectPath "%ProjectPath%" -runTests -testResults "%TestResultsPath%" -testPlatform %Platform% -scriptingBackend %ScriptingBackend% -logFile "%Prefix%.log" -batchmode || exit /b 5
