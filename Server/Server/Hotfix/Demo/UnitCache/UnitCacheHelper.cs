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
}



