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
                return;
            Log.Info($"登录Realm服成功 Token:{response.Token}");


            var getServerInfos = C2R_GetServerInfos.Create();
            getServerInfos.Token = response.Token;
            getServerInfos.Account = account;

            var getServerInfosResult = await clientSenderComponent.Call(getServerInfos) as R2C_GetServerInfos;
            if (getServerInfosResult == null || getServerInfosResult.Error != ErrorCode.ERR_Success) 
                return;

            if (getServerInfosResult.ServerInfoList.Count <= 0) 
            {
                Log.Error("服务器列表为空");
                return;
            }
            Log.Info("获取服务器列表成功");

            var server = getServerInfosResult.ServerInfoList[0];
            
            var getRoles = C2R_GetRoles.Create();
            getRoles.Token = response.Token;
            getRoles.Account = account;
            getRoles.ServerId = server.Id;
            var getRolesResult = await clientSenderComponent.Call(getRoles) as R2C_GetRoles;
            if (getRolesResult == null || getRolesResult.Error != ErrorCode.ERR_Success) 
                return;

            if (getRolesResult.RoleInfoList.Count <= 0)
            {
                Log.Info("获取角色列表成功，角色列表为空，创建角色");
                var createRole = C2R_CreateRole.Create();
                createRole.Token = response.Token;
                createRole.Account = account;
                createRole.ServerId = server.Id;
                createRole.Name = account;
                
                var createRoleResult = await clientSenderComponent.Call(createRole) as R2C_CreateRole;
            }
            else
            {
                Log.Info("获取角色列表成功");
            }
            
            await ETTask.CompletedTask;
        }
    }
}