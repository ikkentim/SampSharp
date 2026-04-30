@echo off
REM Publish script for SampSharp component - copies release DLL to artifacts

setlocal enabledelayedexpansion

pushd "%~dp0"

cd /d "..\..\.."
set "ROOTDIR=%CD%"
set "BUILDDIR=%ROOTDIR%\build\cmake\component"
set "ARTIFACTDIR=%ROOTDIR%\build\artifacts\sampsharp-component"

popd

echo Publishing SampSharp component...
echo Source: %BUILDDIR%\artifacts
echo Destination: %ARTIFACTDIR%
echo.

if not exist "%BUILDDIR%\artifacts\SampSharp.dll" (
    echo Error: SampSharp.dll not found at %BUILDDIR%\artifacts
    echo Please run build first: build.cmd
    exit /b 1
)

if not exist "%ARTIFACTDIR%" mkdir "%ARTIFACTDIR%"

echo Copying files...
copy "%BUILDDIR%\artifacts\SampSharp.dll" "%ARTIFACTDIR%" /y
copy "%BUILDDIR%\artifacts\SampSharp.pdb" "%ARTIFACTDIR%" /y 2>nul

echo.
echo Artifacts published to: %ARTIFACTDIR%
