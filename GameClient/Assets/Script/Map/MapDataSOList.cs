using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "MapDataSOList", menuName = "SO/MapDataList")]
public class MapDataSOList : ScriptableObject
{
    public List<MapDataSO> MapDataList = new();

    [ContextMenu("ExportStageDataToJson")]
    public void ExportStageDataToJson()
    {
        Dictionary<string, MapData> stageData = new();

        foreach (var mapData in MapDataList)
        {
            if (mapData == null || string.IsNullOrEmpty(mapData.MapId))
            {
                continue;
            }

            if (stageData.ContainsKey(mapData.MapId))
            {
                Debug.LogWarning($"duplicate stage id: {mapData.MapId}");
                continue;
            }

            stageData.Add(mapData.MapId, new MakeMapData(mapData).MapData);
        }

        string path = Path.Combine(Application.dataPath, "..", "..", "GameServer", "StageData.json");
        File.WriteAllText(path, JsonConvert.SerializeObject(stageData));
        Debug.Log($"stage data saved: {path}");
    }

    public MapDataSO GetMapDataById(string mapId)
    {
        if(string.IsNullOrEmpty(mapId))
        {
            return null;
        }

        return MapDataList.Where(map => map.MapId == mapId).FirstOrDefault();
    }
}
