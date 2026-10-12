using DG.Tweening;
using MINLib.Script.Core.Resource;
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
    [SerializeField] private UIBlinkEffect blinkEffect;

    private string _currentUnitId;

    public override void Initialize()
    {
        base.Initialize();

        unitListUI.InitializeUnitList(GlobalInstance.I.MainUser.UserUnitData.OwnUnitList);
        unitListUI.OnElementChanged -= RefreshUI;
        unitListUI.OnElementChanged += RefreshUI;
    }

    private void RefreshUI(UnitSlotUI ui)
    {
        _currentUnitId = ui.GetUnitId();

        UnitSO unitSO = unitSOList.GetUnitById(_currentUnitId);
        description.text = unitSO.UnitDesc;
        specialAbilityUI.InitSpecialAbilityUI(unitSO.UnitSpecialAbilityData);
        RefreshButtonState();
    }

    private void RefreshButtonState()
    {
        int currentLevel = new GetUnitLevel(_currentUnitId).Level;
        int upgradePrice = unitSOList.GetUnitById(_currentUnitId).UpgradePrice;
        int finalPrice = new GetUnitUpgradePrice(upgradePrice, currentLevel).Price;
        bool purchasable = finalPrice <= GlobalInstance.I.MainUser.UserMonetaData[EMonetaType.Exp.ToString()] && currentLevel < GameDefine.UnitMaxLevel;
        upgradeButton.interactable = purchasable;

        if (purchasable)
            blinkEffect.PlayBlinkEffect();
        else
            blinkEffect.StopBlinkEffect();
    }

    public async void OnTouchUpgradeUnit()
    {
        if(GlobalInstance.I.MainUser.UserUnitData.OwnUnitList.ContainsKey(_currentUnitId))
        {
            await NetworkManager.I.SendUnitUpgradeRequest(_currentUnitId, 1, HandleUpgradeUnit);
        }
        else
        {
            await NetworkManager.I.SendUnitUnlockRequest(_currentUnitId, HandleUnlockUnit);
        }
    }

    private void HandleUnlockUnit(UnitUnlockPacketResultData data)
    {
        if(data.Result != EPacketResult.Ok)
        {
            return;
        }
    }

    private void HandleUpgradeUnit(UnitUpgradePacketResultData data)
    {
        if (data.Result != EPacketResult.Ok)
        {
            return;
        }

        var unitData = GlobalInstance.I.MainUser.UserUnitData;

        if (unitData.OwnUnitList.TryGetValue(data.UnitId, out var unit) == false)
            return;

        unit = new UpdateUnitData(unit.UnitStatData, data.UnitLevel).UnitData;
        unitData.OwnUnitList[data.UnitId] = unit;

        for (int i = 0; i < GameDefine.MaxSlotCount; i++)
        {
            string slotKey = i.ToString();
            if (unitData.DeployUnitList.TryGetValue(slotKey, out var deployUnit)
                && deployUnit.UnitStatData.UnitId == data.UnitId)
            {
                unitData.DeployUnitList[slotKey] = unit;
            }
        }

        GlobalInstance.I.MainUser.UserMonetaData[EMonetaType.Exp.ToString()] -= data.UpgradePrice;
        unitListUI.GetCurrentSlot()?.PlayPurchseEffect();
        unitListUI.UpdateCurrentSlot(unit);
        RefreshButtonState();
    }

    private void OnDestroy()
    {
        unitListUI.OnElementChanged -= RefreshUI;
    }
}
