using System;
using System.Collections.Generic;

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
    public float Chance;
    public float Duration;
}