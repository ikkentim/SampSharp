@echo off
REM Build script for SampSharp component (x64) on Windows

pushd "%~dp0"

REM Navigate to root (3 levels up)
cd /d "..\..\.."
set "ROOTDIR=%CD%"
set "SRCDIR=%ROOTDIR%\src\sampsharp-component"
set "BUILDDIR=%ROOTDIR%\build\cmake\component"

popd

echo Building SampSharp component (x64)...
echo Root: %ROOTDIR%
echo Build: %BUILDDIR%
echo.

if not exist "%BUILDDIR%" mkdir "%BUILDDIR%"

REM Configure with x64 architecture
cmake -S "%SRCDIR%" -B "%BUILDDIR%" -T ClangCL -DCMAKE_POLICY_VERSION_MINIMUM=3.5
if errorlevel 1 exit /b 1

REM Build
cmake --build "%BUILDDIR%" --config RelWithDebInfo
if errorlevel 1 exit /b 1

echo.
echo Component build complete. Output: %BUILDDIR%\artifacts
