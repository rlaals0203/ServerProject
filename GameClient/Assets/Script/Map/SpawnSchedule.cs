using System;

public class SpawnSchedule : IDisposable
{
    private EnemySpawnData _spawnData;
    private int _spawnCount;
    public float _remainTime;

    public bool IsFinished { get; private set; }

    public event Action<UnitData> OnSpawnEnemy;
    private UnitData _enemyData;

    public SpawnSchedule(EnemySpawnData spawnData, UnitSO enemyData)
    {
        _spawnData = spawnData;
        _remainTime = _spawnData.FirstSpawnDelay;
        _enemyData = new MakeEnemyData(new MakeUnitStatData(enemyData).UnitStatData, 1).EnemyData;
    }

    public void UpdateSchedule(float dt)
    {
        if (IsFinished)
            return;

        _remainTime -= dt;

        if (_remainTime <= 0f)
        {
            OnSpawnEnemy?.Invoke(_enemyData);
            _remainTime = _spawnData.RepeatSpawnDelay;
            _spawnCount++;

            if (_spawnCount >= _spawnData.MaxSpawnCount)
            {
                IsFinished = true;
            }
        }
    }

    public void Dispose()
    {
        OnSpawnEnemy = null;
    }
}