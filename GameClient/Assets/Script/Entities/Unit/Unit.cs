using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Entity
{
    public UnitData UnitData { get; private set; }

    protected override void Awake() { }

    public void InitializeUnit(UnitData unitData)
    {
        UnitData = unitData;
        base.Initialize();
    }
}
