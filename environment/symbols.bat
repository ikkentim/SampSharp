@echo off
echo Converting all pdb symbols to mdb symbols...
echo.

call:prc "plugins"
call:prc "gamemodes"

echo Done!
echo.
goto:eof

:prc
echo Converting folder "%~1"...
for /R "%~1" %%f in (*.mdb) do ( 
echo Converting "%~1\%%~nf"...
"Mono\lib\mono\4.5\pdb2mdb" "%~1\%%~nf"
)
echo.
goto:eof
