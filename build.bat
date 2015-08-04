@echo off

set bin_path=bin\
set package_path=%bin_path%package\

IF "%1"=="" (
echo ERROR: Invalid arguments!
exit /b 1
)

set version=%1

call :build_solution
call :build_package
goto :eof

:build_solution

set msBuildDir=%ProgramFiles(x86)%\MSBuild\14.0\bin\
"%msBuildDir%msbuild" "src\SampSharp.sln" /nologo /maxcpucount /verbosity:minimal /nodeReuse:false /p:Configuration=Release

goto :eof

:build_package

if not exist %package_path% mkdir %package_path%
if not exist %package_path%plugins mkdir %package_path%plugins

xcopy env\gamemodes %package_path%gamemodes /Y /I
xcopy env\filterscripts %package_path%filterscripts /Y /I
copy env\server.cfg.template %package_path% /Y
copy %bin_path%SampSharp.dll %package_path%plugins\ /Y

if exist %bin_path%sampsharp-%version%.zip del %bin_path%sampsharp-%version%.zip
call buildtools\zipjs.bat zipDirItems -source bin/package -destination "%bin_path%sampsharp-%version%.zip" -keep yes -force no

goto :eof
