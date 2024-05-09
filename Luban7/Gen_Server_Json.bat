set LUBAN_DLL=.\Tools\Luban.dll
set CONF_ROOT=.

dotnet %LUBAN_DLL% ^
    -t server ^
    -c cs-dotnet-json ^
    -d json ^
    --conf %CONF_ROOT%\luban.conf ^
    -x outputCodeDir=..\Server\Server\Model\Generate\LuBan ^
    -x outputDataDir=.\TestGen\Data ^
    -x l10n.textProviderFile=.\Datas\Client\L_Localization.xlsx
pause