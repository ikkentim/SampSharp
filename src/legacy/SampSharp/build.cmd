@echo off
REM Build script for legacy SampSharp x86 plugin on Windows

pushd "%~dp0"

REM Navigate to root (3 levels up)
cd /d "..\..\.."
set "ROOTDIR=%CD%"
set "SRCDIR=%ROOTDIR%\src\legacy\SampSharp"
set "BUILDDIR=%ROOTDIR%\build\cmake\legacy"

popd

echo Building legacy SampSharp plugin (x86)...
echo Root: %ROOTDIR%
echo Build: %BUILDDIR%
echo.

if not exist "%BUILDDIR%" mkdir "%BUILDDIR%"

REM Configure with x86 (Win32) architecture
cmake -S "%SRCDIR%" -B "%BUILDDIR%" -A Win32 -T ClangCL
if errorlevel 1 exit /b 1

REM Build
cmake --build "%BUILDDIR%" --config RelWithDebInfo
if errorlevel 1 exit /b 1

echo.
echo Legacy plugin build complete. Output: %ROOTDIR%\build\bin\SampSharp\Release
