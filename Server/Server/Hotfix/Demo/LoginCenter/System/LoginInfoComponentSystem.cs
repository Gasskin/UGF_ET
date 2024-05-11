using System.Collections.Generic;

namespace ET.Server;

[FriendOf(typeof(LoginInfoComponent))]
[EntitySystemOf(typeof(LoginInfoComponent))]
public static partial class LoginInfoComponentSystem
{
    [EntitySystem]
    private static void Awake(this ET.Server.LoginInfoComponent self)
    {

    }
    [EntitySystem]
    private static void Destroy(this ET.Server.LoginInfoComponent self)
    {
        self.LoginInfoDic.Clear();
    }
    
    public static void Add(this ET.Server.LoginInfoComponent self, long key, int zone)
    {
        self.LoginInfoDic[key] = zone;
    }
    
    public static void Remove(this ET.Server.LoginInfoComponent self, long key)
    {
        self.LoginInfoDic.Remove(key);
    }
    
    public static int Get(this ET.Server.LoginInfoComponent self, long key)
    {
        return self.LoginInfoDic.GetValueOrDefault(key, -1);
    }
}