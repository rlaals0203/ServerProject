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