namespace ET.Server;

[FriendOf(typeof(RoleInfo))]
[EntitySystemOf(typeof(RoleInfo))]
public static partial class RoleInfoSystem
{
    [EntitySystem]
    private static void Awake(this ET.Server.RoleInfo self)
    {

    }
    
    public static void FromMessage(this RoleInfo self, RoleInfoProto message)
    {
        self.Name = message.Name;
        self.State = message.State;
        self.Account = message.Account;
        self.LastLoginTime = message.LastLoginTime;
        self.ServerId = message.ServerId;
        self.CreateTime = message.CreateTime;
    }
    
    public static RoleInfoProto ToMessage(this RoleInfo self)
    {
        RoleInfoProto message = RoleInfoProto.Create();
        message.Id = (int)self.Id;
        message.Name = self.Name;
        message.State = self.State;
        message.Account = self.Account;
        message.LastLoginTime = self.LastLoginTime;
        message.ServerId = self.ServerId;
        message.CreateTime = self.CreateTime;
        return message;
    }
}