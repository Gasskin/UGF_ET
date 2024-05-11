@echo off
set PROTOS=.\Protos
set CLIENT=..\..\Client\Assets\Game\Runtime\Proto\Generate
set SERVER=
..\Bin\Tool.exe --AppType=Proto2CS --Console=1 "" %PROTOS% %CLIENT% %SERVER%

pause