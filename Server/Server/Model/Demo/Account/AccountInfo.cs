namespace ET.Server;

[ChildOf(typeof(AccountInfosComponent))]
public class AccountInfo: Entity,IAwake
{
    public string Account;
    public string Password;
}

