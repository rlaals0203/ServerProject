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
    public string UnitId { get; set; }
    public string UnitName { get; set; }
    public int UnitLevel { get; set; }
    public int UnlockPrice { get; set; }
    public int UpgradePrice { get; set; }
    public EUnitClassType unitClassType { get; set; }
    public EUnitType UnitType { get; set; }
    public int UnitHealth { get; set; }
    public int UnitAttackPower { get; set; }
    public int UnitCost { get; set; }
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
    public UnitData UnitData;

    public MakeUnitData(UnitSO unitData)
    {
        UnitData = new UnitData
        {
            UnitId = unitData.UnitId,
            UnitName = unitData.UnitName,
            UnitLevel = 1,
            UnlockPrice = unitData.UnlockPrice,
            UpgradePrice = unitData.UpgradePrice,
            UnitCost = unitData.UnitCost,
            UnitType = unitData.UnitType,
            UnitHealth = unitData.UnitHealth,
            UnitAttackPower = unitData.UnitAttackPower,
            UnitAttackTime = unitData.UnitAttackTime,
            UnitAttackRange = unitData.UnitAttackRange,
            UnitMoveSpeed = unitData.UnitMoveSpeed,
            UnitHitbackCount = unitData.UnitHitbackCount,
            UnitDropMoneyAmount = unitData.UnitDropMoneyAmount,
            UnitSpecialAbilityData = unitData.UnitSpecialAbilityData
        };

        UnitData.UnitSpecialAbilityData = new UnitSpecialAbilityData()
        {
            SpecialAbilityDatas = unitData.UnitSpecialAbilityData.SpecialAbilityDatas,
            TargetTypes = unitData.UnitSpecialAbilityData.TargetTypes
        };
    }
}
