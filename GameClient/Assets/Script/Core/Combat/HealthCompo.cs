using MINLib.Script.Core.Resource;
using System;
using UnityEngine;

public class HealthCompo : MonoBehaviour, IComponentCache, IAfterInit, IDamageable
{
    private Unit _owner;
    private StatCompo _statCompo;
    private StatBase _healthStat;

    public Action OnDead;
    public Action OnDamaged;
    public Action OnHitback;

    private int _damageStacked;
    private int _minHitbackHealth;


    public void InitializeComponent(Unit owner)
    {
        _owner = owner;
    }

    public void AfterInitialize()
    {
        _statCompo = _owner.Get<StatCompo>();
        _healthStat = _statCompo.GetStat(EStatType.Health);

        _minHitbackHealth = (int)_healthStat.CurrentStat / _owner.UnitData.UnitHitbackCount;
    }

    public void OnHit(float damage)
    {
        _healthStat.SubstractStatValue(damage);
        OnDamaged?.Invoke();
        _damageStacked += (int)damage;

        if(_damageStacked >= _minHitbackHealth)
        {
            _damageStacked = 0;
            OnHitback?.Invoke();
        }

        if (_healthStat.CurrentStat <= 0)
        {
            OnDead?.Invoke();
            Destroy(gameObject);
        }
    }
}
