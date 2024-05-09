set LUBAN_DLL=.\Tools\Luban.dll
set CONF_ROOT=.

dotnet %LUBAN_DLL% ^
    -t client ^
    -c cs-simple-json ^
    -d json ^
    --conf %CONF_ROOT%\luban.conf ^
    -x outputCodeDir=..\Assets\Game\HotFix\GameDrivers\Config\Generate ^
    -x outputDataDir=..\Assets\Bundles\LuBan ^
    -x l10n.textProviderFile=.\Datas\Client\L_Localization.xlsx
pause