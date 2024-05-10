namespace ET
{
    public static class LoginHelper
    {
        public static async ETTask Login(Scene root, string account, string password)
        {
            root.RemoveComponent<ClientSenderComponent>();
            
            ClientSenderComponent clientSenderComponent = root.AddComponent<ClientSenderComponent>();
            
            var response = await clientSenderComponent.LoginAsync(account, password);
            if (response.Error!= ErrorCode.ERR_Success)
            {
                Log.Error($"Login error: {response.Error}");
                return;
            }
            Log.Info($"登录Realm服成功 Token:{response.Token}");


            var c2RGetServerInfos = C2R_GetServerInfos.Create();
            c2RGetServerInfos.Token = response.Token;
            c2RGetServerInfos.Account = account;

            var r2CGetServerInfos = await clientSenderComponent.Call(c2RGetServerInfos) as R2C_GetServerInfos;
            if (r2CGetServerInfos.Error != ErrorCode.ERR_Success) 
            {
                Log.Error("请求服务器列表失败");
                return;
            }

            foreach (var serverInfo in r2CGetServerInfos.ServerInfoList)
            {
                Log.Error(serverInfo.ServerName);
            }
            
            await ETTask.CompletedTask;
        }
    }
}