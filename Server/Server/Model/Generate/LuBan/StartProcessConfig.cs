
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;


namespace cfg
{
public sealed partial class StartProcessConfig : Luban.BeanBase
{
    public StartProcessConfig(ByteBuf _buf) 
    {
        Id = _buf.ReadInt();
        MachineId = _buf.ReadInt();
        Port = _buf.ReadInt();
    }

    public static StartProcessConfig DeserializeStartProcessConfig(ByteBuf _buf)
    {
        var buf = new StartProcessConfig(_buf);
        buf.OnInit();
        return buf;
    }

    /// <summary>
    /// Id
    /// </summary>
    public readonly int Id;
    /// <summary>
    /// 所属机器
    /// </summary>
    public readonly int MachineId;
    /// <summary>
    /// 内网端口
    /// </summary>
    public readonly int Port;
   
    public const int __ID__ = 2140444015;
    public override int GetTypeId() => __ID__;

    public  void ResolveRef(Tables tables)
    {
        
        
        
    }

    partial void OnInit();

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "MachineId:" + MachineId + ","
        + "Port:" + Port + ","
        + "}";
    }
}

}
