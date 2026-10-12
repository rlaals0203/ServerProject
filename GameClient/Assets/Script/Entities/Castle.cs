using System;
using UnityEngine;

public class Castle : Unit
{
    public Action OnDead;

    public override void Initialize(UnitData unitData, ETeamType teamType)
    {
        base.Initialize(unitData, teamType);
        IsFreeze = true;
    }
}