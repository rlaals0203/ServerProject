using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatCompo : MonoBehaviour, IComponentCache
{
    private Entity _owner;

    public void InitializeComponent(Entity owner)
    {
        _owner = owner;
    }
}
