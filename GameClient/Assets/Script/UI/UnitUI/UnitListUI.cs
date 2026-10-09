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
            spawned.InitializeUI(unitData.Value, unitSO);
            _elements.Add(spawned);
        }
    }

    public void UpdateCurrentSlot(UnitData unitData)
    {
        UnitSO unitSO = unitSOList.GetUnitById(unitData.UnitStatData.UnitId);
        _currentElement.InitializeUI(unitData, unitSO);
    }

    protected override void ElementChanged()
    {
        OnUnitChanged?.Invoke(_currentElement.GetUnitId());
    }
}