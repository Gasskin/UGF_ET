namespace ET.Server;

public enum AccountType
{
    General = 0,
    BlackList = 1,
}

[ChildOf(typeof(Session))]
public class Account: Entity,IAwake
{
    public string AccountName;
    public string Password;
    public long CreateTime;
    public int AccountType;
}

