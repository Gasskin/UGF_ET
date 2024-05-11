namespace ET.Server;

[MessageSessionHandler(SceneType.Realm)]
[FriendOf(typeof(RoleInfo))]
public class C2R_GetRolesHandler:  MessageSessionHandler<C2R_GetRoles, R2C_GetRoles>
{
    protected override async ETTask Run(Session session, C2R_GetRoles request, R2C_GetRoles response)
    {
        if (session.GetComponent<SessionLockComponent>() != null)
        {
            response.Error = ErrorCode.ERR_LoginRequestRepeated;
            session.Disconnect().Coroutine();
            return;
        }

        var token = session.Root().GetComponent<TokenComponent>().Get(request.Account);
        if (string.IsNullOrEmpty(token)|| token != request.Token)
        {
            response.Error = ErrorCode.ERR_LoginTokenError;
            session.Disconnect().Coroutine();
            return;
        }

        var coroutineLockComponent = session.Root().GetComponent<CoroutineLockComponent>();
        using (session.AddComponent<SessionLockComponent>())
        {
            using (await coroutineLockComponent.Wait(CoroutineLockType.CreateRole, request.Account.GetLongHashCode()))
            {
                var dbComponent = session.Root().GetComponent<DBManagerComponent>().GetZoneDB(session.Zone());
                var roleInfos = await dbComponent.Query<RoleInfo>(
                    info => info.Account == request.Account
                        && info.ServerId == request.ServerId
                        && info.State == (int)RoleInfoState.Normal);

                if (roleInfos is not { Count: > 0 }) 
                    return;

                foreach (RoleInfo roleInfo in roleInfos)
                {
                    response.RoleInfoList.Add(roleInfo.ToMessage());
                    roleInfo?.Dispose();
                }
                roleInfos.Clear();
            }
        }
        
        await ETTask.CompletedTask;
    }
}