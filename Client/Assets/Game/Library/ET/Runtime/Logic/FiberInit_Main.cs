namespace ET
{
    [Invoke((long)SceneType.Main)]
    public class FiberInit_Main: AInvokeHandler<FiberInit, ETTask>
    {
        public override async ETTask Handle(FiberInit fiberInit)
        {
            Scene root = fiberInit.Fiber.Root;
            root.AddComponent<TimerComponent>();
            root.AddComponent<CoroutineLockComponent>();
            root.AddComponent<ObjectWait>();
            root.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.UnOrderedMessage);
            root.AddComponent<ProcessInnerSender>();
            root.AddComponent<CurrentScenesComponent>();
            
            await EventSystem.Instance.PublishAsync(root, new AppStartInitFinish());
        }
    }
}