using System.Collections.Generic;

namespace ET.Server;

public interface IUnitCache
{
    
}


[ChildOf(typeof(UnitCacheComponent))]
public class UnitCache:Entity,IAwake,IDestroy
{
    public string Key;
    public Dictionary<long, EntityRef<Entity>> CacheComponentDic = new ();
}