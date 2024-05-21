using NotImplementedException = System.NotImplementedException;

namespace ET.Server.Handler;

[MessageHandler(SceneType.LoginCenter)]
public class R2L_LoginAccountRequestHandler: MessageHandler<Scene,R2L_LoginAccountRequest,L2R_LoginAccountRequest>
{
    protected override async ETTask Run(Scene scene, R2L_LoginAccountRequest request, L2R_LoginAccountRequest response)
    {
        var key = request.AccountName.GetLongHashCode();
        var coroutineLockComponent = scene.GetComponent<CoroutineLockComponent>();
        using (await coroutineLockComponent.Wait(CoroutineLockType.LoginCenterLock,key))
        {
            var loginInfoComponent = scene.GetComponent<LoginInfoComponent>();
            var zone = loginInfoComponent.Get(key);
            if (zone <= 0) 
                return;
            // 已经登录的，踢掉
            var gateConfig = RealmGateAddressHelper.GetGate(zone, request.AccountName);
            var disconnectGateUnit = L2G_DisconnectGateUnit.Create();
            disconnectGateUnit.AccountName = request.AccountName;
            var disconnectGateUnitResponse = await scene.GetComponent<MessageSender>().Call(gateConfig.ActorId, disconnectGateUnit) as G2L_DisconnectGateUnit;
            response.Error = disconnectGateUnitResponse.Error;
        }
        
        await ETTask.CompletedTask;
    }
}