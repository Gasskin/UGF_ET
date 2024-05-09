using System;
using System.Collections.Generic;
using Luban;
#if DOTNET || UNITY_STANDALONE
using System.Threading.Tasks;
#endif

namespace ET
{
    public class ConfigLoader: Singleton<ConfigLoader>, ISingletonAwake
    {
        public struct GetAllConfigBytes
        {
        }

        public struct GetOneConfigBytes
        {
            public string ConfigName;
        }

        public void Awake()
        {
        }

        public async ETTask Reload()
        {
            this.LoadAsync().Coroutine();
            await ETTask.CompletedTask;
        }

        public async ETTask LoadAsync()
        {
            await EventSystem.Instance.Invoke<GetAllConfigBytes, ETTask>(new GetAllConfigBytes());
        }
    }
}