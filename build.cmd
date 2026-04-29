@echo off
setlocal enabledelayedexpansion

set "TARGET=%1"
set "ACTION=%2"

set "SCRIPTDIR=%~dp0"

if "%TARGET%"=="" goto usage

if /i "%TARGET%"=="legacy-plugin" (
    if "%ACTION%"=="" (
        echo Building legacy plugin x86...
        call "%SCRIPTDIR%src\legacy\SampSharp\build.cmd"
        goto end
    ) else if /i "%ACTION%"=="publish" (
        echo Building and publishing legacy plugin x86...
        call "%SCRIPTDIR%src\legacy\SampSharp\build.cmd"
        if errorlevel 1 exit /b 1
        call "%SCRIPTDIR%src\legacy\SampSharp\publish.cmd"
        goto end
    ) else (
        goto usage
    )
)

if /i "%TARGET%"=="legacy-libraries" (
    if "%ACTION%"=="" (
        echo Building legacy C# libraries...
        call :build_legacy_libraries
        goto end
    ) else if /i "%ACTION%"=="publish" (
        echo Building and packing legacy C# libraries...
        call :pack_legacy_libraries
        goto end
    ) else (
        goto usage
    )
)

if /i "%TARGET%"=="component" (
    echo Component x64 plugin build not yet implemented.
    goto end
)

if /i "%TARGET%"=="component-libraries" (
    echo Component C# libraries not yet implemented.
    goto end
)

if /i "%TARGET%"=="clean" (
    echo Cleaning build directory...
    if exist "%SCRIPTDIR%build" (
        rmdir /s /q "%SCRIPTDIR%build"
        mkdir "%SCRIPTDIR%build"
    )
    echo Build directory cleaned.
    goto end
)

goto usage

:usage
echo Invalid target: %TARGET%
echo.
echo Usage:
echo   build.cmd legacy-plugin           - Build legacy x86 plugin
echo   build.cmd legacy-plugin publish   - Build and publish legacy x86 plugin
echo   build.cmd legacy-libraries        - Build legacy C# libraries
echo   build.cmd legacy-libraries publish - Build and pack legacy C# libraries
echo   build.cmd component               - Build component x64 plugin (not implemented)
echo   build.cmd component-libraries     - Build component C# libraries (not implemented)
echo   build.cmd clean                   - Delete build directory contents
exit /b 1

:build_legacy_libraries
cd /d "%SCRIPTDIR%src\legacy"
echo.
echo Building C# libraries...
dotnet build SampSharp.sln -c Release
if errorlevel 1 exit /b 1
exit /b 0

:pack_legacy_libraries
cd /d "%SCRIPTDIR%src\legacy"
echo.
echo Packing C# libraries...
dotnet pack SampSharp.sln -c Release
if errorlevel 1 exit /b 1
echo.
echo NuGet packages created in: %SCRIPTDIR%build\artifacts\packages
exit /b 0

:end
echo Build complete.
