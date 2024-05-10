namespace ET.Server;

[Invoke((long)SceneType.LoginCenter)]
public class FiberInit_LoginCenter:AInvokeHandler<FiberInit, ETTask>
{
    public override async ETTask Handle(FiberInit args)
    {
        await ETTask.CompletedTask;
    }
}