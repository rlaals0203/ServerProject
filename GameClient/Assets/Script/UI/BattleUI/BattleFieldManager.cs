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

    private void Awake()
    {
        List<UnitStatData> unitList = new();

        foreach(var unit in units)
        {
            unitList.Add(new MakeUnitStatData(unit).UnitStatData);
        }

        //unitBattleDeckUI.InitializeDeckUI(unitList.ToArray());
        unitBattleDeckUI.OnSpawnUnit += HandleSpawnUnit;
    }

    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            var enemy = new SpawnCacheObject<Unit>("Unit", enemyPosition.position).Spawned;
            //enemy.Initialize(new MakeUnitStatData(testEnemy).UnitStatData, ETeamType.Enemy);
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