namespace ET.Server;


[Invoke(TimerInvokeType.AccountSessionCheckOutTime)]
public class AccountSessionCheckOutTimer: ATimer<AccountCheckOutTimeComponent>
{
    protected override void Run(AccountCheckOutTimeComponent t)
    {
    }
}



[FriendOf(typeof (AccountCheckOutTimeComponent))]
[EntitySystemOf(typeof (AccountCheckOutTimeComponent))]
public static partial class AccountCheckOutTimeComponentSystem
{
    [EntitySystem]
    private static void Awake(this ET.Server.AccountCheckOutTimeComponent self, string account)
    {
        self.Account = account;
        self.Root().GetComponent<TimerComponent>().Remove(ref self.Timer);
        var time = TimeInfo.Instance.ServerNow() + 600000;
        self.Timer = self.Root().GetComponent<TimerComponent>().NewOnceTimer(time, TimerInvokeType.AccountSessionCheckOutTime, self);
    }

    [EntitySystem]
    private static void Destroy(this ET.Server.AccountCheckOutTimeComponent self)
    {
        self.Root().GetComponent<TimerComponent>().Remove(ref self.Timer);
    }

    public static void DeleteSession(this AccountCheckOutTimeComponent self)
    {
        var session = self.GetParent<Session>();
        var originSession = session.Root().GetComponent<AccountSessionsComponent>().Get(self.Account);
        if (originSession != null && session.InstanceId == originSession.InstanceId)
        {
            session.Root().GetComponent<AccountSessionsComponent>().Remove(self.Account);
        }

        var a2CDisconnect = A2C_Disconnect.Create();
        a2CDisconnect.Error = 1;
        session?.Send(a2CDisconnect);
        session?.Disconnect().Coroutine();
    }
}