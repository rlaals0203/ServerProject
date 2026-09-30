using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitData
{
    public string UnitId { get; set; }
    public EUnitType UnitType { get; set; }
    public int UnitHealth { get; set; }
    public int UnitAttackPower { get; set; }
    public float UnitAttackTime { get; set; }
    public float UnitAttackRange { get; set; }
    public float UnitMoveSpeed { get; set; }
    public int UnitHitbackCount { get; set; }
    public int UnitDropMoneyAmount { get; set; }
    public UnitSpecialAbilityData UnitSpecialAbilityData { get; set; }
    public List<ESpecialAbilityType> CachedSpecialAbility { get; set; }
    public List<EUnitType> CachedTargetType { get; set; }
}

[Serializable]
public class UnitSpecialAbilityData
{
    public EUnitType[] TargetTypes;
    public SpecialAbilityData[] SpecialAbilityDatas;
}

[Serializable]
public class SpecialAbilityData
{
    public ESpecialAbilityType AbilityType;
    [Range(0f, 1f)]
    [Tooltip("확률형 능력이 아니라면 항상 1로 적용됩니다.")]
    public float Chance;
    public float Duration;
}

public class MakeUnitData
{
    UnitData UnitData;

    public MakeUnitData(UnitSO unitData)
    {
        UnitData = new UnitData
        {
            UnitId = UnitData.UnitId,
            UnitType = UnitData.UnitType,
            UnitHealth = UnitData.UnitHealth,
            UnitAttackPower = UnitData.UnitAttackPower,
            UnitAttackTime = UnitData.UnitAttackTime,
            UnitAttackRange = UnitData.UnitAttackRange,
            UnitMoveSpeed = UnitData.UnitMoveSpeed,
            UnitHitbackCount = UnitData.UnitHitbackCount,
            UnitDropMoneyAmount = UnitData.UnitDropMoneyAmount,
            UnitSpecialAbilityData = UnitData.UnitSpecialAbilityData
        };
    }
}
