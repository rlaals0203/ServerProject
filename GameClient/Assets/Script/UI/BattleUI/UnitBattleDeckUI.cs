using System;
using UnityEngine;

public class UnitBattleDeckUI  : UIBase
{
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
        for(int i = 0; i < unitData.Length; i++)
        {
            deckSlotUI[i].InitializeSlot(unitData[i]);
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