namespace ET.Server;

[FriendOf(typeof (UnitCache))]
[EntitySystemOf(typeof (UnitCache))]
public static partial class UnitCacheSystem
{
    [EntitySystem]
    private static void Awake(this ET.Server.UnitCache self)
    {
    }

    [EntitySystem]
    private static void Destroy(this ET.Server.UnitCache self)
    {
        foreach (Entity entity in self.CacheComponentDic.Values)
        {
            entity.Dispose();
        }
        self.CacheComponentDic.Clear();
        self.Key = null;
    }

    public static void AddOrUpdate(this UnitCache self, Entity entity)
    {
        if (entity == null)
            return;

        if (self.CacheComponentDic.TryGetValue(entity.Id, out var e))
        {
            Entity oldEntity = (Entity)e;
            if (oldEntity != null && entity != oldEntity)
            {
                oldEntity.Dispose();
            }

            self.CacheComponentDic.Remove(entity.Id);
        }

        self.CacheComponentDic.Add(entity.Id, entity);
    }

    public static async ETTask<Entity> Get(this UnitCache self, long unitId)
    {
        Entity entity = self.CacheComponentDic[unitId];
        if (entity == null)
        {
            var dbComponent = self.Root().GetComponent<DBManagerComponent>().GetZoneDB(self.Zone());
            entity = await dbComponent.Query<Entity>(unitId, self.Key);
            if (entity != null) 
                self.AddOrUpdate(entity);
        }
        return entity;
    }
    
    public static void Delete(this ET.Server.UnitCache self,long unitId)
    {
        if (self.CacheComponentDic.TryGetValue(unitId, out var u))
        {
            ((UnitCache)u)?.Dispose();
            self.CacheComponentDic.Remove(unitId);
        }
    }
}













