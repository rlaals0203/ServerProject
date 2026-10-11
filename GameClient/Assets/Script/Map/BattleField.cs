using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleField : MonoBehaviour
{
    [SerializeField] private UnitBattleDeckUI unitBattleDeckUI;

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

        unitBattleDeckUI.InitializeDeckUI(GameManager.I.MainUser.UserUnitData.DeployUnitList.Values.ToArray());
        unitBattleDeckUI.OnSpawnUnit += HandleSpawnUnit;
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

    }

    private void HandleSpawnUnit(UnitData data)
    {

    }
}
