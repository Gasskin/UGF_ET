using System.Collections.Generic;

namespace ET.Server;

[ComponentOf(typeof(Scene))]
public class AccountSessionsComponent: Entity,IAwake,IDestroy
{
    public Dictionary<string, EntityRef<Session>> AccountSessionDic = new();
}