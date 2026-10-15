using MINLib.Script.Core.Resource;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private UnitBattleDeckUI unitBattleDeckUI;

    private Vector3 _unitPosition;
    private Action<int> OnUnitSpawn;
    private List<Unit> _unitList = new();

    public void Initialize(Vector3 unitPosition, Action<int> onUnitSpawn)
    {
        _unitPosition = unitPosition;
        OnUnitSpawn = onUnitSpawn;

        unitBattleDeckUI.InitializeDeckUI();
        unitBattleDeckUI.OnSpawnUnit -= HandleSpawnUnit;
        unitBattleDeckUI.OnSpawnUnit += HandleSpawnUnit;
    }

    private void HandleSpawnUnit(UnitData data)
    {
        var unit = new SpawnCacheObject<Unit>("Unit", _unitPosition).Spawned;
        unit.Initialize(data, ETeamType.Friendly);
        OnUnitSpawn?.Invoke(data.UnitCost);
        _unitList.Add(unit);
    }

    public void ReleaseSpawner()
    {
        foreach(var unit in _unitList)
        {
            if (unit == null || unit.isActiveAndEnabled == false)
                continue;

            ResourceManager.I.DespawnPool(unit.gameObject);
        }

        unitBattleDeckUI.OnSpawnUnit -= HandleSpawnUnit;
    }
}
