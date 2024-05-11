using System;
using GameFramework;
using GameFramework.GameUpdate;

namespace UnityGameFramework.Runtime
{
    public class GameUpdateComponent: GameFrameworkComponent
    {
        private IGameUpdateManager m_GameUpdateManager;

        protected override void Awake()
        {
            base.Awake();
            m_GameUpdateManager = GameFrameworkEntry.GetModule<IGameUpdateManager>();
        }

        private void Update()
        {
            m_GameUpdateManager.OnUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            m_GameUpdateManager.OnLateUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            m_GameUpdateManager.OnFixedUpdate?.Invoke();
        }
        
        public void SetUpdateAction(Action action)
        {
            m_GameUpdateManager.SetUpdateAction(action);
        }

        public void SetLateUpdateAction(Action action)
        {
            m_GameUpdateManager.SetLateUpdateAction(action);
        }

        public void SetFixedUpdateAction(Action action)
        {
            m_GameUpdateManager.SetFixedUpdateAction(action);
        }
        
        public void SetShutDownAction(Action action)
        {
            m_GameUpdateManager.SetShutDownAction(action);
        }
    }
}
