using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UnitDeployUI : ContentUI
{
    [SerializeField] private UnitSOList unitSOList;
    [SerializeField] private UnitListUI unitListUI;
    [SerializeField] private Image draggingUI;

    private UnitDeploySlotUI[] _deploySlots;
    private UnitDeploySlotUI _currentDeploySlot;
    private UnitSlotUI _currentUnitSlot;

    private bool _isDragging;


    protected override void Awake()
    {
        base.Awake();

        _deploySlots = GetComponentsInChildren<UnitDeploySlotUI>(true);
        unitListUI.OnElementChanged += HandleElementChanged;

        unitListUI.InitializeUnitList(unitSOList.GetUnitData());
        foreach (var unitSlot in unitListUI.GetAllElement())
        {
            unitSlot.OnUnitDragStart += HandleSlotDragStart;
            unitSlot.OnUnitDragEnd += HandleSlotDrop;
        }

        foreach(var deploySlot in _deploySlots)
        {
            deploySlot.OnHoverSlot += HandleHoverDeploySlot;
        }

        draggingUI.gameObject.SetActive(false);
    }

    private void HandleElementChanged(UnitSlotUI slotUI)
    {
        _currentUnitSlot = slotUI;
    }

    private void HandleHoverDeploySlot(UnitDeploySlotUI slot, bool isHovering)
    {
        _currentDeploySlot = isHovering ? slot : null;
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
        if (_isDragging == false)
            return;

        draggingUI.transform.position = UserInputUtility.GetCurrentPointer();
    }

    private void HandleSlotDragStart(UnitSlotUI ui)
    {
        _isDragging = true;
        draggingUI.sprite = unitSOList.GetUnitById(ui.GetUnitId()).UnitIcon;
        draggingUI.gameObject.SetActive(true);
    }

    private void HandleSlotDrop(UnitSlotUI ui)
    {
        if(_isDragging && _currentDeploySlot  != null)
        {
            ProcessDeploySlot();
        }

        _isDragging = false;
        draggingUI.gameObject.SetActive(false);
    }

    private void ProcessDeploySlot()
    {
        _currentDeploySlot.Initialize(unitSOList.GetUnitById(_currentUnitSlot.GetUnitId()));
    }

    private void OnDestroy()
    {
        unitListUI.OnElementChanged -= HandleElementChanged;

        foreach (var unitSlot in unitListUI.GetAllElement())
        {
            unitSlot.OnUnitDragStart -= HandleSlotDragStart;
            unitSlot.OnUnitDragEnd -= HandleSlotDrop;
        }

        foreach (var deploySlot in _deploySlots)
        {
            deploySlot.OnHoverSlot -= HandleHoverDeploySlot;
        }
    }
}