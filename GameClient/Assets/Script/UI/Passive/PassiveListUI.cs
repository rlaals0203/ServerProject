using System;
using System.Collections.Generic;
using UnityEngine;

public class PassiveListUI : SnapScrollViewBase<PassiveSlotUI>
{
    [SerializeField] private PassiveSOList passiveSOList;
    public event Action<EPassiveType> OnpassiveChanged;

    public void InitializePassiveList()
    {
        foreach (EPassiveType passiveType in Enum.GetValues(typeof(EPassiveType)))
        {
            var spawned = new SpawnUI<PassiveSlotUI>("PassiveSlotUI", Vector3.zero, content).spawned;
            spawned.RefreshUI(passiveType, passiveSOList.GetPassiveByType(passiveType));
            _elements.Add(spawned);
        }
    }

    public void UpdateCurrentSlot(EPassiveType passiveType)
    {
        var passiveSO = passiveSOList.GetPassiveByType(passiveType);
        _currentElement.RefreshUI(passiveType, passiveSO);
    }

    public PassiveSlotUI GetCurrentSlot()
    {
        return _currentElement;
    }

    protected override void ElementChanged()
    {
        OnpassiveChanged?.Invoke(_currentElement.GetPassiveType());
    }
}