using GameFramework.Fsm;
using GameFramework.Procedure;

namespace Game.HotFix.Procedure
{
    public class LoginProcedure: ProcedureBase
    {
        protected override async void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            await HotFixEntry.s_Instance.AddDriver<GameDrivers.NetDriver>();
        }
    }
}