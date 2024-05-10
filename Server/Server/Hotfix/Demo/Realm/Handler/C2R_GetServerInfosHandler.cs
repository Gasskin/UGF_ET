namespace ET.Server;

[FriendOf(typeof(ServerInfoManagerComponent))]
[MessageSessionHandler(SceneType.Realm)]
public class C2R_GetServerInfosHandler:MessageSessionHandler<C2R_GetServerInfos, R2C_GetServerInfos>
{
    protected override async ETTask Run(Session session, C2R_GetServerInfos request, R2C_GetServerInfos response)
    {
        var token = session.Root().GetComponent<TokenComponent>().Get(request.Account);
        if (string.IsNullOrEmpty(token)|| token != request.Token)
        {
            response.Error = ErrorCode.ERR_LoginTokenError;
            session.Disconnect().Coroutine();
            return;
        }

        foreach (ServerInfo serverInfo in session.Root().GetComponent<ServerInfoManagerComponent>().ServerInfoList)
        {
            response.ServerInfoList.Add(serverInfo.ToMessage());
        }
        
        await ETTask.CompletedTask;
    }
}