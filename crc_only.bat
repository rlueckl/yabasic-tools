@echo off

SET BASEDIR=C:\yabasic-tools
SET SRCFILE=BESCES-50008SAMPLE

cd %BASEDIR%

echo Adding CRC to file

cd jacksum-1.7.0
PowerShell.exe -NoProfile -ExecutionPolicy Bypass -Command "& './add_checksum.ps1' -in '%BASEDIR%\Source\%SRCFILE%.vb' -out '%BASEDIR%\Source\%SRCFILE%'"

cd ..
