@echo off
setlocal

set API=.\libs\RevitAPI.dll
set APIUI=.\libs\RevitAPIUI.dll
set CSC=C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe

set OUT=.\dist\RevitSqlShared.dll
set ADDIN_NAME=RevitSqlShared.addin
set ADDIN_SRC=.\manifest\%ADDIN_NAME%
set ADDIN_DIST=.\dist\%ADDIN_NAME%

set REVIT_ADDINS=C:\ProgramData\Autodesk\Revit\Addins\2024

"%CSC%" ^
/target:library ^
/platform:x64 ^
/nologo ^
/out:"%OUT%" ^
/reference:"%API%" ^
/reference:"%APIUI%" ^
/reference:System.Data.dll ^
/reference:System.Windows.Forms.dll ^
.\src\*.cs

if errorlevel 1 exit /b 1
copy /Y "%ADDIN_SRC%" "%ADDIN_DIST%"
copy /Y ".\dist\*" "%REVIT_ADDINS%"
