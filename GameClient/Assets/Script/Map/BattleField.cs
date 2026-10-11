using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleField : MonoBehaviour
{
    [SerializeField] private UnitSOList enemyList;
    [SerializeField] private UnitBattleDeckUI unitBattleDeckUI;
    [SerializeField] private UnitSO friendlyCastleData;
    [SerializeField] private Transform unitCastlePosition;
    [SerializeField] private Transform enemyCastlePosition;
    [SerializeField] private Transform unitSpawnPosition;
    [SerializeField] private Transform enemySpawnPosition;

    private MapDataSO _mapData;
    private List<SpawnSchedule> _spawnShcedules = new();

    private Vector3 _enemyPosition;
    private Vector3 _unitPosition;
    private Vector3 _enemySpawnPosition;
    private Vector3 _unitSpawnPosition;

    private void Start()
    {
        if (GameManager.I.CurrentMap == null)
        {
            Debug.LogError("No map selected for BattleScene");
            return;
        }

        InitializeBattleField(GameManager.I.CurrentMap);
    }

    public void InitializeBattleField(MapDataSO mapData)
    {
        _mapData = mapData;
        _enemyPosition = enemyCastlePosition.position;
        _unitPosition = unitCastlePosition.position;
        _enemySpawnPosition = enemySpawnPosition.position;
        _unitSpawnPosition = unitSpawnPosition.position;
        if (_mapData.CastleDistance > 0f)
        {
            _enemyPosition.x = -_mapData.CastleDistance;
            _unitPosition.x = _mapData.CastleDistance;
            _enemySpawnPosition.x += _enemyPosition.x - enemyCastlePosition.position.x;
            _unitSpawnPosition.x += _unitPosition.x - unitCastlePosition.position.x;
        }

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
    }

    private void SpawnCastle()
    {
        var unitCastle = new SpawnCacheObject<Castle>("Castle", _unitPosition).Spawned;
        unitCastle.Initialize(new UpdateUnitData(new MakeUnitStatData(friendlyCastleData).UnitStatData, 1).UnitData, ETeamType.Friendly);

        var enemyCastleData = _mapData.CastleData != null ? _mapData.CastleData : friendlyCastleData;
        var enemyCastle = new SpawnCacheObject<Castle>("Castle", _enemyPosition).Spawned;
        //enemyCastle.Initialize(_mapData.CastleData, ETeamType.Enemy);
        enemyCastle.Initialize(new UpdateUnitData(new MakeUnitStatData(enemyCastleData).UnitStatData, 1).UnitData, ETeamType.Enemy);
    }

    private void HandleSpawnEnemy(UnitData enemyData)
    {
        var enemy = new SpawnCacheObject<Unit>("Unit", _enemySpawnPosition).Spawned;
        enemy.Initialize(enemyData, ETeamType.Enemy);
    }

    private void HandleSpawnUnit(UnitData unitData)
    {
        var unit = new SpawnCacheObject<Unit>("Unit", _unitSpawnPosition).Spawned;
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
