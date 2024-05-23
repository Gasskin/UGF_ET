namespace ET.Server
{
	[FriendOf(typeof(Player))]
	[MessageSessionHandler(SceneType.Gate)]
	public class C2G_EnterMapHandler : MessageSessionHandler<C2G_EnterMap, G2C_EnterMap>
	{
		protected override async ETTask Run(Session session, C2G_EnterMap request, G2C_EnterMap response)
		{
			if (session.GetComponent<SessionLockComponent>() != null)
			{
				response.Error = ErrorCode.ERR_RequestRepeated;
				return;
			}
			
			var player = session.GetComponent<SessionPlayerComponent>().Player;
			var coroutineLockComponent = session.Root().GetComponent<CoroutineLockComponent>();

			using (session.AddComponent<SessionLockComponent>())
			{
				using (await coroutineLockComponent.Wait(CoroutineLockType.LoginGate, player.Account.GetLongHashCode()))
				{
					if (player.PlayerState == PlayerState.Game)
					{
						//TODO 玩家已经在游戏中了
						return;
					}
					
					//从数据库或者缓存中加载出Unit实体及其相关组件
					(bool isNewPlayer, Unit unit) = await UnitHelper.LoadUnit(player);
			
					response.MyId = player.Id;

					// 等到一帧的最后面再传送，先让G2C_EnterMap返回，否则传送消息可能比G2C_EnterMap还早
					var startSceneConfig = cfg.StartSceneTable.Instance.GetBySceneName(session.Zone(), "Map1");
					TransferHelper.TransferAtFrameFinish(unit, startSceneConfig.ActorId, startSceneConfig.Name).Coroutine();
				}
			}


		}
	}
}