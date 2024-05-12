namespace ET.Server;

[MessageHandler(SceneType.UnitCache)]
public class A2U_AddOrUpdateUnitCacheHandler: MessageHandler<Scene, A2U_AddOrUpdateUnitCache, U2A_AddOrUpdateUnitCache>
{
    protected override async ETTask Run(Scene scene, A2U_AddOrUpdateUnitCache request, U2A_AddOrUpdateUnitCache response)
    {
        var t = MongoHelper.Deserialize<TestEntity>(request.EntityBytes[0]);
        Log.Error(MongoHelper.ToJson(t));
        await ETTask.CompletedTask;
    }
}