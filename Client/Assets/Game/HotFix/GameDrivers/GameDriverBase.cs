using Cysharp.Threading.Tasks;

namespace Game.HotFix.GameDrivers
{
    public abstract class GameDriverBase
    {
        public abstract int Priority { get; }

        public abstract UniTask InitAsync();

        public abstract void Update();

        public abstract void LateUpdate();

        public abstract void FixedUpdate();

        public abstract void ShutDown();
    }
}
