using MongoDB.Bson.Serialization.Attributes;
using System;

[Serializable]
public class UnitData
{
    public string UnitId { get; set; }
    public EUnitType UnitType { get; set; }
    public double UnitHealth { get; set; }
    public double UnitAttackPower { get; set; }
    public double UnitAttackTime { get; set; }
    public double UnitAttackRange { get; set; }
    public double UnitMoveSpeed { get; set; }
    public int UnitHitbackCount { get; set; }
    public int UnitDropMoneyAmount { get; set; }
    public UnitSpecialAbilityData UnitSpecialAbilityData { get; set; }
}

[Serializable]
public class UnitSpecialAbilityData
{
    public EUnitType[] TargetTypes;
    public ESpecialAbilityType[] SpecialAbilityDatas;
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
            UnitDropMoneyAmount = UnitData.UnitDropMoneyAmount
        };
    }
}
