using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitDeckSlotUI : UIBase
{
    [SerializeField] private UIBarComponent cooldownBar;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private GameObject lockObject;
    [SerializeField] private Image unitIcon;

    private BattleField _battleField;
    private UnitData _unitData;
    private float _currentTime;
    private float _unitCooltime;
    private Action<UnitData> _spawnRequest;

    private bool _canSpawn;

    public Action<UnitData> OnSpawnUnit;

    public void InitializeSlot(UnitData unitData, UnitSOList unitSOList, BattleField battleField)
    {
        _spawnRequest = null;
        if(unitData == null)
        {
            unitIcon.sprite = null;
            cooldownBar.gameObject.SetActive(false);
            return;
        }

        _unitData = unitData;
        _battleField = battleField;
        _unitCooltime = unitData.UnitCooldown;
        _currentTime = 0f;

        costText.text = $"{unitData.UnitStatData.UnitCost}¿ø";
        unitIcon.sprite = unitSOList.GetUnitById(unitData.UnitStatData.UnitId).UnitIcon;
        SetLockState(true);
    }

    public void InitializeView(UnitData unitData, UnitSOList unitSOList, Action<UnitData> spawnRequest)
    {
        _unitData = unitData;
        _spawnRequest = spawnRequest;
        _canSpawn = false;
        cooldownBar.gameObject.SetActive(false);

        if(unitData == null)
        {
            unitIcon.sprite = null;
            costText.gameObject.SetActive(false);
            SetLockState(true);
            return;
        }

        _unitCooltime = unitData.UnitCooldown;
        costText.text = $"{unitData.UnitStatData.UnitCost}¿ø";
        costText.gameObject.SetActive(true);
        unitIcon.sprite = unitSOList.GetUnitById(unitData.UnitStatData.UnitId).UnitIcon;
        SetLockState(true);
    }

    public void UpdatePvpView(Dictionary<string, float> cooldowns, float money)
    {
        if(_unitData == null)
            return;

        if(cooldowns.TryGetValue(_unitData.UnitStatData.UnitId, out var remainCooldown) == false)
        {
            _canSpawn = false;
            SetLockState(true);
            return;
        }

        remainCooldown = Mathf.Clamp(remainCooldown, 0f, _unitCooltime);
        _canSpawn = remainCooldown <= 0f && money >= _unitData.UnitCost;
        cooldownBar.gameObject.SetActive(remainCooldown > 0f);
        costText.gameObject.SetActive(remainCooldown <= 0f);

        if(remainCooldown > 0f)
            cooldownBar.SetBar(_unitCooltime - remainCooldown, _unitCooltime);

        SetLockState(_canSpawn == false);
    }

    public void OnTouchSpawnUnit()
    {
        if (_unitData == null)
            return;

        if (_spawnRequest != null)
        {
            if (_canSpawn == false)
                return;

            _canSpawn = false;
            SetLockState(true);
            _spawnRequest(_unitData);
            return;
        }

        if (_canSpawn == false)
            return;

        SetLockState(true);
        cooldownBar.gameObject.SetActive(true);
        costText.gameObject.SetActive(false);

        _currentTime = Time.time;
        OnSpawnUnit?.Invoke(_unitData);
    }

    private void Update()
    {
        if (_unitData == null || _spawnRequest != null)
            return;

        if(IsEnoughCooltime())
        {
            costText.gameObject.SetActive(true);
            cooldownBar.gameObject.SetActive(false);
        }

        _canSpawn = IsEnoughCooltime() && HasEnoughMoney();
        cooldownBar.SetBar(GetCooltime(), _unitCooltime);
        SetLockState(!IsEnoughCooltime() || !HasEnoughMoney());
    }

    private bool HasEnoughMoney() => _battleField.GetMoney() >= _unitData.UnitCost;
    private bool IsEnoughCooltime() => GetCooltime() >= _unitCooltime;

    private float GetCooltime() => Time.time - _currentTime;

    private void SetLockState(bool isLock)
    {
        lockObject.gameObject.SetActive(isLock);
    }
}