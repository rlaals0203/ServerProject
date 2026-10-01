using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserUnitData
{
    public Dictionary<int, UnitData> DeployUnitList;
    public Dictionary<string, UnitData> OwnUnitList;
}

[Serializable]
public class UnitData
{
    public string UnitId;
    public int UnitLevel;
    public EUnitClassType unitClassType;
    public EUnitType UnitType;
    public int UnitHealth;
    public int UnitAttackPower;
    public float UnitAttackTime;
    public float UnitAttackRange;
    public float UnitMoveSpeed;
    public int UnitHitbackCount;
    public int UnitDropMoneyAmount;
    public UnitSpecialAbilityData UnitSpecialAbilityData;
    public List<ESpecialAbilityType> CachedSpecialAbility;
    public List<EUnitType> CachedTargetType;
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
