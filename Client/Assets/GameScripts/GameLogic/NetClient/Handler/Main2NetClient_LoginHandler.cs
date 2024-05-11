using System.Net;
using System.Net.Sockets;
using ET;

namespace GameScripts.GameLogic
{
    [MessageHandler(SceneType.NetClient)]
    public class Main2NetClient_LoginHandler : MessageHandler<Scene, Main2NetClient_Login, NetClient2Main_Login>
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

            var loginAccount = C2R_LoginAccount.Create();
            loginAccount.AccountName = account;
            loginAccount.Password = password;
            var loginAccountResponse = (R2C_LoginAccount)await session.Call(loginAccount);

            if (loginAccountResponse.Error != ErrorCode.ERR_Success) 
            {
                session?.Dispose();
            }
            else
            {
                root.AddComponent<SessionComponent>().Session = session;
            }

            response.Error = loginAccountResponse.Error;
            response.Token = loginAccountResponse.Token;

            await ETTask.CompletedTask;
        }
    }
}