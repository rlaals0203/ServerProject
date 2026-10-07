using System;
using UnityEngine;

public class HealthCompo : MonoBehaviour, IComponentCache, IDamageable
{
    private Unit _owner;
    private StatCompo _statCompo;
    private StatBase _healthStat;

    public Action OnDead;
    public Action OnDamaged;

    public void InitializeComponent(Unit owner)
    {
        _owner = owner;
        _statCompo = owner.Get<StatCompo>();
        _healthStat = _statCompo.GetStat(EStatType.Health);
    }

    public void Hit(float damage)
    {
        _healthStat.SubstractStatValue(damage);
        OnDamaged?.Invoke();

        if (_healthStat.CurrentStat <= 0)
        {
            OnDead?.Invoke();
        }
    }
}
