using System;
using UnityEngine;

public class Castle : Unit
{
    [SerializeField] private Transform cameraTrm;

    public Action<bool> OnDead;

    public override void Initialize(UnitData unitData, ETeamType teamType)
    {
        base.Initialize(unitData, teamType);
        IsFreeze = true;
    }

    public Transform GetCameraTrm() => cameraTrm;
}