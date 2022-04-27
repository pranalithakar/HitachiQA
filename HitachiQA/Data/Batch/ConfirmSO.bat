@ECHO OFF
SETLOCAL ENABLEEXTENSIONS

SET /A errno=0
SET /A ERROR_HELP_SCREEN=1
SET /A ERROR_SOMECOMMAND_NOT_FOUND=2
SET /A ERROR_OTHERCOMMAND_FAILED=4

cd C:\Program Files (x86)\Regression Suite Automation Tool\


Microsoft.Dynamics.RegressionSuite.ConsoleApp.exe list


Microsoft.Dynamics.RegressionSuite.ConsoleApp.exe playbackbyid 284