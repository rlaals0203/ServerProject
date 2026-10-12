using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitListUI : SnapScrollViewBase<UnitSlotUI>
{
    [SerializeField] private UnitSOList unitSOList;
    public event Action<string> OnUnitChanged;

    public void InitializeUnitList(Dictionary<string, UnitData> unitDatas)
    {
        foreach(var unitData in unitDatas)
        {
            var spawned = new SpawnUI<UnitSlotUI>("UnitSlotUI", Vector3.zero, content).spawned;
            UnitSO unitSO = unitSOList.GetUnitById(unitData.Value.UnitStatData.UnitId);
            spawned.RefreshUI(unitData.Value, unitSO);
            _elements.Add(spawned);
        }
    }

    public void UpdateCurrentSlot(UnitData unitData)
    {
        UnitSO unitSO = unitSOList.GetUnitById(unitData.UnitStatData.UnitId);
        _currentElement.RefreshUI(unitData, unitSO);
    }

    public UnitSlotUI GetCurrentSlot()
    {
        return _currentElement;
    }

    protected override void ElementChanged()
    {
        OnUnitChanged?.Invoke(_currentElement.GetUnitId());
    }
}