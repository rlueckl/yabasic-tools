@echo off

SET BASEDIR=C:\YaBasic
SET SRCFILE=BESCES-50008SAMPLE
SET MEMFILE="C:\Users\DRuNKeN MaSTeR\Documents\PCSX2\memcards\Mcd001.ps2"

cd %BASEDIR%

echo Adding CRC to file

cd jacksum-1.7.0
PowerShell.exe -NoProfile -ExecutionPolicy Bypass -Command "& './add_checksum.ps1' -in '%BASEDIR%\Source\%SRCFILE%.vb' -out '%BASEDIR%\Source\%SRCFILE%'"

cd ..

cd mymc-alpha-2.6

echo Removing old file from memcard
mymc.exe %MEMFILE% remove %SRCFILE%/%SRCFILE%

echo Adding new file to memcard
mymc.exe %MEMFILE% add -d %SRCFILE% "%BASEDIR%\Source\%SRCFILE%"

cd ..
