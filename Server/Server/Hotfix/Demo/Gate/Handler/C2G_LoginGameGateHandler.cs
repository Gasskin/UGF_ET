using cfg;

namespace ET.Server;

[FriendOf(typeof(Player))]
[MessageSessionHandler(SceneType.Gate)]
public class C2G_LoginGameGateHandler: MessageSessionHandler<C2G_LoginGameGate, G2C_LoginGameGate>
{
    protected override async ETTask Run(Session session, C2G_LoginGameGate request, G2C_LoginGameGate response)
    {
        if (session.GetComponent<SessionLockComponent>() != null)
        {
            response.Error = ErrorCode.ERR_LoginRequestRepeated;
            session.Disconnect().Coroutine();
            return;
        }

        Scene root = session.Root();
        string account = root.GetComponent<GateSessionKeyComponent>().Get(request.Key);
        if (account == null)
        {
            response.Error = ErrorCore.ERR_ConnectGateKeyError;
            return;
        }

        session.RemoveComponent<SessionAcceptTimeoutComponent>();
        using (session.AddComponent<SessionLockComponent>())
        using (await root.GetComponent<CoroutineLockComponent>().Wait(CoroutineLockType.LoginGate, request.AccountName.GetLongHashCode()))
        {
            var addLoginRecordRequest = G2L_AddLoginRecord.Create();
            addLoginRecordRequest.Account = request.AccountName;
            addLoginRecordRequest.ServerId = root.Zone();
            var addLoginRecordResponse = await root.GetComponent<MessageSender>().Call(StartSceneTable.Instance.LoginCenterConfig.ActorId, addLoginRecordRequest) as L2G_AddLoginRecord;
            if (addLoginRecordResponse.Error != ErrorCode.ERR_Success)
            {
                response.Error = addLoginRecordResponse.Error;
                session?.Disconnect().Coroutine();
                return;
            }
            
            PlayerComponent playerComponent = root.GetComponent<PlayerComponent>();
            Player player = playerComponent.GetByAccount(account);
            if (player == null)
            {
                player = playerComponent.AddChildWithId<Player, string, long>(request.RoleId, account, request.RoleId);
                playerComponent.Add(player);
                PlayerSessionComponent playerSessionComponent = player.AddComponent<PlayerSessionComponent>();
                playerSessionComponent.Session = session;
                playerSessionComponent.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.GateSession);
                await playerSessionComponent.AddLocation(LocationType.GateSession);
			
                player.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.UnOrderedMessage);
                await player.AddLocation(LocationType.Player);
			
                session.AddComponent<SessionPlayerComponent>().Player = player;
                player.PlayerState = PlayerState.Gate;
            }
            else
            {
                player.RemoveComponent<PlayerOfflineOutTimeComponent>();
                PlayerSessionComponent playerSessionComponent = player.GetComponent<PlayerSessionComponent>();
                playerSessionComponent.Session = session;
            }

            response.PlayerId = player.Id;
        
            var unitComponent = root.GetComponent<UnitComponent>();
            var unit = unitComponent.AddChildWithId<Unit, int>(player.Id, 1001);
            await UnitCacheHelper.AddOrUpdateUnitAllCache(unit);
        }
    }
}