using System;
using TMPro;
using UnityEngine;

public class HealthCompo : MonoBehaviour, IComponentCache, IAfterInit, IDamageable
{
    protected Unit _owner;
    protected StatBase _healthStat;
    private StatCompo _statCompo;

    public bool IsDead { get; private set; }

    public Action OnDamaged;
    public Action OnHitback;

    private int _damageStacked;
    private int _minHitbackHealth;


    public void InitializeComponent(Unit owner)
    {
        _owner = owner;
        IsDead = false;
    }

    public virtual void AfterInitialize()
    {
        _statCompo = _owner.Get<StatCompo>();
        _healthStat = _statCompo.GetStat(EStatType.Health);

        _minHitbackHealth = (int)_healthStat.CurrentStat / _owner.UnitData.UnitHitbackCount;
        _damageStacked = 0;
    }

    public virtual void OnHit(float damage)
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
            SetDead();
        }
    }

    public void SetDead()
    {
        IsDead = true;
        _owner.OnDead?.Invoke(_owner);
    }
}
