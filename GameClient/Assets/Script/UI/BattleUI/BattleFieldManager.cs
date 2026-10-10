using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleFieldManager : MonoBehaviour
{
    [SerializeField] private UnitSO[] units;
    [SerializeField] private UnitSO testEnemy;
    [SerializeField] private UnitBattleDeckUI unitBattleDeckUI;

    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform enemyPosition;


    [SerializeField] private UnitSO castleData;
    [SerializeField] private Transform unitCastlePosition;
    [SerializeField] private Transform enemyCastlePosition;

    private void Awake()
    {
        List<UnitData> unitList = new();

        foreach(var unit in units)
        {
            unitList.Add(new UpdateUnitData(new MakeUnitStatData(unit).UnitStatData, 1).UnitData);
        }

        unitBattleDeckUI.InitializeDeckUI(unitList.ToArray());
        unitBattleDeckUI.OnSpawnUnit += HandleSpawnUnit;
    }

    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            var enemy = new SpawnCacheObject<Unit>("Unit", enemyPosition.position).Spawned;
            enemy.Initialize(new UpdateUnitData(new MakeUnitStatData(testEnemy).UnitStatData, 1).UnitData, ETeamType.Enemy);
        }

        if(Keyboard.current.tKey.wasPressedThisFrame) 
        {
            var castle = new SpawnCacheObject<Castle>("Castle", unitCastlePosition.position).Spawned;
            castle.Initialize(new UpdateUnitData(new MakeUnitStatData(castleData).UnitStatData, 1).UnitData, ETeamType.Friendly);

            var enemyCastle = new SpawnCacheObject<Castle>("Castle", enemyCastlePosition.position).Spawned;
            enemyCastle.Initialize(new UpdateUnitData(new MakeUnitStatData(castleData).UnitStatData, 1).UnitData, ETeamType.Enemy);
        }
    }

    private void HandleSpawnUnit(UnitData data)
    {
        var spawned = new SpawnCacheObject<Unit>("Unit", spawnPosition.position).Spawned;
        spawned.Initialize(data, ETeamType.Friendly);
    }

    private void OnDestroy()
    {
        unitBattleDeckUI.OnSpawnUnit -= HandleSpawnUnit;
    }
}