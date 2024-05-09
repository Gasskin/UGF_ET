using System.Net;

namespace ET.Server
{
    [Invoke((long)SceneType.RouterManager)]
    public class FiberInit_RouterManager: AInvokeHandler<FiberInit, ETTask>
    {
        public override async ETTask Handle(FiberInit fiberInit)
        {
            Scene root = fiberInit.Fiber.Root;
            cfg.StartSceneConfig startSceneConfig = cfg.StartSceneTable.Instance.Get((int)root.Id);
            root.AddComponent<HttpComponent, string>($"http://*:{startSceneConfig.Port}/");

            await ETTask.CompletedTask;
        }
    }
}