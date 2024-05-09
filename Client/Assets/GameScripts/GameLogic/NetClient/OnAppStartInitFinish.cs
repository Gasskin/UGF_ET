namespace ET
{
    [Event(SceneType.Main)]
    public class OnAppStartInitFinish : AEvent<Scene, AppStartInitFinish>
    {
        protected override async ETTask Run(Scene root, AppStartInitFinish args)
        {
            await ETTask.CompletedTask;

            Log.Warning("On AppStartInit Finish");

            LoginHelper.Login(root,"","").Coroutine();
        }
    }

}