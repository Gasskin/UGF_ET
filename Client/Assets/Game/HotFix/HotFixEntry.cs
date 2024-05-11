using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.HotFix.GameDrivers;
using Game.HotFix.Procedure;
using GameFramework;
using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game.HotFix
{
    public class HotFixEntry
    {
        public static HotFixEntry s_Instance;

        private readonly List<GameDriverBase> m_GameDrivers = new();
        private readonly Dictionary<Type, GameDriverBase> m_GameDriverDic = new();

        public static void Start()
        {
            if (s_Instance != null)
            {
                Log.Error("重复初始化HotFixEntry？？？");
                return;
            }
            s_Instance = new HotFixEntry();

            var updateComponent = GameEntry.GetComponent<GameUpdateComponent>();
            updateComponent.SetUpdateAction(s_Instance.Update);
            updateComponent.SetLateUpdateAction(s_Instance.LateUpdate);
            updateComponent.SetFixedUpdateAction(s_Instance.FixedUpdate);
            updateComponent.SetShutDownAction(s_Instance.ShutDown);
                
            var fsmManager = GameFrameworkEntry.GetModule<IFsmManager>();
            fsmManager.DestroyFsm<IProcedureManager>();
            var procedureManager = GameFrameworkEntry.GetModule<IProcedureManager>();
            procedureManager.Initialize(fsmManager,
                new LoginProcedure(),
                new InitFrameworkProcedure());

            procedureManager.StartProcedure<InitFrameworkProcedure>();
        }

        public T GetDriver<T>() where T : GameDriverBase
        {
            var type = typeof(T);
            if (s_Instance.m_GameDriverDic.TryGetValue(type, out var driver))
            {
                return driver as T;
            }
            return null;
        }

        public async UniTask AddDriver<T>() where T : GameDriverBase, new()
        {
            var driver = new T();
            s_Instance.m_GameDrivers.Add(driver);
            s_Instance.m_GameDriverDic.Add(typeof(T), driver);
            await driver.InitAsync();
        }
        
    #region Update
        private void Update()
        {
            foreach (var gameDriver in m_GameDrivers)
            {
                gameDriver.Update();
            }
        }

        private void LateUpdate()
        {
            foreach (var gameDriver in m_GameDrivers)
            {
                gameDriver.LateUpdate();
            }
        }

        private void FixedUpdate()
        {
            foreach (var gameDriver in m_GameDrivers)
            {
                gameDriver.FixedUpdate();
            }
        }

        private void ShutDown()
        {
            foreach (var gameDriver in m_GameDrivers)
            {
                gameDriver.ShutDown();
            }
            s_Instance = null;
        }
    #endregion
    }
}
