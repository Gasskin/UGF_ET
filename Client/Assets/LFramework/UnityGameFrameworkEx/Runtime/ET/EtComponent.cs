using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ET;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    public class EtComponent : GameFrameworkComponent
    {
        private void Update()
        {
            TimeInfo.Instance.Update();
            FiberManager.Instance.Update();
        }

        private void LateUpdate()
        {
            FiberManager.Instance.LateUpdate();
        }

        private void OnApplicationQuit()
        {
            World.Instance.Dispose();
        }
    }
}