namespace ET.Server;

[FriendOf(typeof(TokenComponent))]
[EntitySystemOf(typeof(TokenComponent))]
public static partial class TokenComponentSystem
{
    [EntitySystem]
    private static void Awake(this ET.Server.TokenComponent self)
    {

    }
    
    public static void Add(this ET.Server.TokenComponent self, string key, string token)
    {
        self.TokenDic[key] = token;
        self.TimeOut(key, token).Coroutine();
    }
    
    public static string Get(this ET.Server.TokenComponent self, string key)
    {
        if (self.TokenDic.TryGetValue(key, out var token))
            return token;
        return string.Empty;
    }
    
    public static void Remove(this ET.Server.TokenComponent self, string key)
    {
        self.TokenDic.Remove(key);
    }

    private static async ETTask TimeOut(this ET.Server.TokenComponent self, string key, string token)
    {
        await self.Root().GetComponent<TimerComponent>().WaitAsync(600000);

        var onlineKey = self.Get(key);
        if (!string.IsNullOrEmpty(onlineKey) && onlineKey == token)
        {
            self.Remove(key);
        }
    }
}