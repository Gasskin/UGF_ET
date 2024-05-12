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
        
        Test(root).Coroutine();
        
        await ETTask.CompletedTask;
    }

    private async ETTask Test(Scene root)
    {
        await root.GetComponent<TimerComponent>().WaitAsync(2000);
        var t = root.AddChild<TestEntity>();
        await t.AddOrUpdateUnitCache();
    }
}