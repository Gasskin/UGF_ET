set LUBAN_DLL=.\Tools\Luban.dll
set CONF_ROOT=.

dotnet %LUBAN_DLL% ^
    -t client ^
    -c cs-bin ^
    -d bin  ^
    --conf %CONF_ROOT%\luban.conf ^
    -x outputCodeDir=..\Assets\Game\HotFix\GameDrivers\Config\Generate ^
    -x outputDataDir=..\Assets\Bundles\LuBan

pause