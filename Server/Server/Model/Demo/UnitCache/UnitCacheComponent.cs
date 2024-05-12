using System.Collections.Generic;

namespace ET.Server;

public class UnitCacheComponent:Entity,IAwake,IDestroy
{
    public Dictionary<string, EntityRef<UnitCache>> UnitCacheDic = new();
    public List<string> UnitCacheKeyList = new();
}