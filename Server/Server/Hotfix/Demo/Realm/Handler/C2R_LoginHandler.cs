using System;
using System.Net;


namespace ET.Server
{
	[FriendOf(typeof(Account))]
	[MessageSessionHandler(SceneType.Realm)]
	public class C2R_LoginHandler : MessageSessionHandler<C2R_Login, R2C_Login>
	{
		protected override async ETTask Run(Session session, C2R_Login request, R2C_Login response)
		{
			if (string.IsNullOrEmpty(request.Account) || string.IsNullOrEmpty(request.Password))
			{
				response.Error = ErrorCode.ERR_LoginInfoEmpty;
				CloseSession(session).Coroutine();
				return;
			}

			var dbComponent = session.Root().GetComponent<DBManagerComponent>().GetZoneDB(session.Zone());
			var infos = await dbComponent.Query<Account>(info => info.AccountName == request.Account);

			if (infos.Count <= 0)
			{
				// var accountInfosComponent = session.GetComponent<AccountInfosComponent>() ?? session.AddComponent<AccountInfosComponent>();
				// var accountInfo = accountInfosComponent.AddChild<Account>();
				// accountInfo.AccountName = request.Account;
				// accountInfo.Password = request.Password;
				// await dbComponent.Save(accountInfo);
			}
			else
			{
				var accountInfo = infos[0];
				if (accountInfo.Password!=request.Password)
				{
					response.Error = ErrorCode.ERR_LoginPasswordError;
					CloseSession(session).Coroutine();
					return;
				}
			}
			
			// 随机分配一个Gate
			cfg.StartSceneConfig config = RealmGateAddressHelper.GetGate(session.Zone(), request.Account);
			Log.Debug($"gate address: {config}");
			
			// 向gate请求一个key,客户端可以拿着这个key连接gate
			R2G_GetLoginKey r2GGetLoginKey = R2G_GetLoginKey.Create();
			r2GGetLoginKey.Account = request.Account;
			G2R_GetLoginKey g2RGetLoginKey = (G2R_GetLoginKey) await session.Fiber().Root.GetComponent<MessageSender>().Call(
				config.ActorId, r2GGetLoginKey);

			response.Address = config.InnerIPPort.ToString();
			response.Key = g2RGetLoginKey.Key;
			response.GateId = g2RGetLoginKey.GateId;
			
			CloseSession(session).Coroutine();
		}

		private async ETTask CloseSession(Session session)
		{
			await session.Root().GetComponent<TimerComponent>().WaitAsync(1000);
			session.Dispose();
		}
	}
}
