using System;
using ET.Server;

namespace ET.Server;

[MessageHandler(SceneType.UnitCache)]
public class A2U_AddOrUpdateUnitCacheHandler: MessageHandler<Scene, A2U_AddOrUpdateUnitCache, U2A_AddOrUpdateUnitCache>
{
    protected override async ETTask Run(Scene scene, A2U_AddOrUpdateUnitCache request, U2A_AddOrUpdateUnitCache response)
    {
        UpdateUnitCacheAsync(scene,request).Coroutine();
        await ETTask.CompletedTask;
    }
    
    private async ETTask UpdateUnitCacheAsync(Scene scene, A2U_AddOrUpdateUnitCache request)
    {
        var unitCacheComponent = scene.GetComponent<UnitCacheComponent>();

        using (var entityList = ListComponent<Entity>.Create())
        {
            for (int i = 0; i < request.EntityTypes.Count; i++)
            {
                var type = CodeTypes.Instance.GetType(request.EntityTypes[i]);
                var entity = MongoHelper.Deserialize(type, request.EntityBytes[i]) as Entity;
                entityList.Add(entity);
            }

            await unitCacheComponent.AddOrUpdate(request.UnitId, entityList);
        }
    }
}