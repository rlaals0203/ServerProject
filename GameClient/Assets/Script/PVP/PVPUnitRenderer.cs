using MINLib.Script.Core.Resource;
using System.Collections.Generic;
using UnityEngine;

public class PVPUnitRenderer
{
    private UnitSOList _unitSOList;
    private Dictionary<string, PVPUnit> _units = new();

    public PVPUnitRenderer(UnitSOList unitSOList)
    {
        _unitSOList = unitSOList;
    }

    public void UpdateRenderer(List<RuntimeUnitData> allUnitData)
    {
        if (allUnitData == null)
            return;

        foreach (var unitData in allUnitData)
        {
            if(unitData.IsDead)
            {
                if (_units.TryGetValue(unitData.RuntimeId, out var deadUnit))
                {
                    deadUnit.UpdateState(unitData.State);
                    deadUnit.UpdateUnit(unitData.PositionX, unitData.PositionY);
                    deadUnit.PlayDeath(HandleUnitDead);
                }
                continue;
            }

            if (_units.TryGetValue(unitData.RuntimeId, out var unit))
            {
                unit.UpdateState(unitData.State);
                unit.UpdateAbilityStatus(unitData.AbilityStatus, unitData.FreezeApplyCount);
                unit.UpdateHitback(unitData.HitbackCount);
                unit.UpdateUnit(unitData.PositionX, unitData.PositionY);
                continue;
            }

            UnitSO unitSO = _unitSOList.GetUnitById(unitData.UnitId);
            if (unitSO == null)
                continue;

            unit = ResourceManager.I.SpawnAndInitPool<PVPUnit>("PVPUnit", Vector3.zero, null, spawned =>
            {
                spawned.InitializePVPUnit(unitData, unitSO, GlobalInstance.I.IsPvpOpponent);
            });

            if(unit == null)
                continue;

            _units.Add(unitData.RuntimeId, unit);
        }
    }

    private void HandleUnitDead(PVPUnit unit)
    {
        new SpawnCacheObject<SpiritEffect>("SpiritEffect", unit.transform.position, null, spawned =>
        {
            Vector3 spawnPosition = unit.transform.position + Vector3.up;
            spawned.PlayEffect(spawnPosition, Quaternion.identity, 5f);
        });

        _units.Remove(unit.RuntimeId);
        unit.Release();
    }

    public void ReleaseRenderer()
    {
        foreach (var unit in _units.Values)
        {
            unit.Release();
        }

        _units.Clear();
    }
}