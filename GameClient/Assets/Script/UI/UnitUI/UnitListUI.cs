using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitListUI : SnapScrollViewBase<UpgradeSlotUI>
{
    [SerializeField] private PassiveSOList passiveSOList;
    [SerializeField] private UnitSOList unitSOList;

    public event Action<string> OnUnitChanged;
    public event Action<EPassiveType> OnpassiveChanged;

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

    public void InitializePassiveList()
    {
        foreach (EPassiveType passiveType in Enum.GetValues(typeof(EPassiveType)))
        {
            var spawned = new SpawnUI<PassiveSlotUI>("PassiveSlotUI", Vector3.zero, content).spawned;
            spawned.RefreshUI(passiveType, passiveSOList.GetPassiveByType(passiveType));
            _elements.Add(spawned);
        }
    }

    public void UpdateCurrentSlot(UnitData unitData)
    {
        if (_currentElement is not UnitSlotUI unitSlot)
            return;

        UnitSO unitSO = unitSOList.GetUnitById(unitData.UnitStatData.UnitId);
        unitSlot.RefreshUI(unitData, unitSO);
    }

    public void UpdateCurrentSlot(EPassiveType passiveType)
    {
        if (_currentElement is not PassiveSlotUI passiveSlot)
            return;

        var passiveSO = passiveSOList.GetPassiveByType(passiveType);
        passiveSlot.RefreshUI(passiveType, passiveSO);
    }

    public UpgradeSlotUI GetCurrentSlot()
    {
        return _currentElement;
    }

    protected override void ElementChanged()
    {
        if (_currentElement is UnitSlotUI unitSlot)
        {
            OnUnitChanged?.Invoke(unitSlot.GetUnitId());
        }
        else if(_currentElement is PassiveSlotUI passiveSlot)
        {
            OnpassiveChanged?.Invoke(passiveSlot.GetPassiveType());
        }
    }
}