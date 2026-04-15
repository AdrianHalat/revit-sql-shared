@echo off
setlocal

set REVIT_EXE="C:\Program Files\Autodesk\Revit 2024\Revit.exe"
set ADDINS_DIR=C:\ProgramData\Autodesk\Revit\Addins\2024
set DIST=.\dist
set MODEL=.\test-env\TestProject.rvt

copy /Y "%DIST%\*" "%ADDINS_DIR%"

start "" %REVIT_EXE% "%MODEL%"
