using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserUnitData
{
    public Dictionary<int, UnitData> DeployUnitList { get; set; }
    public Dictionary<string, UnitData> OwnUnitList { get; set; }
}

[Serializable]
public class UnitData
{
    public UnitStatData UnitStatData { get; }
    public int UnitLevel { get; set; }
    public int UnitHealth { get; set; }
    public int UnitCost { get; set; }
    public int UnitCooldown { get; set; }
    public float UnitMoveSpeed { get; set; }
    public int UnitHitbackCount { get; set; }
    public int UnitDropMoneyAmount { get; set; }
    public UnitAttackData UnitAttackData { get; set; }
}

[Serializable]
public class UnitStatData
{
    public string UnitId { get; set; }
    public string UnitName { get; set; }
    public int UnlockPrice { get; set; }
    public int UpgradePrice { get; set; }
    public int UnitHealth { get; set; }
    public int UnitCost { get; set; }
    public int UnitCooldown { get; set; }
    public float UnitMoveSpeed { get; set; }
    public int UnitHitbackCount { get; set; }
    public int UnitDropMoneyAmount { get; set; }
    public EUnitClassType UnitClassType { get; set; }
    public EUnitType[] UnitType { get; set; }
    public UnitAttackData UnitAttackData { get; set; }
    public UnitSpecialAbilityData UnitSpecialAbilityData { get; set; }
    public List<ESpecialAbilityType> CachedSpecialAbility { get; set; }
    public List<EUnitType> CachedTargetType { get; set; }
}

[Serializable]
public class UnitAttackData
{
    public int UnitAttackPower { get; set; }
    public float UnitAttackSpeed { get; set; }
    public float AttackRange { get; set; }
    public float AttackDistance { get; set; }
    public float DetectRange { get; set; }
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

public class MakeUnitStatData
{
    public UnitStatData UnitStatData;

    public MakeUnitStatData(UnitSO unitData)
    {
        UnitStatData = new UnitStatData
        {
            UnitId = unitData.UnitId,
            UnitName = unitData.UnitName,
            UnlockPrice = unitData.UnlockPrice,
            UpgradePrice = unitData.UpgradePrice,
            UnitCost = unitData.UnitCost,
            UnitCooldown = unitData.UnitCooldown,
            UnitType = unitData.UnitType,
            UnitHealth = unitData.UnitHealth,
            UnitMoveSpeed = unitData.UnitMoveSpeed,
            UnitHitbackCount = unitData.UnitHitbackCount,
            UnitDropMoneyAmount = unitData.UnitDropMoneyAmount,
            UnitSpecialAbilityData = unitData.UnitSpecialAbilityData
        };

        UnitStatData.UnitAttackData = new UnitAttackData()
        {
            UnitAttackPower = unitData.UnitAttackPower,
            UnitAttackSpeed = unitData.AttackSpeed,
            AttackRange = unitData.UnitAttackRange,
            AttackDistance = unitData.UnitAttackDistance,
            DetectRange = unitData.DetectRange
        };

        UnitStatData.UnitSpecialAbilityData = new UnitSpecialAbilityData()
        {
            SpecialAbilityDatas = unitData.UnitSpecialAbilityData.SpecialAbilityDatas,
            TargetTypes = unitData.UnitSpecialAbilityData.TargetTypes
        };
    }
}
