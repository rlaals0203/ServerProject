using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealthCompo : MonoBehaviour, IComponentCache
{
    private Entity _owner;
    private StatCompo _statCompo;

    public double CurrentHealth;

    public Action OnDead;
    public Action OnDamaged;

    public void InitializeComponent(Entity owner)
    {
        _owner = owner;
        _statCompo = owner.Get<StatCompo>();
    }

    public void GetDamage(Entity dealer)
    {

    }
}
