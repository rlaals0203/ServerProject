using System.Collections.Generic;
using UnityEngine;

public class BattleField : MonoBehaviour
{
    private MapData _mapData;
    private List<SpawnSchedule> _spawnShcedules = new();

    public void InitializeBattleField(MapData mapData)
    {
        _mapData = mapData;

        foreach (var spawnData in _mapData.EnemySpawnList)
        {
            var spawnSchdule = new SpawnSchedule(spawnData);
            spawnSchdule.OnSpawnEnemy += HandleSpawnEnemy;
            _spawnShcedules.Add(spawnSchdule);
        }
    }

    private void Update()
    {
        foreach(var schedule in _spawnShcedules)
        {
            schedule.UpdateSchedule(Time.deltaTime);
        }
    }

    private void HandleSpawnEnemy(string enemyId)
    {
        SpawnEnemy(enemyId);
    }

    private void SpawnEnemy(string enemyId)
    {

    }
}
