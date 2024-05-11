using System;
using System.Reflection;
using Cysharp.Threading.Tasks;
using ET;

namespace Game.HotFix.GameDrivers
{
    public class NetDriver: GameDriverBase
    {
        public override int Priority => 0;

        public Scene MainScene { get; private set; }
        
        private int m_MainFiberId;
        
        public override async UniTask InitAsync()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => { ELog.Error(e.ExceptionObject.ToString()); };

            World.Instance.AddSingleton<ET.Logger>().Log = new UnityLogger();
            ETTask.ExceptionHandler += ELog.Error;

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

            m_MainFiberId = await FiberManager.Instance.Create(SchedulerType.Main, ConstFiberId.Main, 0, SceneType.Main, "");
        }

        public override void Update()
        {
            TimeInfo.Instance.Update();
            FiberManager.Instance.Update();
        }

        public override void LateUpdate()
        {
            FiberManager.Instance.LateUpdate();
        }

        public override void FixedUpdate()
        {
        }

        public override void ShutDown()
        {
        }

        public void SetMainScene(Scene scene)
        {
            MainScene = scene;
        }
    }
}