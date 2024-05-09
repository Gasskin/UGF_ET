namespace ET.Server
{
    [FriendOf(typeof (AccountInfo))]
    [MessageSessionHandler(SceneType.Realm)]
    public class C2R_LoginAccountHandler: MessageSessionHandler<C2R_LoginAccount, R2C_LoginAccount>
    {
        protected override async ETTask Run(Session session, C2R_LoginAccount request, R2C_LoginAccount response)
        {
            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            if (session.GetComponent<SessionLockComponent>() != null)
            {
                response.Error = ErrorCode.ERR_LoginRequestRepeated;
                session.Disconnect().Coroutine();
                return;
            }

            if (string.IsNullOrEmpty(request.AccountName) || string.IsNullOrEmpty(request.Password))
            {
                response.Error = ErrorCode.ERR_LoginInfoEmpty;
                session.Disconnect().Coroutine();
                return;
            }

            var coroutineLock = session.Root().GetComponent<CoroutineLockComponent>();
            using (session.AddComponent<SessionLockComponent>())
            {
                using (await coroutineLock.Wait(CoroutineLockType.LoginAccount,request.AccountName.GetLongHashCode()))
                {
                    var dbComponent = session.Root().GetComponent<DBManagerComponent>().GetZoneDB(session.Zone());
                    var infos = await dbComponent.Query<AccountInfo>(info => info.Account == request.AccountName);

                    if (infos.Count <= 0)
                    {
                        var accountInfosComponent = session.GetComponent<AccountInfosComponent>() ?? session.AddComponent<AccountInfosComponent>();
                        var accountInfo = accountInfosComponent.AddChild<AccountInfo>();
                        accountInfo.Account = request.AccountName;
                        accountInfo.Password = request.Password;
                        await dbComponent.Save(accountInfo);
                    }
                    else
                    {
                        var accountInfo = infos[0];
                        if (accountInfo.Password!=request.Password)
                        {
                            response.Error = ErrorCode.ERR_LoginPasswordError;
                            session.Disconnect().Coroutine();
                            return;
                        }
                    }
                }
            }
            
            await ETTask.CompletedTask;
        }
    }
}