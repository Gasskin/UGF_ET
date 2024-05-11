using System.Net;
using System.Net.Sockets;
using ET;

namespace GameScripts.GameLogic
{
    [MessageHandler(SceneType.NetClient)]
    public class Main2NetClient_LoginGameHandler : MessageHandler<Scene, Main2NetClient_LoginGame, NetClient2Main_LoginGame>
    {
        protected override async ETTask Run(Scene root, Main2NetClient_LoginGame request, NetClient2Main_LoginGame response)
        {
            var account = request.Account;
            var netComponent = root.GetComponent<NetComponent>();
            var gateSession = await netComponent.CreateRouterSession(NetworkHelper.ToIPEndPoint(request.GateAddress), account, account);
            gateSession.AddComponent<ClientSessionErrorComponent>();
            root.GetComponent<SessionComponent>().Session = gateSession;

            var loginGameGate = C2G_LoginGameGate.Create();
            loginGameGate.AccountName = account;
            loginGameGate.Key = request.RealmKey;
            loginGameGate.RoleId = request.RoleId;
            var loginGameGateResponse = await gateSession.Call(loginGameGate) as G2C_LoginGameGate;
            if (loginGameGateResponse == null || loginGameGateResponse.Error != ErrorCode.ERR_Success)
            {
                if (loginGameGateResponse != null) 
                    response.Error = loginGameGateResponse.Error;
                Log.Error("登录网关失败");
                return;
            }

            var enterGameResponse = await gateSession.Call(C2G_EnterGame.Create());
            if (enterGameResponse==null|| enterGameResponse.Error!= ErrorCode.ERR_Success)
            {
                if (enterGameResponse != null) 
                    response.Error = enterGameResponse.Error;
                Log.Error("进入游戏失败");
                return;
            }
            
            Log.Info("进入Map");

            await ETTask.CompletedTask;
        }
    }
}