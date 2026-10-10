using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
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
                Debug.LogWarning("StageSOList has an empty stage or StageId.");
                continue;
            }

            if (stageData.ContainsKey(mapData.MapId))
            {
                Debug.LogWarning($"Duplicate StageId: {mapData.MapId}");
                continue;
            }

            stageData.Add(mapData.MapId, new MakeMapData(mapData).MapData);
        }

        string path = Path.Combine(Application.dataPath, "..", "..", "GameServer", "StageData.json");
        File.WriteAllText(path, JsonConvert.SerializeObject(stageData));
        Debug.Log($"Stage data saved: {path}");
    }
}
