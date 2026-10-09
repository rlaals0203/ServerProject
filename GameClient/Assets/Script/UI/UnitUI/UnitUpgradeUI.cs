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

        unitListUI.InitializeUnitList(GameManager.I.MainUser.UserUnitData.OwnUnitList);
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
        upgradeButton.interactable = GetUnitPrice(_currentUnitId) <= GameManager.I.MainUser.UserMonetaData.Exp;
    }

    public async void OnTouchUpgradeUnit()
    {
        if(GameManager.I.MainUser.UserUnitData.OwnUnitList.ContainsKey(_currentUnitId))
        {
            await NetworkManager.I.SendUnitUpgradeRequest(_currentUnitId, 1, HandleUpgradeUnit);
        }
        else
        {
            await NetworkManager.I.SendUnitUnlockRequest(_currentUnitId, HandleUnlockUnit);
        }
    }

    private int GetUnitPrice(string userId)
    {
        int upgradePrice = unitSOList.GetUnitById(_currentUnitId).UpgradePrice;
        int currentLevel = new GetUnitLevel(_currentUnitId).Level;
        return new GetUnitUpgradePrice(upgradePrice, currentLevel).Price;
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


    }

    private void OnDestroy()
    {
        unitListUI.OnElementChanged -= RefreshUI;
    }
}