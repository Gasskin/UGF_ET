namespace ET.Server;

[Invoke((long)SceneType.UnitCache)]
public class FiberInit_UnitCache:AInvokeHandler<FiberInit, ETTask>
{
    public override async ETTask Handle(FiberInit args)
    {
        Scene root = args.Fiber.Root;
        root.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.UnOrderedMessage);
        root.AddComponent<TimerComponent>();
        root.AddComponent<CoroutineLockComponent>();
        root.AddComponent<ProcessInnerSender>();
        root.AddComponent<MessageSender>();
        root.AddComponent<DBManagerComponent>();
        root.AddComponent<UnitCacheComponent>();
        
        await ETTask.CompletedTask;
    }
}