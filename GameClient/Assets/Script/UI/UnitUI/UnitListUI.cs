using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitListUI : SnapScrollViewBase<UnitSlotUI>
{
    protected override void Awake()
    {
        var upgradeSlots = GetComponentsInChildren<UnitSlotUI>();
        foreach(var slot in upgradeSlots)
        {
            _elements.Add(slot);
        }
    }

    protected override void ElementChanged()
    {

    }
}