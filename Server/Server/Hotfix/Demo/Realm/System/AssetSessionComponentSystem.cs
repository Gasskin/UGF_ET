namespace ET.Server;

[FriendOf(typeof(AccountSessionsComponent))]
[EntitySystemOf(typeof(AccountSessionsComponent))]
public static partial class AccountSessionsComponentSystem
{
    [EntitySystem]
    private static void Awake(this ET.Server.AccountSessionsComponent self)
    {

    }
    [EntitySystem]
    private static void Destroy(this ET.Server.AccountSessionsComponent self)
    {
        self.AccountSessionDic.Clear();
    }
    
    public static Session Get(this ET.Server.AccountSessionsComponent self, string account)
    {
        if (self.AccountSessionDic.TryGetValue(account, out var session))
            return session;
        return null;
    }
    
    public static void Add(this ET.Server.AccountSessionsComponent self, string account, EntityRef<Session> session)
    {
        self.AccountSessionDic[account] = session;
    }
    
    public static void Remove(this ET.Server.AccountSessionsComponent self, string account)
    {
        self.AccountSessionDic.Remove(account);
    }
}