
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
public partial class StartProcessTable
{
    public static StartProcessTable Instance { get; private set; }

    private readonly System.Collections.Generic.Dictionary<int, StartProcessConfig> _dataMap;
    private readonly System.Collections.Generic.List<StartProcessConfig> _dataList;
    
    public StartProcessTable(ByteBuf _buf)
    {
        _dataMap = new System.Collections.Generic.Dictionary<int, StartProcessConfig>();
        _dataList = new System.Collections.Generic.List<StartProcessConfig>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            StartProcessConfig _v;
            _v = StartProcessConfig.DeserializeStartProcessConfig(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
    }

    public System.Collections.Generic.Dictionary<int, StartProcessConfig> DataMap => _dataMap;
    public System.Collections.Generic.List<StartProcessConfig> DataList => _dataList;

    public StartProcessConfig GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public StartProcessConfig Get(int key) => _dataMap[key];
    public StartProcessConfig this[int key] => _dataMap[key];

    public void ResolveRef(Tables tables)
    {
        foreach(var _v in _dataList)
        {
            _v.ResolveRef(tables);
        }
    }


    public void SetInstance()
    {
        StartProcessTable.Instance = this;
        OnInit();
    }

    partial void OnInit();
}

}

