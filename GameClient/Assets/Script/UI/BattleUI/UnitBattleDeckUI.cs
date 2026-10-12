using System;
using UnityEngine;

public class UnitBattleDeckUI  : UIBase
{
    [SerializeField] private UnitSOList unitList;
    [SerializeField] private UnitDeckSlotUI[] deckSlotUI;

    public Action<UnitData> OnSpawnUnit;

    protected override void Awake()
    {
        base.Awake();

        foreach(var deckUI in deckSlotUI)
        {
            deckUI.OnSpawnUnit += HandleSpawnUnit;
        }
    }

    public void InitializeDeckUI(UnitData[] unitData)
    {
        for(int i = 0; i < GameDefine.MaxSlotCount; i++)
        {
            if(i >= unitData.Length)
                deckSlotUI[i].InitializeSlot(null, unitList);
            else
                deckSlotUI[i].InitializeSlot(new UpdateUnitData(new MakeUnitStatData(unitList.GetUnitById(unitData[i].UnitStatData.UnitId)).UnitStatData, 1).UnitData, unitList);
        }
    }

    private void HandleSpawnUnit(UnitData data)
    {
        OnSpawnUnit?.Invoke(data);
    }

    private void OnDestroy()
    {
        foreach (var deckUI in deckSlotUI)
        {
            deckUI.OnSpawnUnit -= HandleSpawnUnit;
        }
    }
}