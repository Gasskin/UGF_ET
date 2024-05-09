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
            await ETTask.CompletedTask;
        }
    }
}