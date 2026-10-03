using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitDeployUI : ContentUI
{
    [SerializeField] private UnitSOList unitSOList;
    [SerializeField] private Image draggingUI;

    private UnitSlotUI[] _unitSlots;
    private UnitDeploySlotUI[] _deploySlots;

    private UnitSlotUI _draggingSlot;
    private bool _isDragging;


    protected override void Awake()
    {
        base.Awake();

        _unitSlots = GetComponentsInChildren<UnitSlotUI>(true);
        _deploySlots = GetComponentsInChildren<UnitDeploySlotUI>(true);

        foreach(var unitSlot in _unitSlots)
        {
            unitSlot.InitializeUI(new MakeUnitData(unitSOList.UnitList[0]).UnitData);
            unitSlot.OnUnitDragStart += HandleSlotDragStart;
            unitSlot.OnUnitDragEnd += HandleSlotDrop;
        }

        draggingUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_isDragging)
        {
            UpdateDrag();
        }
    }

    private void UpdateDrag()
    {
        if (_draggingSlot == null)
            return;

        draggingUI.transform.position = UserInputUtility.GetCurrentPointer();
    }

    private void HandleSlotDragStart(UnitSlotUI ui)
    {
        _draggingSlot = ui;
        _isDragging = true;
        draggingUI.sprite = unitSOList.GetUnitById(ui.GetUnitId()).UnitIcon;
        draggingUI.gameObject.SetActive(true);
    }

    private void HandleSlotDrop(UnitSlotUI ui)
    {
        _draggingSlot = null;
        _isDragging = false;

        draggingUI.gameObject.SetActive(false);
    }
}