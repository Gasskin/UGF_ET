namespace ET.Server
{
    [FriendOf(typeof (Account))]
    [MessageSessionHandler(SceneType.Realm)]
    public class C2R_LoginAccountHandler: MessageSessionHandler<C2R_LoginAccount, R2C_LoginAccount>
    {
        protected override async ETTask Run(Session session, C2R_LoginAccount request, R2C_LoginAccount response)
        {
            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            if (session.GetComponent<SessionLockComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeated;
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
                using (await coroutineLock.Wait(CoroutineLockType.LoginAccount, request.AccountName.GetLongHashCode()))
                {
                    var dbComponent = session.Root().GetComponent<DBManagerComponent>().GetZoneDB(session.Zone());
                    var infos = await dbComponent.Query<Account>(info => info.AccountName == request.AccountName);

                    Account account = null;
                    if (infos is { Count: > 0 })
                    {
                        account = infos[0];

                        session.AddChild(account);
                        if (account.AccountType == (int)AccountType.BlackList)
                        {
                            response.Error = ErrorCode.ERR_LoginAccountInBlackList;
                            session.Disconnect().Coroutine();
                            account?.Dispose();
                            return;
                        }

                        if (account.Password != request.Password)
                        {
                            response.Error = ErrorCode.ERR_LoginPasswordError;
                            session.Disconnect().Coroutine();
                            account?.Dispose();
                            return;
                        }
                    }
                    else
                    {
                        account = session.AddChild<Account>();
                        account.AccountName = request.AccountName;
                        account.Password = request.Password;
                        account.CreateTime = TimeInfo.Instance.ServerNow();
                        account.AccountType = (int)AccountType.General;
                        await dbComponent.Save(account);
                    }

                    var loginAccountRequest = R2L_LoginAccountRequest.Create();
                    loginAccountRequest.AccountName = request.AccountName;
                    var loginCenterConfig = cfg.StartSceneTable.Instance.LoginCenterConfig;
                    var loginAccountRequestResponse = await session.Root().GetComponent<MessageSender>().Call(loginCenterConfig.ActorId, loginAccountRequest) as L2R_LoginAccountRequest;
                    
                    if (loginAccountRequestResponse.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = loginAccountRequestResponse.Error;
                        session.Disconnect().Coroutine();
                        account?.Dispose();
                        return;
                    }

                    var accountSessionsComponent = session.Root().GetComponent<AccountSessionsComponent>();
                    var otherSession = accountSessionsComponent.Get(request.AccountName);
                    otherSession?.Send(A2C_Disconnect.Create());
                    otherSession?.Disconnect().Coroutine();
                    
                    accountSessionsComponent.Add(request.AccountName, session);
                    var token = TimeInfo.Instance.ServerNow().ToString() + RandomGenerator.RandomNumber(int.MinValue, int.MaxValue);
                    var tokenComponent = session.Root().GetComponent<TokenComponent>();
                    tokenComponent.Remove(request.AccountName);
                    tokenComponent.Add(request.AccountName, token);

                    response.Token = token;
                    account?.Dispose();
                }
            }

            await ETTask.CompletedTask;
        }
    }
}