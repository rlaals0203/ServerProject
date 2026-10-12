using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleField : MonoBehaviour
{
    [SerializeField] private UnitSOList enemyList;
    [SerializeField] private UnitBattleDeckUI unitBattleDeckUI;
    [SerializeField] private UnitSO friendlyCastleData;

    private MapDataSO _mapData;
    private List<SpawnSchedule> _spawnShcedules = new();

    private Vector3 _enemyPosition;
    private Vector3 _unitPosition;
    private float _money;
    private float _moneyMultiplier = 20f;

    private void Start()
    {
        InitializeBattleField(GameManager.I.CurrentMap);
    }

    public void InitializeBattleField(MapDataSO mapData)
    {
        _mapData = mapData;
        _enemyPosition = new Vector3(-_mapData.CastleDistance, -1.8f, 0);
        _unitPosition = new Vector3(_mapData.CastleDistance, -1.8f, 0);

        _spawnShcedules.Clear();

        foreach (var spawnData in _mapData.EnemySpawnList)
        {
            var spawnSchdule = new SpawnSchedule(spawnData, enemyList.GetUnitById(spawnData.EnemyId));
            spawnSchdule.OnSpawnEnemy += HandleSpawnEnemy;
            _spawnShcedules.Add(spawnSchdule);
        }

        unitBattleDeckUI.InitializeDeckUI(GameManager.I.MainUser.UserUnitData.DeployUnitList.Values.ToArray());
        unitBattleDeckUI.OnSpawnUnit -= HandleSpawnUnit;
        unitBattleDeckUI.OnSpawnUnit += HandleSpawnUnit;
        SpawnCastle();
    }

    private void Update()
    {
        foreach(var schedule in _spawnShcedules)
        {
            schedule.UpdateSchedule(Time.deltaTime);
        }

        _money += Time.deltaTime * _moneyMultiplier;
    }

    private void SpawnCastle()
    {
        var unitCastle = new SpawnCacheObject<Castle>("Castle", _unitPosition).Spawned;
        unitCastle.Initialize(new UpdateUnitData(new MakeUnitStatData(friendlyCastleData).UnitStatData, 1).UnitData, ETeamType.Friendly);

        var enemyCastle = new SpawnCacheObject<Castle>("Castle", _enemyPosition).Spawned;
        enemyCastle.Initialize(new UpdateUnitData(new MakeUnitStatData(_mapData.CastleData).UnitStatData, 1).UnitData, ETeamType.Enemy);
    }

    private void HandleSpawnEnemy(UnitData enemyData)
    {
        var enemy = new SpawnCacheObject<Unit>("Unit", _enemyPosition).Spawned;
        enemy.Initialize(enemyData, ETeamType.Enemy);
    }

    private void HandleSpawnUnit(UnitData unitData)
    {
        var unit = new SpawnCacheObject<Unit>("Unit", _unitPosition).Spawned;
        unit.Initialize(unitData, ETeamType.Friendly);
    }

    public void CompleteBattle()
    {
        unitBattleDeckUI.OnSpawnUnit -= HandleSpawnUnit;
    }

    private void OnDestroy()
    {
        CompleteBattle();
    }
}
