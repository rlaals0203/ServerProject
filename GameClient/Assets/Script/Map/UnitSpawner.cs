using System;
using System.Linq;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private UnitBattleDeckUI unitBattleDeckUI;

    private Vector3 _unitPosition;
    private Action<int> OnUnitSpawn;

    public void Initialize(Vector3 unitPosition, Action<int> onUnitSpawn)
    {
        _unitPosition = unitPosition;
        OnUnitSpawn = onUnitSpawn;

        unitBattleDeckUI.InitializeDeckUI(GameManager.I.MainUser.UserUnitData.DeployUnitList.Values.ToArray());
        unitBattleDeckUI.OnSpawnUnit -= HandleSpawnUnit;
        unitBattleDeckUI.OnSpawnUnit += HandleSpawnUnit;
    }

    private void HandleSpawnUnit(UnitData data)
    {
        var unit = new SpawnCacheObject<Unit>("Unit", _unitPosition).Spawned;
        unit.Initialize(data, ETeamType.Friendly);
        OnUnitSpawn?.Invoke(data.UnitCost);
    }

    public void ReleaseSpaner()
    {
        unitBattleDeckUI.OnSpawnUnit -= HandleSpawnUnit;
    }
}