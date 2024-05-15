namespace ET.Server;

[MessageSessionHandler(SceneType.Gate)]
public class C2G_LoginGameGateHandler: MessageSessionHandler<C2G_LoginGameGate, G2C_LoginGameGate>
{
    protected override async ETTask Run(Session session, C2G_LoginGameGate request, G2C_LoginGameGate response)
    {
        Scene root = session.Root();
        string account = root.GetComponent<GateSessionKeyComponent>().Get(request.Key);
        if (account == null)
        {
            response.Error = ErrorCore.ERR_ConnectGateKeyError;
            return;
        }
            
        session.RemoveComponent<SessionAcceptTimeoutComponent>();

        PlayerComponent playerComponent = root.GetComponent<PlayerComponent>();
        Player player = playerComponent.GetByAccount(account);
        if (player == null)
        {
            player = playerComponent.AddChild<Player, string,long>(account, request.RoleId);
            playerComponent.Add(player);
            PlayerSessionComponent playerSessionComponent = player.AddComponent<PlayerSessionComponent>();
            playerSessionComponent.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.GateSession);
            await playerSessionComponent.AddLocation(LocationType.GateSession);
			
            player.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.UnOrderedMessage);
            await player.AddLocation(LocationType.Player);
			
            session.AddComponent<SessionPlayerComponent>().Player = player;
            playerSessionComponent.Session = session;
        }
        else
        {
            PlayerSessionComponent playerSessionComponent = player.GetComponent<PlayerSessionComponent>();
            playerSessionComponent.Session = session;
        }

        response.PlayerId = player.Id;
        
        var unitComponent = root.GetComponent<UnitComponent>();
        var unit = unitComponent.AddChildWithId<Unit, int>(player.Id, 1001);
        await UnitCacheHelper.AddOrUpdateUnitAllCache(unit);
    }
}