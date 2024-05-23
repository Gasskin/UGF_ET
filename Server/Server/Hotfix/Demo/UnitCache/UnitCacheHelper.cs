using cfg;

namespace ET.Server;

public static class UnitCacheHelper
{
    /// <summary>
    /// 添加或者更新缓存
    /// </summary>
    /// <param name="self"></param>
    /// <typeparam name="T"></typeparam>
    public static async ETTask AddOrUpdateUnitCache<T>(this T self) where T : Entity, IUnitCache
    {
        var message = A2U_AddOrUpdateUnitCache.Create();
        message.EntityTypes.Add(typeof(T).FullName);
        message.EntityBytes.Add(MongoHelper.Serialize(self));

        var messageSender = self.Root().GetComponent<MessageSender>();
        await messageSender.Call(StartSceneTable.Instance.UnitCacheConfig.ActorId, message);
    }

    /// <summary>
    /// 获取组件缓存
    /// </summary>
    /// <param name="self"></param>
    /// <param name="unitId"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static async ETTask<T> GetUnitCacheComponent<T>(this T self,long unitId) where T : Entity, IUnitCache
    {
        var message = A2U_GetUnitCache.Create();
        message.UnitId = unitId;
        message.ComponentNameList.Add(typeof (T).Name);
        
        var messageSender = self.Root().GetComponent<MessageSender>();
        var response = await messageSender.Call(StartSceneTable.Instance.UnitCacheConfig.ActorId, message) as U2A_GetUnitCache;
        if (response.Error == ErrorCode.ERR_Success)
            return response.EntityList[0] as T;
        return null;
    }


    /// <summary>
    /// 删除玩家缓存
    /// </summary>
    /// <param name="self"></param>
    /// <param name="unitId"></param>
    /// <typeparam name="T"></typeparam>
    public static async ETTask DeleteUnitCache<T>(this T self,long unitId) where T : Entity, IUnitCache
    {
        var message = A2U_DeleteUnitCache.Create();
        message.UnitId = unitId;
        var messageSender = self.Root().GetComponent<MessageSender>();
        await messageSender.Call(StartSceneTable.Instance.UnitCacheConfig.ActorId, message); 
    }
    
    /// <summary>
    /// 保存Unit及Unit身上组件到缓存服及数据库中
    /// </summary>
    /// <param name="unit"></param>
    public static async ETTask AddOrUpdateUnitAllCache(Unit unit)
    {
        var message = A2U_AddOrUpdateUnitCache.Create();
        message.UnitId = unit.Id;
            
        message.EntityTypes.Add(unit.GetType().FullName);
        message.EntityBytes.Add(MongoHelper.Serialize(unit));

        foreach (var entity in unit.Components.Values)
        {
            if (!(entity is IUnitCache))
                continue;
            message.EntityTypes.Add(entity.GetType().FullName);
            message.EntityBytes.Add(MongoHelper.Serialize(entity));
        }
            
        var messageSender = unit.Root().GetComponent<MessageSender>();
        await messageSender.Call(StartSceneTable.Instance.UnitCacheConfig.ActorId, message);
    }
    
    /// <summary>
    /// 获取玩家缓存
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="unitId"></param>
    /// <returns></returns>
    public static async ETTask<Unit> GetUnitCache(Scene scene, long unitId)
    {
        var message = A2U_GetUnitCache.Create();
        message.UnitId = unitId;
        var messageSender = scene.Root().GetComponent<MessageSender>();
        var response = await messageSender.Call(StartSceneTable.Instance.UnitCacheConfig.ActorId, message) as U2A_GetUnitCache;
        if (response.Error != ErrorCode.ERR_Success || response.EntityList.Count <= 0)
            return null;

        int indexOf = response.ComponentNameList.IndexOf(nameof (Unit));
        var unit = response.EntityList[indexOf] as Unit;
        if (unit == null)
            return null;
        scene.AddChild(unit);
        foreach (Entity entity in response.EntityList)
        {
            if (entity == null || entity is Unit)
                continue;
            unit.AddComponent(entity);
        }
        return unit;
    }
}



