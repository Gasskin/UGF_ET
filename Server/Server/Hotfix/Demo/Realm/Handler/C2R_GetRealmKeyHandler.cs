namespace ET.Server;

[MessageSessionHandler(SceneType.Realm)]
public class C2R_GetRealmKeyHandler:  MessageSessionHandler<C2R_GetRealmKey, R2C_GetRealmKey>
{
    protected override async ETTask Run(Session session, C2R_GetRealmKey request, R2C_GetRealmKey response)
    {
        if (session.GetComponent<SessionLockComponent>() != null)
        {
            response.Error = ErrorCode.ERR_RequestRepeated;
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
            using (await coroutineLockComponent.Wait(CoroutineLockType.LoginAccount, request.Account.GetLongHashCode()))
            {
                // 请求网关
                var gateConfig = RealmGateAddressHelper.GetGate(request.ServerId, request.Account);
                var getLoginKey = R2G_GetLoginKey.Create();
                getLoginKey.Account = request.Account;
                var getLoginKeyResponse = await session.Root().GetComponent<MessageSender>().Call(gateConfig.ActorId, getLoginKey) as G2R_GetLoginKey;
                response.Address = gateConfig.InnerIPPort.ToString();
                response.key = getLoginKeyResponse.Key;
                response.GateId = getLoginKeyResponse.GateId;
                
                session.Disconnect().Coroutine();
            }
        }
        
        await ETTask.CompletedTask;
    }
}