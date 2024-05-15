using System.Collections.Generic;

namespace ET.Server;

[ComponentOf(typeof(Scene))]
public class UnitCacheComponent:Entity,IAwake,IDestroy
{
    public Dictionary<string, EntityRef<UnitCache>> UnitCacheDic = new();
    public List<string> UnitCacheKeyList = new();
}