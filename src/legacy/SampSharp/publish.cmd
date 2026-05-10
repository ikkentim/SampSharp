@echo off
REM Publish script for legacy SampSharp plugin - copies release DLL to artifacts

setlocal enabledelayedexpansion

pushd "%~dp0"

cd /d "..\..\.."
set "ROOTDIR=%CD%"
set "DLLSOURCE=%ROOTDIR%\build\bin\SampSharp\Release"
set "ARTIFACTDIR=%ROOTDIR%\build\artifacts\sampsharp-legacy"

popd

echo Publishing legacy SampSharp plugin...
echo Source: %DLLSOURCE%
echo Destination: %ARTIFACTDIR%
echo.

if not exist "%DLLSOURCE%\SampSharp.dll" (
    echo Error: SampSharp.dll not found at %DLLSOURCE%
    echo Please run build first: build.cmd legacy
    exit /b 1
)

if not exist "%ARTIFACTDIR%" mkdir "%ARTIFACTDIR%"

echo Copying files...
copy "%DLLSOURCE%\SampSharp.dll" "%ARTIFACTDIR%" /y
copy "%DLLSOURCE%\SampSharp.pdb" "%ARTIFACTDIR%" /y 2>nul
copy "%DLLSOURCE%\SampSharp.lib" "%ARTIFACTDIR%" /y 2>nul

echo.
echo Artifacts published to: %ARTIFACTDIR%
exit /b 0
