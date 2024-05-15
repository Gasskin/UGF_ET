using System;

namespace ET.Server;

[FriendOf(typeof(UnitCache))]
[FriendOf(typeof(UnitCacheComponent))]
[EntitySystemOf(typeof(UnitCacheComponent))]
public static partial class UnitCacheComponentSystem
{
    [EntitySystem]
    private static void Awake(this ET.Server.UnitCacheComponent self)
    {
        self.UnitCacheKeyList.Clear();
        foreach (Type type in CodeTypes.Instance.GetTypes().Values)
        {
            if (type != typeof (IUnitCache) && typeof (IUnitCache).IsAssignableFrom(type))
            {
                self.UnitCacheKeyList.Add(type.Name);
            }
        }
            
        foreach (string key in self.UnitCacheKeyList)
        {
            UnitCache unitCache = self.AddChild<UnitCache>();
            unitCache.Key = key;
            self.UnitCacheDic.Add(key, unitCache);
        }
    }
    [EntitySystem]
    private static void Destroy(this ET.Server.UnitCacheComponent self)
    {
        foreach (UnitCache unitCache in self.UnitCacheDic.Values)
        {
            unitCache?.Dispose();
        }
        self.UnitCacheDic.Clear();
    }

    public static async ETTask AddOrUpdate(this ET.Server.UnitCacheComponent self, long id, ListComponent<Entity> entityList)
    {
        using (var list = ListComponent<Entity>.Create())
        {
            foreach (var entity in entityList)
            {   
                var key = entity.GetType().Name;
                UnitCache unitCache = self.UnitCacheDic[key];
                if (unitCache == null) 
                {
                    unitCache = self.AddChild<UnitCache>();
                    unitCache.Key = key;
                    self.UnitCacheDic.Add(key, unitCache);
                }
                unitCache.AddOrUpdate(entity);
                list.Add(entity);
            }
            if (list.Count > 0)
            {
                var dbComponent = self.Root().GetComponent<DBManagerComponent>().GetZoneDB(self.Zone());
                await dbComponent.Save(id, list);
            }
        }
        await ETTask.CompletedTask;
    }
    
    public static async ETTask<Entity> Get(this ET.Server.UnitCacheComponent self,long unitId,string key)
    {
        UnitCache unitCache = self.UnitCacheDic[key];
        if (unitCache == null)
        {
            unitCache = self.AddChild<UnitCache>();
            unitCache.Key = key;
            self.UnitCacheDic.Add(key, unitCache);
        }
        return await unitCache.Get(unitId);
    }
    
    public static void Delete(this ET.Server.UnitCacheComponent self,long unitId)
    {
        foreach (UnitCache unitCache in self.UnitCacheDic.Values)
        {
            unitCache.Delete(unitId);
        }
    }
}













