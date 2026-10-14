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

        bool isOpponent = GlobalInstance.I.IsPvpOpponent;

        foreach (var unitData in allUnitData)
        {
            if(unitData.IsDead)
            {
                _removeQueue.Enqueue(unitData.RuntimeId);
            }

            if (_units.TryGetValue(unitData.RuntimeId, out var unit))
            {
                if (unit == null)
                    continue;

                unit.UpdateState(unitData.State);
                unit.UpdateUnit(Time.deltaTime, unitData.PositionX, unitData.PositionY);
                continue;
            }

            UnitSO unitSO = _unitSOList.GetUnitById(unitData.UnitId);
            if (unitSO == null)
                continue;

            unit = ResourceManager.I.SpawnAndInitPool<PVPUnit>("PVPUnit", Vector3.zero, null, spawned =>
            {
                spawned.InitializePVPUnit(unitData, unitSO, null, GlobalInstance.I.IsPvpOpponent);
            });

            _units.Add(unitData.RuntimeId, unit);
        }

        foreach (var removeUnit in _removeQueue)
        {
            if (_units.TryGetValue(removeUnit, out var unit) == false)
                continue;

            _units.Remove(removeUnit);
            ResourceManager.I.DespawnPool(unit.gameObject);
        }

        _removeQueue.Clear();
    }

    public void ReleaseRenderer()
    {
        foreach (var unit in _units.Values)
        {
            unit.Release();
        }
    }
}