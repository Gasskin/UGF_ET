namespace ET.Server
{
    [EntitySystemOf(typeof(Player))]
    [FriendOf(typeof(Player))]
    public static partial class PlayerSystem
    {
        [EntitySystem]
        private static void Awake(this Player self, string a,long b)
        {
            self.Account = a;
            self.RoleId = b;
        }
    }
}