namespace ET.Server;

public enum RoleInfoState
{
    Normal = 0,
    Freeze = 1, // 冻结
}

[ChildOf]
public class RoleInfo:Entity,IAwake,IUnitCache
{
    public string Name;
    public int State;
    public string Account;
    public long LastLoginTime;
    public int ServerId;
    public long CreateTime;
}