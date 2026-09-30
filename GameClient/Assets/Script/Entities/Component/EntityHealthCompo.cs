using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealthCompo : MonoBehaviour, IComponentCache
{
    private Entity _owner;

    public double CurrentHealth;

    public void InitializeComponent(Entity owner)
    {
        _owner = owner;
    }
}
