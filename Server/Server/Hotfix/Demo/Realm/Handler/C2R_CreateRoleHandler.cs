namespace ET.Server;

[MessageSessionHandler(SceneType.Realm)]
[FriendOf(typeof(RoleInfo))]
public class C2R_CreateRoleHandler:  MessageSessionHandler<C2R_CreateRole, R2C_CreateRole>
{
    protected override async ETTask Run(Session session, C2R_CreateRole request, R2C_CreateRole response)
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
                        && info.Name == request.Name);

                if (roleInfos is { Count: > 0 })
                {
                    response.Error = ErrorCode.ERR_LoginCreateRoleNameSame;
                    session.Disconnect().Coroutine();
                    return;
                }
       
                var roleInfo = session.AddChild<RoleInfo>();
                roleInfo.Name = request.Name;
                roleInfo.State = (int)RoleInfoState.Normal;
                roleInfo.ServerId = request.ServerId;
                roleInfo.Account = request.Account;
                roleInfo.CreateTime = TimeInfo.Instance.ServerNow();
                roleInfo.LastLoginTime = 0;

                await dbComponent.Save(roleInfo);
                response.RoleInfoProto = roleInfo.ToMessage();
                roleInfo?.Dispose();
            }
        }
        await ETTask.CompletedTask;
    }
}