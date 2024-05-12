using cfg;

namespace ET.Server;

public static class UnitCacheHelper
{
    public static async ETTask AddOrUpdateUnitCache<T>(this T self) where T : Entity, IUnitCache
    {
        var message = A2U_AddOrUpdateUnitCache.Create();
        message.EntityTypes.Add(typeof(T).FullName);
        message.EntityBytes.Add(MongoHelper.Serialize(self));

        var messageSender = self.Root().GetComponent<MessageSender>();
        await messageSender.Call(StartSceneTable.Instance.UnitCacheConfig.ActorId, message);
    }
}
