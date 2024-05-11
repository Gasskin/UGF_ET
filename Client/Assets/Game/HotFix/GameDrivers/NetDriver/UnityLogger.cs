using System;
using System.Text.RegularExpressions;
using ET;
using Log = UnityGameFramework.Runtime.Log;

namespace Game.HotFix.GameDrivers
{
    public class UnityLogger: ILog
    {
        public void Trace(string msg)
        {
            Log.Info(msg);
            // UnityEngine.Debug.Log($"<color=gray><b>[Trace] ► </b></color> - {msg}");
        }

        public void Debug(string msg)
        {
            Log.Debug(msg);
            //UnityEngine.Debug.Log($"<color=#gray><b>[Debug] ► </b></color> - {msg}");
        }

        public void Info(string msg)
        {
            Log.Info(msg);
            // UnityEngine.Debug.Log($"<color=gray><b>[Info] ► </b></color> - {msg}");
        }

        public void Warning(string msg)
        {
            Log.Warning(msg);   
            // UnityEngine.Debug.LogWarning($"<color=#FF9400><b>[WARNING] ► </b></color> - {msg}");
        }

        public void Error(string msg)
        {
#if UNITY_EDITOR
            msg = Msg2LinkStackMsg(msg);
#endif
            Log.Error(msg);
            // UnityEngine.Debug.LogError($"<color=red><b>[ERROR] ► </b></color> - {msg}");
        }
        
        private static string Msg2LinkStackMsg(string msg)
        {
            msg = Regex.Replace(msg,@"at (.*?) in (.*?\.cs):(\w+)", match =>
            {
                string path = match.Groups[2].Value;
                string line = match.Groups[3].Value;
                return $"{match.Groups[1].Value}\n<a href=\"{path}\" line=\"{line}\">{path}:{line}</a>";
            });
            return msg;
        }

        public void Error(Exception e)
        {
            Log.Fatal(e);
            // UnityEngine.Debug.LogException(new Exception($"<color=red><b>[ERROR] ► </b></color> - {e}"));
        }
        
    }
}