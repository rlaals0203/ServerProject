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
    public int UnitLevel { get; set; }
    public EUnitClassType unitClassType { get; set; }
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
    public EUnitType[] TargetTypes { get; set; }
    public SpecialAbilityData[] SpecialAbilityDatas { get; set; }
}

[Serializable]
public class SpecialAbilityData
{
    public ESpecialAbilityType AbilityType { get; set; }
    public float Chance { get; set; }
    public float Duration { get; set; }
}