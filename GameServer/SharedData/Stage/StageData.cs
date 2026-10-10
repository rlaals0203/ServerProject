public class StageData
{
    public string StageId { get; set; }
    public float EnemySpawnPositionX { get; set; }
    public List<EnemySpawnData> EnemySpawnList { get; set; } = new();
}

public class EnemySpawnData
{
    public string EnemyUnitId { get; set; }
    public float FirstSpawnDelay { get; set; }
    public float RepeatSpawnDelay { get; set; }
    public int MaxSpawnCount { get; set; }
    public EStageSpawnTrigger SpawnTrigger { get; set; }
}
