using System;
using System.Collections;

namespace ET.Server
{
    public static partial class WatcherHelper
    {
        public static cfg.StartMachineConfig GetThisMachineConfig()
        {
            string[] localIP = NetworkHelper.GetAddressIPs();
            cfg.StartMachineConfig startMachineConfig = null;
            foreach (cfg.StartMachineConfig config in cfg.StartMachineTable.Instance.DataList)
            {
                if (!WatcherHelper.IsThisMachine(config.InnerIP, localIP))
                {
                    continue;
                }
                startMachineConfig = config;
                break;
            }

            if (startMachineConfig == null)
            {
                throw new Exception("not found this machine ip config!");
            }

            return startMachineConfig;
        }
        
        public static bool IsThisMachine(string ip, string[] localIPs)
        {
            if (ip != "127.0.0.1" && ip != "0.0.0.0" && !((IList) localIPs).Contains(ip))
            {
                return false;
            }
            return true;
        }
        
        public static System.Diagnostics.Process StartProcess(int processId, int createScenes = 0)
        {
            cfg.StartProcessConfig startProcessConfig = cfg.StartProcessTable.Instance.Get(processId);
            const string exe = "dotnet";
            string arguments = $"App.dll" + 
                    $" --Process={startProcessConfig.Id}" +
                    $" --AppType=Server" +  
                    $" --StartConfig={Options.Instance.StartConfig}" +
                    $" --Develop={Options.Instance.Develop}" +
                    $" --LogLevel={Options.Instance.LogLevel}" +
                    $" --Console={Options.Instance.Console}";
            Log.Debug($"{exe} {arguments}");
            System.Diagnostics.Process process = ProcessHelper.Run(exe, arguments);
            return process;
        }
    }
}