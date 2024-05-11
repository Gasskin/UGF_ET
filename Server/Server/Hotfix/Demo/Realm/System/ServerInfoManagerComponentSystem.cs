using cfg;

namespace ET.Server;

[FriendOf(typeof(ServerInfo))]
[FriendOf(typeof(ServerInfoManagerComponent))]
[EntitySystemOf(typeof(ServerInfoManagerComponent))]
public static partial class ServerInfoManagerComponentSystem
{
    [EntitySystem]
    private static void Awake(this ET.Server.ServerInfoManagerComponent self)
    {
        self.Load();
    }

    [EntitySystem]
    private static void Destroy(this ET.Server.ServerInfoManagerComponent self)
    {
        foreach (ServerInfo serverInfo in self.ServerInfoList)
        {
            serverInfo?.Dispose();
        }
        self.ServerInfoList.Clear();
    }

    public static void Load(this ET.Server.ServerInfoManagerComponent self)
    {
        foreach (ServerInfo serverInfo in self.ServerInfoList)
        {
            serverInfo?.Dispose();
        }
        self.ServerInfoList.Clear();

        var serverInfoConfigs = StartZoneTable.Instance.DataList;
        foreach (var serverInfoConfig in serverInfoConfigs)
        {
            if (serverInfoConfig.ZoneType != 1) 
                continue;
            var serverInfo = self.AddChildWithId<ServerInfo>(serverInfoConfig.Id);
            serverInfo.ServerName = serverInfoConfig.DBName;
            serverInfo.Status = (int)ServerStatus.Normal;
            self.ServerInfoList.Add(serverInfo);
        }
    }
}