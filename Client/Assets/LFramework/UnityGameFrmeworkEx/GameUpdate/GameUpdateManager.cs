using System;

namespace GameFramework.GameUpdate
{
    internal sealed class GameUpdateManager : GameFrameworkModule, IGameUpdateManager
    {
        public Action OnUpdate { get; set; }

        public Action OnLateUpdate { get; set; }

        public Action OnFixedUpdate { get; set; }
        
        public Action OnShutDown { get; set; }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        internal override void Shutdown()
        {
            OnShutDown?.Invoke();
            OnUpdate = null;
            OnLateUpdate = null;
            OnFixedUpdate = null;
            OnShutDown = null;
        }

        public void SetUpdateAction(Action action)
        {
            OnUpdate = action;
        }

        public void SetLateUpdateAction(Action action)
        {
            OnLateUpdate = action;
        }

        public void SetFixedUpdateAction(Action action)
        {
            OnFixedUpdate = action;
        }
        
        public void SetShutDownAction(Action action)
        {
            OnShutDown = action;
        }
    }
}