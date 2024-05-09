
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;
using System.Text.Json;

namespace cfg
{
public partial class Tables
{
    public StartMachineTable StartMachineTable {get; }
    public StartProcessTable StartProcessTable {get; }
    public StartSceneTable StartSceneTable {get; }
    public StartZoneTable StartZoneTable {get; }

    public Tables(System.Func<string, JsonElement> loader)
    {
        StartMachineTable = new StartMachineTable(loader("startmachinetable"));
        StartProcessTable = new StartProcessTable(loader("startprocesstable"));
        StartSceneTable = new StartSceneTable(loader("startscenetable"));
        StartZoneTable = new StartZoneTable(loader("startzonetable"));
        ResolveRef();
    }
    
    private void ResolveRef()
    {
        StartMachineTable.ResolveRef(this);
        StartProcessTable.ResolveRef(this);
        StartSceneTable.ResolveRef(this);
        StartZoneTable.ResolveRef(this);
    }
}

}
