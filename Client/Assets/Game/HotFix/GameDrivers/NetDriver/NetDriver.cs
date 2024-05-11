using System;
using System.Reflection;
using Cysharp.Threading.Tasks;
using ET;

namespace Game.HotFix.GameDrivers
{
    public class NetDriver: GameDriverBase
    {
        public override int Priority => 0;
        public override async UniTask InitAsync()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => { Log.Error(e.ExceptionObject.ToString()); };

            World.Instance.AddSingleton<ET.Logger>().Log = new UnityLogger();
            ETTask.ExceptionHandler += Log.Error;

            World.Instance.AddSingleton<TimeInfo>();
            World.Instance.AddSingleton<FiberManager>();

            World.Instance.AddSingleton<CodeTypes, Assembly[]>(new[]
            {
                // Game.HotFix (Assembly)
                this.GetType().Assembly,
                // ET (Assembly)
                typeof(ET.ETLibrary).Assembly,
            });
            
            await UniTask.CompletedTask;
            
            // 注册Entity序列化器
            EntitySerializeRegister.Init();
            World.Instance.AddSingleton<IdGenerater>();
            World.Instance.AddSingleton<OpcodeType>();
            World.Instance.AddSingleton<ObjectPool>();
            World.Instance.AddSingleton<MessageQueue>();
            World.Instance.AddSingleton<NetServices>();
            World.Instance.AddSingleton<LogMsg>();

            // 创建需要reload的code singleton
            CodeTypes.Instance.CreateCode();

            // await World.Instance.AddSingleton<ConfigLoader>().LoadAsync();

            await FiberManager.Instance.Create(SchedulerType.Main, ConstFiberId.Main, 0, SceneType.Main, "");
        }

        public override void Update()
        {
        }

        public override void LateUpdate()
        {
        }

        public override void FixedUpdate()
        {
        }

        public override void ShutDown()
        {
        }
    }
}