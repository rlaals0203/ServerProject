using System;
using System.Collections.Generic;
using UnityEngine;

public class PVPBattleField : MonoBehaviour
{
    [SerializeField] private UnitSOList unitSOList;

    private Dictionary<int, PVPUnitRenderer> _units = new();
    private List<int> _removedUnitIds = new();
    private int _snapshotId;
    private readonly object _messageLock = new();
    private UpdateAllUnitDataMessage _pendingData;
    private IDisposable _subscription;

    private void Start()
    {
        _subscription = SignalRClient.I.SubscribeToHub<UpdateAllUnitDataMessage>(UpdateAllUnitDataMessage.ROUTE, HandleUpdateUnits);
    }

    private void HandleUpdateUnits(UpdateAllUnitDataMessage data)
    {
        lock(_messageLock)
        {
            _pendingData = data;
        }
    }

    private void Update()
    {
        UpdateAllUnitDataMessage data;
        lock(_messageLock)
        {
            data = _pendingData;
            _pendingData = null;
        }

        if(data != null)
            RefreshUnits(data);

        float dt = Time.deltaTime;
        foreach(var unit in _units.Values)
        {
            unit.Update(dt);
        }
    }

    private void RefreshUnits(UpdateAllUnitDataMessage data)
    {
        bool isOpponent = GlobalInstance.I.IsPvpOpponent;
        _snapshotId++;
        foreach(var unitData in data.AllUnitDatas)
        {
            if(_units.TryGetValue(unitData.RuntimeId, out var unit))
            {
                unit.UpdateUnit(unitData, isOpponent);
                unit.LastSnapshotId = _snapshotId;
                continue;
            }

            UnitSO unitSO = unitSOList.GetUnitById(unitData.UnitId);
            if(unitSO == null)
                continue;

            unit = new PVPUnitRenderer(unitData, unitSO, transform, isOpponent);
            unit.LastSnapshotId = _snapshotId;
            _units.Add(unitData.RuntimeId, unit);
        }

        _removedUnitIds.Clear();
        foreach(var unit in _units)
        {
            if(unit.Value.LastSnapshotId != _snapshotId)
                _removedUnitIds.Add(unit.Key);
        }

        foreach(int unitId in _removedUnitIds)
        {
            _units[unitId].Release();
            _units.Remove(unitId);
        }
    }

    private void OnDestroy()
    {
        if(_subscription != null)
            _subscription.Dispose();

        foreach(var unit in _units.Values)
        {
            unit.Release();
        }
    }
}