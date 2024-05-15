namespace ET.Server;

[FriendOf(typeof(UnitCacheComponent))]
[MessageHandler(SceneType.UnitCache)]
public class A2U_DeleteUnitCacheHandler: MessageHandler<Scene, A2U_DeleteUnitCache, U2A_DeleteUnitCache>
{
    protected override async ETTask Run(Scene scene, A2U_DeleteUnitCache request, U2A_DeleteUnitCache response)
    {
        var unitCacheComponent = scene.GetComponent<UnitCacheComponent>();
        unitCacheComponent.Delete(request.UnitId);
        await ETTask.CompletedTask;
    }
}