using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitListUI : SnapScrollViewBase<UnitSlotUI>
{
    public event Action<string> OnUnitChanged;

    public void InitializeUnitList(Dictionary<string, UnitData> unitDatas, Dictionary<string, UnitStatData> unitStatData)
    {
        foreach(var unitData in unitStatData)
        {
            var spawned = new SpawnUI<UnitSlotUI>("UnitSlotUI", Vector3.zero, content).spawned;


            spawned.InitializeUI(unitData.Value);
            _elements.Add(spawned);
        }
    }

    public void UpdateCurrentSlot(UnitData unitData)
    {
        _currentElement.InitializeUI(unitData);
    }

    protected override void ElementChanged()
    {
        OnUnitChanged?.Invoke(_currentElement.GetUnitId());
    }
}