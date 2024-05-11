using System.Collections.Generic;

namespace ET.Server;

[ComponentOf(typeof(Scene))]
public class LoginInfoComponent:Entity,IAwake,IDestroy
{
    public Dictionary<long, int> LoginInfoDic = new();
}