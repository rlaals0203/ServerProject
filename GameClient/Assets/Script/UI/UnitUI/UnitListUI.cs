using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitListUI : SnapScrollViewBase<UnitSlotUI>
{
    public event Action<string> OnUnitChanged;

    public void InitializeUnitList(Dictionary<string, UnitSO> unitDatas)
    {
        foreach(var unitData in unitDatas)
        {
            var spawned = new SpawnUI<UnitSlotUI>("UnitSlotUI", Vector3.zero, content).spawned;
            spawned.InitializeUI(unitData.Value);
            _elements.Add(spawned);
        }
    }

    protected override void ElementChanged()
    {
        OnUnitChanged?.Invoke(_currentElement.GetUnitId());
    }
}