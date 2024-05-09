using System.Collections.Generic;


namespace ET.Server
{
	public static partial class RealmGateAddressHelper
	{
		public static cfg.StartSceneConfig GetGate(int zone, string account)
		{
			ulong hash = (ulong)account.GetLongHashCode();
			
			List<cfg.StartSceneConfig> zoneGates = cfg.StartSceneTable.Instance.Gates[zone];
			
			return zoneGates[(int)(hash % (ulong)zoneGates.Count)];
		}
	}
}
