using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private List<SpawnSchedule> _spawnShcedules = new();

    private Vector3 _spawnPosition;
    private List<Unit> _enemyList = new();
    private Action<int> _onEnemyDead;

    public void Initialize(MapDataSO mapData, UnitSOList unitList, Vector3 enemyPosition, Action<int> onEnemyDead)
    {
        _spawnShcedules.Clear();
        _onEnemyDead = onEnemyDead;
        _spawnPosition = enemyPosition;

        foreach (var spawnData in mapData.EnemySpawnList)
        {
            var spawnSchdule = new SpawnSchedule(spawnData, unitList.GetUnitById(spawnData.EnemyId));
            spawnSchdule.OnSpawnEnemy += HandleSpawnEnemy;
            _spawnShcedules.Add(spawnSchdule);
        }
    }

    public void UpdateSpawner()
    {
        foreach (var schedule in _spawnShcedules)
        {
            schedule.UpdateSchedule(Time.deltaTime);
        }
    }

    private void HandleSpawnEnemy(UnitData enemyData)
    {
        var enemy = new SpawnCacheObject<Unit>("Unit", _spawnPosition).Spawned;
        enemy.Initialize(enemyData, ETeamType.Enemy);
        enemy.OnDead += HandleEnemyDead;
        _enemyList.Add(enemy);
    }

    private void HandleEnemyDead(Unit unit)
    {
        if (unit.IsEnemyUnit == false)
            return;

        _onEnemyDead?.Invoke(unit.UnitData.UnitStatData.UnitDropMoneyAmount);
    }

    public void ReleaseSpawner()
    {
        foreach (var schedule in _spawnShcedules)
        {
            schedule.OnSpawnEnemy -= HandleSpawnEnemy;
        }

        foreach (var enemy in _enemyList)
        {
            enemy.OnDead -= HandleEnemyDead;
        }
    }
}