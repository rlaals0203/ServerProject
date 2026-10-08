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
    [SerializeField] private GameObject lockObject;

    [SerializeField] private TMP_Text unitUpgradeCostText;
    [SerializeField] private GameObject unitUpgradeUI;

    public event Action<UnitSlotUI> OnUnitDragStart;
    public event Action<UnitSlotUI> OnUnitDragEnd;

    private const string _unknownName = "??????";
    private const string _unknownCost = "???";

    private string _currentUnitId;

    public void InitializeUI(UnitSO unitSO)
    {
        if(GameManager.I.MainUser.UserUnitData.OwnUnitList.TryGetValue(unitSO.UnitId, out var unitData))
        {
            unitNameText.text = unitData.UnitStatData.UnitName;
            unitLevelText.text = unitData.UnitLevel.ToString();
            unitCostText.text = unitData.UnitCost.ToString();
            lockObject.SetActive(false);
        }
        else
        {
            unitNameText.text = _unknownName;
            unitCostText.text = _unknownCost;
            lockObject.SetActive(true);
        }

        _currentUnitId = unitSO.UnitId;
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
        OnUnitDragEnd?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData) { }
}