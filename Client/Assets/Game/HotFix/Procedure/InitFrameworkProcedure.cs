using Game.HotFix.GameDrivers;
using GameFramework.Fsm;
using GameFramework.Procedure;

namespace Game.HotFix.Procedure
{
    public class InitFrameworkProcedure: ProcedureBase
    {
        protected override async void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            var m_Owner = procedureOwner;
            
            var instance = HotFixEntry.s_Instance;
            
            await instance.AddDriver<NetDriver>();
            
            ChangeState<LoginProcedure>(m_Owner);
        }
    }
}