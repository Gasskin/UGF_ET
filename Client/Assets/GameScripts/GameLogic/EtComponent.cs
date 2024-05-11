using ET;
using System;
using System.Reflection;
using UnityEngine;

public class EtComponent : MonoBehaviour
{
    private void Start()
    {
        this.StartAsync().Coroutine();
    }

    private async ETTask StartAsync()
    {
        AppDomain.CurrentDomain.UnhandledException += (sender, e) => { Log.Error(e.ExceptionObject.ToString()); };

        World.Instance.AddSingleton<ET.Logger>().Log = new UnityLogger();
        ETTask.ExceptionHandler += Log.Error;

        World.Instance.AddSingleton<TimeInfo>();
        World.Instance.AddSingleton<FiberManager>();

        await ETTask.CompletedTask;

        Log.Debug($"StartAsync");

        // GameNetty.Runtime (Assembly)
        Assembly runtime = typeof(Entry).Assembly;

        World.Instance.AddSingleton<CodeTypes, Assembly[]>(new[]
        {
            // GameNetty.Runtime (Assembly)
            typeof(Entry).Assembly,
            // Assembly-CSharp (Assembly)
            typeof(EtComponent).Assembly,
            // GameProto (Assembly)
            typeof(C2G_Ping).Assembly,
            // GameLogic (Assembly)
            typeof(PingComponent).Assembly
        });

        IStaticMethod start = new StaticMethod(runtime, "ET.Entry", "Start");
        start.Run();
    }

    private void Update()
    {
        TimeInfo.Instance.Update();
        FiberManager.Instance.Update();
    }

    private void LateUpdate()
    {
        FiberManager.Instance.LateUpdate();
    }

    private void OnApplicationQuit()
    {
        World.Instance.Dispose();
    }
}