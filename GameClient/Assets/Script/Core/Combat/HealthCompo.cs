using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCompo : MonoBehaviour, IComponentCache
{
    private Unit _owner;
    private StatCompo _statCompo;

    public double CurrentHealth;

    public Action OnDead;
    public Action OnDamaged;

    public void InitializeComponent(Unit owner)
    {
        _owner = owner;
        _statCompo = owner.Get<StatCompo>();
    }

    public void GetDamage(Unit dealer)
    {

    }
}
