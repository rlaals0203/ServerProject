using System;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class HealthCompo : MonoBehaviour, IComponentCache, IAfterInit, IDamageable
{
    private Unit _owner;
    private StatCompo _statCompo;
    private StatBase _healthStat;

    public Action OnDead;
    public Action OnDamaged;


    public void InitializeComponent(Unit owner)
    {
        _owner = owner;
    }

    public void AfterInitialize()
    {
        _statCompo = _owner.Get<StatCompo>();
        _healthStat = _statCompo.GetStat(EStatType.Health);
    }

    public void OnHit(float damage)
    {
        _healthStat.SubstractStatValue(damage);
        OnDamaged?.Invoke();

        if (_healthStat.CurrentStat <= 0)
        {
            OnDead?.Invoke();
        }
    }
}
