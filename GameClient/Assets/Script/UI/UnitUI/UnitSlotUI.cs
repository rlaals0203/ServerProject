using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitSlotUI : UIBase, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private TMP_Text unitNameText;
    [SerializeField] private TMP_Text unitLevelText;
    [SerializeField] private TMP_Text unitCostText;
    [SerializeField] private TMP_Text unitUpgradeCost;
    [SerializeField] private Image unitIcon;
    [SerializeField] private GameObject lockObject;

    [SerializeField] private TMP_Text unitUpgradeCostText;
    [SerializeField] private GameObject unitUpgradeUI;

    public event Action<UnitSlotUI> OnUnitDragStart;
    public event Action<UnitSlotUI, PointerEventData> OnUnitDragEnd;
    public event Action<UnitSlotUI, PointerEventData> OnUnitDrag;

    private string _currentUnitId;

    public void RefreshUI(UnitData unitData, UnitSO unitSO)
    {
        bool isLockd = unitData.UnitLevel == 0;
        lockObject.SetActive(isLockd);
        _currentUnitId = unitData.UnitStatData.UnitId;

        unitLevelText.text = unitData.UnitLevel.ToString();
        unitCostText.text = unitData.UnitCost.ToString();
        unitUpgradeCost.text = new GetUnitUpgradePrice(unitData.UnitStatData.UpgradePrice, unitData.UnitLevel).Price.ToString();
        unitNameText.text = unitSO.UnitName;
        unitIcon.sprite = unitSO.UnitIcon;
    }

    public string GetUnitId()
    {
        return _currentUnitId;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnUnitDragStart?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnUnitDragEnd?.Invoke(this, eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnUnitDrag?.Invoke(this, eventData);
    }
}