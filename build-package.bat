@echo off

:: ***********************************
:: CONFIGURATION
:: ***********************************

setlocal EnableDelayedExpansion
title Build SampSharp packages

set default_version=UNKNOWN_VERSION
set tag_min_length=1
set tag_max_length=8
set package_path=bin\package\
set bin_path=bin\
set nugetsource=http://nuget.timpotze.nl/upload/

:: ***********************************
:: PROMPT NEW VERSION TAG
:: ***********************************

echo.
echo Versioning
echo ----------

:getmajor
set /p major="Enter Major Version: "
if [%major%] == [] goto :getmajor
:getminor
set /p minor="Enter Minor Version: "
if [%minor%] == [] goto :getminor
:getpatch
set /p patch="Enter Patch Version: "
if [%major%] == [] goto :getpatch

set version=v%major%.%minor%.%patch%

:: ***********************************
:: LET USER VERIFY CONFIGURATION
:: ***********************************

echo.
echo Read configuration
echo ------------------

REM git latest tag
set gittag=%default_version%
for /f %%i in ('"git describe --tags --abbrev=0"') do set gittag=%%i
call :strlen gitlen gittag
if %gitlen% GTR %tag_max_length% set gittag=%default_version%
if %gitlen% LSS %tag_min_length% set gittag=%default_version%

REM nuget key config
set /p nugetkey=< buildtools\nugetkey.cfg
call :strlen keylen nugetkey
if %keylen% NEQ 32 goto :nonugetkey
goto :displayconfig

:nonugetkey
echo No NuGet key was set. Create buildtools\nuget.cfg and fill it with your NuGet key.

pause
goto :eof

:displayconfig
echo.
echo Configuration:
echo Version                = %version%
echo Git latest tag         = %gittag%
echo NuGet key              = %nugetkey%
echo.

echo MAKE SURE YOU HAVE BUILD THE PROJECT IN "RELEASE" MODE^^!^^!^^!
echo.
pause

:: ***********************************
:: CREATE NEW VERSION TAG
:: ***********************************

:tagrepo
echo.
echo Tagging Repository
echo --------------------

call git tag -a %version% -m "%version%"
call git push --tags

:: ***********************************
:: BUILD PLUGIN PACKAGE
:: ***********************************

:buildpluginpackage
echo.
echo Build Plugin Package
echo --------------------

echo Initialize directory structure...
if not exist %package_path% mkdir %package_path% >NUL
if not exist %package_path%plugins mkdir %package_path%plugins >NUL

echo Copy gamemodes...
xcopy env\gamemodes %package_path%gamemodes /Y /I >NUL

echo Copy filterscripts...
xcopy env\filterscripts %package_path%filterscripts /Y /I >NUL

echo Copy configuration template...
copy env\server.cfg.template %package_path% /Y >NUL

echo Copy binaries...
copy %bin_path%SampSharp.dll %package_path%plugins\ /Y >NUL

echo Zipping...
del %bin_path%sampsharp-%version%.zip
call buildtools\zipjs.bat zipDirItems -source bin/package -destination "%bin_path%sampsharp-%version%.zip" -keep yes -force no
echo Archive saved in %bin_path%sampsharp-%version%.zip
pause
:: ***********************************
:: BUILD NUGET PACKAGE
:: ***********************************

:buildnugetpackage
echo.
echo Build NuGet Package
echo -------------------

echo Deleting old NuGet packages...
del /S %bin_path%*.nupkg

echo Packing NuGet package...
call buildtools\nuget pack src\SampSharp.GameMode\SampSharp.GameMode.csproj -Prop Configuration=Release -OutputDirectory bin

for %%f in (%bin_path%*.nupkg) do set packagepath=%%~ff
    
echo Pushing nuget package...   
call buildtools\nuget push "%packagepath%" "%nugetkey%" -Source %nugetsource%
echo.
echo Done^^!

pause
goto :eof

:: ***********************************
:: FUNCTIONS
:: ***********************************

:strlen <resultVar> <stringVar>
(   
    set "s=!%~2!#"
    set "len=0"
    for %%P in (4096 2048 1024 512 256 128 64 32 16 8 4 2 1) do (
        if "!s:~%%P,1!" NEQ "" ( 
            set /a "len+=%%P"
            set "s=!s:~%%P!"
        )
    )
)
( 
    set "%~1=%len%"
    exit /b
)
:eof