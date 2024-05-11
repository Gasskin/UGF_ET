using System.Collections.Generic;

namespace ET.Server;

[ComponentOf(typeof(Scene))]
public class ServerInfoManagerComponent:Entity,IAwake,IDestroy
{
    public List<EntityRef<ServerInfo>> ServerInfoList = new();
}