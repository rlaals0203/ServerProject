using System;
using UnityEngine;

public class UnitBattleDeckUI  : UIBase
{
    [SerializeField] private UnitSOList unitList;
    [SerializeField] private UnitDeckSlotUI[] deckSlotUI;
    [SerializeField] private BattleField battleField;

    public Action<UnitData> OnSpawnUnit;

    protected override void Awake()
    {
        base.Awake();

        foreach(var deckUI in deckSlotUI)
        {
            deckUI.OnSpawnUnit += HandleSpawnUnit;
        }
    }

    public void InitializeDeckUI()
    {
        var deployUnits = GlobalInstance.I.MainUser.UserUnitData.DeployUnitList;

        for(int i = 0; i < deckSlotUI.Length; i++)
        {
            if(deployUnits == null || deployUnits.TryGetValue(i.ToString(), out var unitData) == false)
                deckSlotUI[i].InitializeSlot(null, unitList, battleField);
            else
                deckSlotUI[i].InitializeSlot(unitData, unitList, battleField);
        }
    }

    private void HandleSpawnUnit(UnitData data)
    {
        if(battleField.GetMoney() < data.UnitCost)
        {
            return;
        }

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
