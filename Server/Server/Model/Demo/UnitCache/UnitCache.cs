using System.Collections.Generic;

namespace ET.Server;

public interface IUnitCache
{
    
}

[ChildOf(typeof(Scene))]
public class TestEntity :Entity,IUnitCache,IAwake
{
    public string Name = "TestEntity";
}

public class UnitCache:Entity,IAwake,IDestroy
{
    public string Key;
    public Dictionary<long, EntityRef<Entity>> CacheComponentDic = new ();
}