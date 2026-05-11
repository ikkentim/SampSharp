@echo off
setlocal enabledelayedexpansion

set "TARGET=%~1"
if "%TARGET%"=="" goto usage
shift

set "ACTION="
if /i "%~1"=="test" (
    set "ACTION=test"
    shift
) else if /i "%~1"=="publish" (
    set "ACTION=publish"
    shift
)

set "CI_VERSION="
set "NO_BUILD_ARG="

:parse_options
if "%~1"=="" goto options_done

if /i "%~1"=="--no-build" (
    set "NO_BUILD_ARG=--no-build"
    shift
    goto parse_options
)

set "ARG=%~1"
if /i "!ARG:~0,10!"=="--version=" (
    set "CI_VERSION=!ARG:~10!"
    if "!CI_VERSION!"=="" goto missing_version
    shift
    goto parse_options
)

if /i "%~1"=="--version" (
    if "%~2"=="" goto missing_version
    set "CI_VERSION=%~2"
    shift
    shift
    goto parse_options
)

echo Invalid option: %~1
goto usage

:missing_version
echo Missing value for --version
exit /b 1

:options_done

set "SCRIPTDIR=%~dp0"

if /i "%TARGET%"=="legacy-plugin" (
    if "%ACTION%"=="" (
        echo Building legacy plugin x86...
        call "%SCRIPTDIR%src\legacy\SampSharp\build.cmd"
        if errorlevel 1 exit /b 1
        goto end
    ) else if /i "%ACTION%"=="publish" (
        echo Building and publishing legacy plugin x86...
        call "%SCRIPTDIR%src\legacy\SampSharp\build.cmd"
        if errorlevel 1 exit /b 1
        call "%SCRIPTDIR%src\legacy\SampSharp\publish.cmd"
        if errorlevel 1 exit /b 1
        goto end
    ) else (
        goto usage
    )
)

if /i "%TARGET%"=="legacy-libraries" (
    if "%ACTION%"=="" (
        echo Building legacy C# libraries...
        call :build_legacy_libraries
        if errorlevel 1 exit /b 1
        goto end
    ) else if /i "%ACTION%"=="publish" (
        echo Building and packing legacy C# libraries...
        call :pack_legacy_libraries
        if errorlevel 1 exit /b 1
        goto end
    ) else (
        goto usage
    )
)

if /i "%TARGET%"=="component" (
    if "%ACTION%"=="" (
        echo Building open.mp component...
        call "%SCRIPTDIR%src\sampsharp-component\build.cmd"
        if errorlevel 1 exit /b 1
        goto end
    ) else if /i "%ACTION%"=="publish" (
        echo Building and publishing open.mp component...
        call "%SCRIPTDIR%src\sampsharp-component\build.cmd"
        if errorlevel 1 exit /b 1
        call "%SCRIPTDIR%src\sampsharp-component\publish.cmd"
        if errorlevel 1 exit /b 1
        goto end
    ) else (
        goto usage
    )
)

if /i "%TARGET%"=="component-libraries" (
    if "%ACTION%"=="" (
        echo Building C# libraries...
        call :build_component_libraries
        if errorlevel 1 exit /b 1
        goto end
    ) else if /i "%ACTION%"=="test" (
        echo Testing C# libraries...
        call :test_component_libraries
        if errorlevel 1 exit /b 1
        goto end
    ) else if /i "%ACTION%"=="publish" (
        echo Building and packing C# libraries...
        call :pack_component_libraries
        if errorlevel 1 exit /b 1
        goto end
    ) else (
        goto usage
    )
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

echo Invalid target: %TARGET%
goto usage

:usage
echo.
echo Usage:
echo   build.cmd legacy-plugin           - Build legacy x86 plugin
echo   build.cmd legacy-plugin publish   - Build and publish legacy x86 plugin
echo   build.cmd legacy-libraries        - Build legacy C# libraries
echo   build.cmd legacy-libraries publish - Build and pack legacy C# libraries
echo   build.cmd component               - Build open.mp component
echo   build.cmd component publish       - Build and publish open.mp component
echo   build.cmd component-libraries        - Build C# libraries
echo   build.cmd component-libraries test   - Test C# libraries
echo   build.cmd component-libraries publish - Build and pack C# libraries
echo   build.cmd clean                   - Delete build directory contents
echo.
echo Options:
echo   --no-build            - Skip the build step for tests
echo   --version^=<version^>   - Set the CI package version
exit /b 1

:build_legacy_libraries
cd /d "%SCRIPTDIR%src\legacy"
echo.
echo Building C# libraries...
if defined CI_VERSION (
    dotnet build SampSharp.Legacy.slnx -c Release "/p:CiVersion=%CI_VERSION%"
) else (
    dotnet build SampSharp.Legacy.slnx -c Release
)
if errorlevel 1 exit /b 1
exit /b 0

:pack_legacy_libraries
cd /d "%SCRIPTDIR%src\legacy"
echo.
echo Packing C# libraries...
if defined CI_VERSION (
    dotnet pack SampSharp.Legacy.slnx -c Release "/p:CiVersion=%CI_VERSION%"
) else (
    dotnet pack SampSharp.Legacy.slnx -c Release
)
if errorlevel 1 exit /b 1
echo.
echo NuGet packages created in: %SCRIPTDIR%build\artifacts\packages
exit /b 0

:build_component_libraries
cd /d "%SCRIPTDIR%"
echo.
echo Building C# libraries...
if defined CI_VERSION (
    dotnet build SampSharp.slnx -c Release "/p:CiVersion=%CI_VERSION%"
) else (
    dotnet build SampSharp.slnx -c Release
)
if errorlevel 1 exit /b 1
exit /b 0

:test_component_libraries
cd /d "%SCRIPTDIR%"
set "RESULTSDIR=%SCRIPTDIR%build\test-results\component-libraries"
if not exist "%RESULTSDIR%" mkdir "%RESULTSDIR%"
echo.
echo Testing C# libraries...
if defined CI_VERSION (
    dotnet test SampSharp.slnx -c Release %NO_BUILD_ARG% --results-directory "%RESULTSDIR%" --logger "trx;LogFilePrefix=component-libraries" "/p:CiVersion=%CI_VERSION%"
) else (
    dotnet test SampSharp.slnx -c Release %NO_BUILD_ARG% --results-directory "%RESULTSDIR%" --logger "trx;LogFilePrefix=component-libraries"
)
if errorlevel 1 exit /b 1
exit /b 0

:pack_component_libraries
cd /d "%SCRIPTDIR%"
echo.
echo Packing C# libraries...
if defined CI_VERSION (
    dotnet pack SampSharp.slnx -c Release "/p:CiVersion=%CI_VERSION%"
) else (
    dotnet pack SampSharp.slnx -c Release
)
if errorlevel 1 exit /b 1
echo.
echo NuGet packages created in: %SCRIPTDIR%build\artifacts\packages
exit /b 0

:end
echo Build complete.
