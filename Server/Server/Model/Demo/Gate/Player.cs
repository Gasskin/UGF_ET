namespace ET.Server
{
    [ChildOf(typeof(PlayerComponent))]
    public sealed class Player : Entity, IAwake<string,long>
    {
        public string Account { get; set; }

        public long RoleId { get; set; }
    }
}