using System.Net;
using System.Net.Sockets;
using ET;

namespace GameScripts.GameLogic
{
    [MessageHandler(SceneType.NetClient)]
    public class Main2NetClientLoginHandler : MessageHandler<Scene, Main2NetClient_Login, NetClient2Main_Login>
    {
        protected override async ETTask Run(Scene root, Main2NetClient_Login request, NetClient2Main_Login response)
        {
            string account = request.Account;
            string password = request.Password;

            root.RemoveComponent<RouterAddressComponent>();
            RouterAddressComponent routerAddressComponent = root.AddComponent<RouterAddressComponent, string, int>(ConstValue.RouterHttpHost, ConstValue.RouterHttpPort);
            await routerAddressComponent.Init();
            root.AddComponent<NetComponent, AddressFamily, NetworkProtocol>(routerAddressComponent.RouterManagerIPAddress.AddressFamily, NetworkProtocol.UDP);
            root.GetComponent<FiberParentComponent>().ParentFiberId = request.OwnerFiberId;

            NetComponent netComponent = root.GetComponent<NetComponent>();

            IPEndPoint realmAddress = routerAddressComponent.GetRealmAddress(account);

            var session = await netComponent.CreateRouterSession(realmAddress, account, password);

            var c2RLoginAccount = C2R_LoginAccount.Create();
            c2RLoginAccount.AccountName = account;
            c2RLoginAccount.Password = password;
            var r2CLoginAccount = (R2C_LoginAccount)await session.Call(c2RLoginAccount);

            if (r2CLoginAccount.Error != ErrorCode.ERR_Success) 
            {
                session?.Dispose();
            }
            else
            {
                root.AddComponent<SessionComponent>().Session = session;
            }

            response.Error = r2CLoginAccount.Error;
            response.Token = r2CLoginAccount.Token;

            // R2C_Login r2CLogin;
            // using (Session session = await netComponent.CreateRouterSession(realmAddress, account, password))
            // {
            //     C2R_Login c2RLogin = C2R_Login.Create();
            //     c2RLogin.Account = account;
            //     c2RLogin.Password = password;
            //     r2CLogin = (R2C_Login)await session.Call(c2RLogin);
            // }
            //
            // if (r2CLogin.Error != ErrorCode.ERR_Success)
            // {
            //     response.Error = r2CLogin.Error;
            //     return;
            // }
            //
            // // 创建一个gate Session,并且保存到SessionComponent中
            // Session gateSession = await netComponent.CreateRouterSession(NetworkHelper.ToIPEndPoint(r2CLogin.Address), account, password);
            // gateSession.AddComponent<ClientSessionErrorComponent>();
            // root.AddComponent<SessionComponent>().Session = gateSession;
            // C2G_LoginGate c2GLoginGate = C2G_LoginGate.Create();
            // c2GLoginGate.Key = r2CLogin.Key;
            // c2GLoginGate.GateId = r2CLogin.GateId;
            // G2C_LoginGate g2CLoginGate = (G2C_LoginGate)await gateSession.Call(c2GLoginGate);
            //
            // response.PlayerId = g2CLoginGate.PlayerId;
            await ETTask.CompletedTask;
        }
    }
}