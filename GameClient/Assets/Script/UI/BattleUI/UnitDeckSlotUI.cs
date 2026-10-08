using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitDeckSlotUI : UIBase
{
    [SerializeField] private UIBarComponent cooldownBar;
    [SerializeField] private GameObject lockObject;
    [SerializeField] private Image unitIcon;

    public Action<UnitData> OnSpawnUnit;

    private UnitData _unitData;
    private float _currentTime;
    private float _unitCooltime;

    private bool _canSpawn;


    public void InitializeSlot(UnitData unitData)
    {
        _unitData = unitData;
        _unitCooltime = unitData.UnitCooldown;
        _currentTime = 0f;
        _canSpawn = true;

        SetLockState(false);
    }

    public void OnTouchSpawnUnit()
    {
        if(_canSpawn == false)
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
    }
}