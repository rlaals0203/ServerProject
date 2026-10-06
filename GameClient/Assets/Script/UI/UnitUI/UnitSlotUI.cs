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
    [SerializeField] private Image unitIcon;

    [SerializeField] private TMP_Text unitUpgradeCostText;
    [SerializeField] private GameObject unitUpgradeUI;

    public event Action<UnitSlotUI> OnUnitDragStart;
    public event Action<UnitSlotUI> OnUnitDragEnd;

    private UnitData _currentUnitData;

    public void InitializeUI(UnitData unitData)
    {
        unitNameText.text = unitData.UnitName;
        unitLevelText.text = unitData.UnitLevel.ToString();
        unitCostText.text = unitData.UnitCost.ToString();

        _currentUnitData = unitData;
    }

    public string GetUnitId()
    {
        return _currentUnitData == null ? string.Empty : _currentUnitData.UnitId;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnUnitDragStart?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnUnitDragEnd?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData) { }
}