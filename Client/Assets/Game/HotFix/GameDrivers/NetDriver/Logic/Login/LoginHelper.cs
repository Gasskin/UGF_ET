namespace ET
{
    public static class LoginHelper
    {
        public static async ETTask Login(Scene root, string account, string password)
        {
            root.RemoveComponent<ClientSenderComponent>();

            ClientSenderComponent clientSenderComponent = root.AddComponent<ClientSenderComponent>();

            var loginResponse = await clientSenderComponent.LoginAsync(account, password);
            if (loginResponse.Error != ErrorCode.ERR_Success)
                return;
            ELog.Info($"登录Realm服成功 Token:{loginResponse.Token}");


            var getServerInfos = C2R_GetServerInfos.Create();
            getServerInfos.Token = loginResponse.Token;
            getServerInfos.Account = account;

            var getServerInfosResponse = await clientSenderComponent.Call(getServerInfos) as R2C_GetServerInfos;
            if (getServerInfosResponse == null || getServerInfosResponse.Error != ErrorCode.ERR_Success)
                return;

            if (getServerInfosResponse.ServerInfoList.Count <= 0)
            {
                ELog.Error("服务器列表为空");
                return;
            }
            ELog.Info("获取服务器列表成功");

            var server = getServerInfosResponse.ServerInfoList[0];

            var getRoles = C2R_GetRoles.Create();
            getRoles.Token = loginResponse.Token;
            getRoles.Account = account;
            getRoles.ServerId = server.Id;
            var getRolesResponse = await clientSenderComponent.Call(getRoles) as R2C_GetRoles;
            if (getRolesResponse == null || getRolesResponse.Error != ErrorCode.ERR_Success)
                return;

            RoleInfoProto roleInfo;
            if (getRolesResponse.RoleInfoList.Count <= 0)
            {
                ELog.Info("获取角色列表成功，角色列表为空，创建角色");
                var createRole = C2R_CreateRole.Create();
                createRole.Token = loginResponse.Token;
                createRole.Account = account;
                createRole.ServerId = server.Id;
                createRole.Name = account;

                var createRoleResponse = await clientSenderComponent.Call(createRole) as R2C_CreateRole;
                if (createRoleResponse == null || createRoleResponse.Error != ErrorCode.ERR_Success)
                {
                    ELog.Error("创建角色失败");
                    return;
                }

                roleInfo = createRoleResponse.RoleInfoProto;
            }
            else
            {
                ELog.Info("获取角色列表成功");
                roleInfo = getRolesResponse.RoleInfoList[0];
            }

            // var getRealmKey = C2R_GetRealmKey.Create();
            // getRealmKey.Account = account;
            // getRealmKey.Token = loginResponse.Token;
            // getRealmKey.ServerId = server.Id;
            // var getRealmKeyResponse = await clientSenderComponent.Call(getRealmKey) as R2C_GetRealmKey;
            // if (getRealmKeyResponse == null || getRealmKeyResponse.Error != ErrorCode.ERR_Success)
            // {
            //     Log.Error("请求网关失败");
            //     return;
            // }
            //
            // var loginGameResponse = await clientSenderComponent.LoginGameAsync(account, getRealmKeyResponse.key, roleInfo.Id, getRealmKeyResponse.Address);
            // if (loginGameResponse.Error != ErrorCode.ERR_Success)
            //     return;

            // TODO 进入游戏
            ELog.Info("进入游戏");

            await ETTask.CompletedTask;
        }
    }
}