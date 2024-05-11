using System;
using System.Reflection;
using Cysharp.Threading.Tasks;
using ET;
using GameFramework.Fsm;
using GameFramework.Procedure;
using Log = UnityGameFramework.Runtime.Log;

namespace Game.Runtime
{
    public class InitEtProcedure : ProcedureBase
    {
        protected override async void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                Log.Error(e.ExceptionObject.ToString());
            };

            // World.Instance.AddSingleton<ET.Logger>().Log = new UnityLogger();
            ETTask.ExceptionHandler += (exception =>
            {
                Log.Error(exception);
            });

            World.Instance.AddSingleton<TimeInfo>();
            World.Instance.AddSingleton<FiberManager>();

            await ETTask.CompletedTask;

            Log.Debug($"StartAsync");

            World.Instance.AddSingleton<CodeTypes, Assembly[]>(new[]
            {
                // Game.Runtime (Assembly)
                this.GetType().Assembly,
            });
            
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
    }
}