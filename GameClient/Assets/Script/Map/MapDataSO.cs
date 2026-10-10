using System;
using System.Collections.Generic;
using UnityEngine;

public enum EStageSpawnTrigger
{
    None,
    CastleHit,
}

[CreateAssetMenu(fileName = "MapDataSO", menuName = "SO/MapData")]
public class MapDataSO : ScriptableObject
{
    public string MapId;
    public float CastleDistance;
    public List<EnemySpawnData> EnemySpawnList = new();
}


[Serializable]
public class EnemySpawnData
{
    public string EnemyId;
    public float FirstSpawnDelay;
    public float RepeatSpawnDelay;
    public int MaxSpawnCount = 1;
    public EStageSpawnTrigger SpawnTrigger;
}

[Serializable]
public class MapData
{
    public string StageId { get; set; }
    public float EnemySpawnPositionX { get; set; }
    public List<EnemySpawnData> EnemySpawnList { get; set; } = new();
}

public class MakeMapData
{
    public MapData MapData;

    public MakeMapData(MapDataSO mapDataSO)
    {
        MapData = new MapData
        {
            StageId = mapDataSO.MapId,
            EnemySpawnPositionX = mapDataSO.CastleDistance,
        };

        foreach (var spawnSOData in mapDataSO.EnemySpawnList)
        {
            if (spawnSOData.EnemyId == null)
            {
                continue;
            }

            MapData.EnemySpawnList.Add(new EnemySpawnData
            {
                EnemyId = spawnSOData.EnemyId,
                FirstSpawnDelay = spawnSOData.FirstSpawnDelay,
                RepeatSpawnDelay = spawnSOData.RepeatSpawnDelay,
                MaxSpawnCount = spawnSOData.MaxSpawnCount,
                SpawnTrigger = spawnSOData.SpawnTrigger
            });
        }
    }
}
