using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UnitDeployUI : ContentUI
{
    [SerializeField] private UnitSOList unitSOList;
    [SerializeField] private UnitListUI unitListUI;
    [SerializeField] private Image draggingUI;

    private Dictionary<int, string> _deployList = new();
    private UnitDeploySlotUI[] _deploySlots;
    private UnitSlotUI _currentUnitSlot;
    private int _currentSlotIndex;
    private string _currentUnitId;
    private bool _isDragging;


    protected override void Awake()
    {
        base.Awake();

        _deploySlots = GetComponentsInChildren<UnitDeploySlotUI>(true);
        unitListUI.OnElementChanged += HandleElementChanged;
        //unitListUI.InitializeUnitList(GameManager.I.MainUser.UserUnitData.OwnUnitList);

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

    private void HandleHoverDeploySlot(int slotIndex, bool isHovering)
    {
        _currentSlotIndex = isHovering ? slotIndex : -1;
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
        _currentUnitId = ui.GetUnitId();
        draggingUI.sprite = unitSOList.GetUnitById(_currentUnitId).UnitIcon;
        draggingUI.gameObject.SetActive(true);
    }

    private void HandleSlotDrop(UnitSlotUI ui)
    {
        if(_isDragging && string.IsNullOrEmpty(_currentUnitId) == false && _currentSlotIndex != -1)
        {
            ProcessDeploySlot();
        }

        _currentUnitId = string.Empty;
        _isDragging = false;
        draggingUI.gameObject.SetActive(false);
    }

    private void ProcessDeploySlot()
    {
        if(_deployList.TryGetValue(_currentSlotIndex, out var unitId))
        {
            if(_currentUnitId == unitId)
            {
                return;
            }
        }

        _deploySlots[_currentSlotIndex].Initialize(unitSOList.GetUnitById(_currentUnitSlot.GetUnitId()));
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