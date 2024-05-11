//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 默认游戏框架日志辅助器。
    /// </summary>
    public class DefaultLogHelper : GameFrameworkLog.ILogHelper
    {
        /// <summary>
        /// 记录日志。
        /// </summary>
        /// <param name="level">日志等级。</param>
        /// <param name="message">日志内容。</param>
        public void Log(GameFrameworkLogLevel level, object message)
        {
            switch (level)
            {
                case GameFrameworkLogLevel.Debug:
                    Debug.Log(Utility.Text.Format("<color=#0079FF>[DEBUG]</color>{0}", message));
                    break;

                case GameFrameworkLogLevel.Info:
                    Debug.Log(Utility.Text.Format("<color=#00DFA2>[INFO]</color>{0}", message));
                    break;

                case GameFrameworkLogLevel.Warning:
                    Debug.LogWarning(Utility.Text.Format("<color=#F6FA70>[WARNING]</color>{0}", message));
                    break;

                case GameFrameworkLogLevel.Error:
                    Debug.LogError(Utility.Text.Format("<color=#FF0060>[ERROR]</color>{0}", message));
                    break;

                default:
                    throw new GameFrameworkException(message.ToString());
            }
        }
    }
}
