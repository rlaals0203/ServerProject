using System;
using System.Collections.Generic;
using UnityEngine;

public enum EStageSpawnTrigger
{
    None,
    CastleHit,
}

[Serializable]
public class EnemySpawnData
{
    public string EnemyId;
    public float Level;
    public float FirstSpawnDelay;
    public float RepeatSpawnDelay;
    public int MaxSpawnCount = 1;
    public EStageSpawnTrigger SpawnTrigger;
}

[CreateAssetMenu(fileName = "MapDataSO", menuName = "SO/MapData")]
public class MapDataSO : ScriptableObject
{
    public string MapId;
    public string MapName;
    public float CastleDistance;
    public UnitSO CastleData;
    public List<EnemySpawnData> EnemySpawnList = new();
}