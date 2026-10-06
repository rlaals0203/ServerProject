using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitUpgradeUI : ContentUI
{
    [SerializeField] private UnitSOList unitSOList;
    [SerializeField] private UnitListUI unitListUI;
    [SerializeField] private UnitSpecialAbilityUI specialAbilityUI;

    [SerializeField] private TMP_Text description;
    [SerializeField] private Button upgradeButton;

    private string _currentUnitId;

    public override void Initialize()
    {
        base.Initialize();

        foreach(var unitData in unitSOList.UnitList)
        {
            if(GameManager.I.User.UserUnitData.OwnUnitList.TryGetValue(unitData.UnitId, out var data))
            {

            }
        }

        unitListUI.InitializeUnitList(unitSOList.CreateUnitDataCache());
        unitListUI.OnElementChanged += HandleElementChanged;
    }

    private void HandleElementChanged(UnitSlotUI ui)
    {
        _currentUnitId = ui.GetUnitId();

        UnitSO unitSO = unitSOList.GetUnitById(_currentUnitId);
        description.text = unitSO.UnitDesc;
        specialAbilityUI.InitSpecialAbilityUI(unitSOList.GetUnitDataByid(_currentUnitId).UnitSpecialAbilityData);
        RefreshButtonState();
    }

    private void RefreshButtonState()
    {
        int upgradePrice = unitSOList.GetUnitById(_currentUnitId).UpgradePrice;
        int currentLevel = new GetUnitLevel(_currentUnitId).Level;
        int finalPrice = new GetUnitUpgradePrice(upgradePrice, currentLevel).Price;

        upgradeButton.interactable = finalPrice <= GameManager.I.User.UserMonetaData.Exp;
    }

    public async void OnTouchUpgradeUnit()
    {
        await NetworkManager.I.SendUnitUpgradeRequest(_currentUnitId, 1, HandleUpgradeUnit);
    }

    private void HandleUpgradeUnit(UnitUpgradePacketResultData data)
    {

    }

    private void OnDestroy()
    {
        unitListUI.OnElementChanged -= HandleElementChanged;
    }
}