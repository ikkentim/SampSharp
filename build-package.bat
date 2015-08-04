@echo off

setlocal EnableDelayedExpansion
title Build SampSharp packages

set bin_path=bin\
set package_path=%bin_path%package\
set nugetsource=http://nuget.timpotze.nl/upload/
set /p nugetkey=< buildtools\nugetkey.cfg

call :versionprompt
call :displayconfig
call :tagrepo
call build %version%
call :buildnugetpackage
call :pushnugetpackage

echo Done^^!
pause
exit

:versionprompt

:getmajor
set /p major="Enter Major Version (0-...): "
if [%major%] == [] goto :getmajor
:getminor
set /p minor="Enter Minor Version (0-...): "
if [%minor%] == [] goto :getminor
:getpatch
set /p patch="Enter Patch Version (0-...): "
if [%major%] == [] goto :getpatch
:getbeta
set /p beta="Enter Beta Version (0=no beta, 1-...): "
if [%beta%] == [] goto :getbeta

if "%beta%" == "0" ( 
    set version=v%major%.%minor%.%patch%
) else ( 
    set version=v%major%.%minor%.%patch%-beta%beta%
)

goto :eof

:displayconfig
echo.
echo Configuration:
echo Version                = %version%
echo NuGet key              = %nugetkey%
echo.

goto :eof

:tagrepo
call git tag -a %version% -m "%version%"
call git push --tags

goto :eof

:buildnugetpackage
del /S %bin_path%*.nupkg
call buildtools\nuget pack src\SampSharp.GameMode\SampSharp.GameMode.csproj -Prop Configuration=Release -OutputDirectory bin

goto :eof

:pushnugetpackage
for %%f in (%bin_path%*.nupkg) do set packagepath=%%~ff 
call buildtools\nuget push "%packagepath%" "%nugetkey%" -Source %nugetsource%

goto :eof