using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "MapDataSOList", menuName = "SO/MapDataList")]
public class MapDataSOList : ScriptableObject
{
    public List<MapDataSO> MapDataList = new();

    public MapDataSO GetMapDataById(string mapId)
    {
        if(string.IsNullOrEmpty(mapId))
        {
            return null;
        }

        return MapDataList.Where(map => map.MapId == mapId).FirstOrDefault();
    }
}
