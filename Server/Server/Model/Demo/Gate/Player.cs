namespace ET.Server
{
    public enum PlayerState
    {
        Disconnect,
        Gate,
        Game
    }
    
    [ChildOf(typeof(PlayerComponent))]
    public sealed class Player : Entity, IAwake<string,long>
    {
        public string Account;

        public PlayerState PlayerState;

        public long RoleId;
    }
}