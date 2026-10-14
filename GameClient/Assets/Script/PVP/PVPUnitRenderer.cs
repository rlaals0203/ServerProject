using MINLib.Script.Core.Resource;
using System.Collections.Generic;
using UnityEngine;

public class PVPUnitRenderer
{
    private UnitSOList _unitSOList;
    private Dictionary<string, PVPUnit> _units = new();
    private Queue<string> _removeQueue = new();

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
                _removeQueue.Enqueue(unitData.RuntimeId);
                continue;
            }

            if (_units.TryGetValue(unitData.RuntimeId, out var unit))
            {
                unit.UpdateState(unitData.State);
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

        while(_removeQueue.Count > 0)
        {
            string removeId = _removeQueue.Dequeue();

            if (_units.TryGetValue(removeId, out var pvpUnit) == false)
                continue;

            new SpawnCacheObject<SpiritEffect>("SpiritEffect", pvpUnit.transform.position, null, spawned =>
            {
                Vector3 spawnPosition = pvpUnit.transform.position + Vector3.up;
                spawned.PlayEffect(spawnPosition, Quaternion.identity, 5f);
            });

            pvpUnit.Release();
            _units.Remove(removeId);
        }
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