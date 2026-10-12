using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitDeployUI : ContentUI
{
    [SerializeField] private UnitSOList unitSOList;
    [SerializeField] private UnitListUI unitListUI;
    [SerializeField] private Image draggingUI;

    private Dictionary<string, string> _deployList = new();
    private UnitDeploySlotUI[] _deploySlots;
    private int _currentSlotIndex = -1;

    protected override void Awake()
    {
        base.Awake();

        _deploySlots = GetComponentsInChildren<UnitDeploySlotUI>(true);
        unitListUI.InitializeUnitList(GameManager.I.MainUser.UserUnitData.OwnUnitList);

        foreach (var unitSlot in unitListUI.GetAllElement())
        {
            unitSlot.OnUnitDragStart += HandleSlotDragStart;
            unitSlot.OnUnitDragEnd += HandleSlotDrop;
            unitSlot.OnUnitDrag += HandleDrag;
        }

        foreach (var deploySlot in _deploySlots)
        {
            deploySlot.OnHoverSlot += HandleHoverDeploySlot;
        }

        var savedUnits = GameManager.I.MainUser.UserUnitData.DeployUnitList;
        if (savedUnits != null)
        {
            for (int i = 0; i < _deploySlots.Length; i++)
            {
                if (savedUnits.TryGetValue(i.ToString(), out var unit) == false)
                    continue;

                string unitId = unit.UnitStatData.UnitId;
                if (_deployList.ContainsValue(unitId))
                    continue;

                int index = _deployList.Count;
                _deployList[index.ToString()] = unitId;
                _deploySlots[index].Initialize(unitSOList.GetUnitById(unitId));
            }
        }

        draggingUI.raycastTarget = false;
        draggingUI.gameObject.SetActive(false);
    }

    private void HandleDrag(UnitSlotUI ui, PointerEventData eventData)
    {
        draggingUI.transform.position = eventData.position;
    }

    private void HandleHoverDeploySlot(int slotIndex, bool isHovering)
    {
        if (isHovering)
        {
            _currentSlotIndex = slotIndex;
        }
        else if (_currentSlotIndex == slotIndex)
        {
            _currentSlotIndex = -1;
        }
    }

    private void HandleSlotDragStart(UnitSlotUI ui)
    {
        draggingUI.sprite = unitSOList.GetUnitById(ui.GetUnitId()).UnitIcon;
        draggingUI.gameObject.SetActive(true);
    }

    private async void HandleSlotDrop(UnitSlotUI ui, PointerEventData eventData)
    {
        int index = _currentSlotIndex;
        string unitId = ui.GetUnitId();

        _currentSlotIndex = -1;
        draggingUI.gameObject.SetActive(false);

        if (string.IsNullOrEmpty(unitId))
        {
            return;
        }

        if (index < 0 || index >= _deploySlots.Length || string.IsNullOrEmpty(unitId))
        {
            return;
        }

        if (_deployList.ContainsValue(unitId))
        {
            return;
        }

        int idx = Math.Min(index, _deployList.Count);
        _deployList[idx.ToString()] = unitId;
        await NetworkManager.I.SendUnitDeployRequest(_deployList, HandleDeployUnit);
    }

    private void HandleDeployUnit(UnitDeployPacketResultData data)
    {
        if(data.Result != EPacketResult.Ok)
        {
            return;
        }

        var userUnitData = GameManager.I.MainUser.UserUnitData;
        var ownUnits = userUnitData.OwnUnitList;
        userUnitData.DeployUnitList.Clear();
        _deployList.Clear();

        foreach (var deployData in data.DeployUnits)
        {
            _deployList[deployData.Key] = deployData.Value;
            userUnitData.DeployUnitList[deployData.Key] = ownUnits[deployData.Value];
        }

        for (int i = 0; i < _deploySlots.Length; i++)
        {
            if (_deployList.TryGetValue(i.ToString(), out var unitId))
            {
                _deploySlots[i].Initialize(unitSOList.GetUnitById(unitId));
            }
            else
            {
                _deploySlots[i].Initialize(null);
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var unitSlot in unitListUI.GetAllElement())
        {
            unitSlot.OnUnitDragStart -= HandleSlotDragStart;
            unitSlot.OnUnitDragEnd -= HandleSlotDrop;
            unitSlot.OnUnitDrag -= HandleDrag;
        }

        foreach (var deploySlot in _deploySlots)
        {
            deploySlot.OnHoverSlot -= HandleHoverDeploySlot;
        }
    }
}