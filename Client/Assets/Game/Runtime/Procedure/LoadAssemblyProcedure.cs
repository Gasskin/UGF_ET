using System.Linq;
using System.Reflection;
using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;

namespace Game.Runtime
{
    public class LoadAssemblyProcedure : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            var assembly = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "Game.HotFix");

            var cls = assembly.GetType("Game.HotFix.HotFixEntry");
            if (cls != null)
            {
                var method = cls.GetMethod("Start", BindingFlags.Public | BindingFlags.Static);
                if (method != null)
                {
                    method.Invoke(null, null);
                }
                else
                {
                    Log.Fatal("加载HotFix.dll失败，不存在入口方法");
                }
            }
        }
    }
}