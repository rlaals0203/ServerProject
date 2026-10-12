using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitDeckSlotUI : UIBase
{
    [SerializeField] private UIBarComponent cooldownBar;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private GameObject lockObject;
    [SerializeField] private Image unitIcon;

    public Action<UnitData> OnSpawnUnit;

    private UnitData _unitData;
    private float _currentTime;
    private float _unitCooltime;

    private bool _canSpawn;


    public void InitializeSlot(UnitData unitData, UnitSOList unitSOList)
    {
        if(unitData == null)
        {
            unitIcon.sprite = null;
            cooldownBar.gameObject.SetActive(false);
            return;
        }

        _unitData = unitData;
        _unitCooltime = unitData.UnitCooldown;
        _currentTime = 0f;
        _canSpawn = true;

        costText.text = $"{unitData.UnitStatData.UnitCost}¿ø";
        unitIcon.sprite = unitSOList.GetUnitById(unitData.UnitStatData.UnitId).UnitIcon;
        cooldownBar.gameObject.SetActive(true);
        SetLockState(false);
    }

    public void OnTouchSpawnUnit()
    {
        if(_canSpawn == false || _unitData == null)
        {
            return;
        }

        _currentTime = Time.time;
        _canSpawn = false;
        SetLockState(true);

        OnSpawnUnit?.Invoke(_unitData);
    }

    private void Update()
    {
        if (_unitData == null)
            return;

        if(_canSpawn == false && Time.time - _currentTime >= _unitCooltime)
        {
            _canSpawn = true;
            SetLockState(false);
            return;
        }

        cooldownBar.SetBar(Time.time - _currentTime, _unitCooltime);
    }

    private void SetLockState(bool isLock)
    {
        cooldownBar.gameObject.SetActive(isLock);
        lockObject.gameObject.SetActive(isLock);
        costText.gameObject.SetActive(!isLock);
    }
}