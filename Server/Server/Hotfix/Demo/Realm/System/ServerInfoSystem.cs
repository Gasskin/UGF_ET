namespace ET.Server;

[FriendOf(typeof(ServerInfo))]
[EntitySystemOf(typeof(ServerInfo))]
public static partial class ServerInfoSystem
{
    [EntitySystem]
    private static void Awake(this ET.Server.ServerInfo self)
    {

    }
    
    public static void FromMessage(this ServerInfo self, ServerInfoProto message)
    {
        self.Status = message.Status;
        self.ServerName = message.ServerName;
    }
    
    public static ServerInfoProto ToMessage(this ServerInfo self)
    {
        ServerInfoProto message = ServerInfoProto.Create();
        message.Id = (int)self.Id;
        message.Status = self.Status;
        message.ServerName = self.ServerName;
        return message;
    }
}