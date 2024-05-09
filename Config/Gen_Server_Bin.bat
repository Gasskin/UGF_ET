set LUBAN_DLL=.\Tools\Luban.dll
set CONF_ROOT=.

dotnet %LUBAN_DLL% ^
    -t server ^
    -c cs-bin ^
    -d bin ^
    --conf %CONF_ROOT%\luban.conf ^
    -x outputCodeDir=..\Server\Server\Model\Generate\LuBan ^
    -x outputDataDir=..\Server\Config\Generate ^
    -x l10n.textProviderFile=.\Datas\Client\L_Localization.xlsx
pause