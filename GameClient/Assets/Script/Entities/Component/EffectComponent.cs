using MINLib.Script.Core.Resource;
using System;
using UnityEngine;

public class EffectComponent : MonoBehaviour, IComponentCache
{
    private Unit _owner;

    public void InitializeComponent(Unit owner)
    {
        _owner = owner;
    }
}