namespace ET
{
    [MessageHandler(SceneType.All)]
    public class NetClient2Main_SessionDisposeHandler: MessageHandler<Scene, NetClient2Main_SessionDispose>
    {
        protected override async ETTask Run(Scene entity, NetClient2Main_SessionDispose message)
        {
            ELog.Error($"session dispose, error: {message.Error}");
            await ETTask.CompletedTask;
        }
    }
}