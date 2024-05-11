using ET;
using Game.HotFix.GameDrivers;
using GameFramework.Fsm;
using GameFramework.Procedure;

namespace Game.HotFix.Procedure
{
    public class LoginProcedure: ProcedureBase
    {
        protected override async void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            var net = HotFixEntry.s_Instance.GetDriver<NetDriver>();
            if (net != null)
            {
                LoginHelper.Login(net.MainScene, "222", "333").Coroutine();
            }
        }
    }
}   