namespace ET.Server;

[ComponentOf(typeof(Session))]
public class AccountCheckOutTimeComponent: Entity, IAwake<string>,IDestroy
{
    public long Timer = 0;
    public string Account;
}