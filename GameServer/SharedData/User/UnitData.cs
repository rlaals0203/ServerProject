[Serializable]
public class UserUnitData
{
    public Dictionary<int, UnitData> DeployUnitList { get; set; }
    public Dictionary<string, UnitData> OwnUnitList { get; set; }
}

[Serializable]
public class UnitData
{
    public UnitStatData UnitStatData { get; set; }
    public int UnitLevel { get; set; }
    public int UnitHealth { get; set; }
    public int UnitCost { get; set; }
    public float UnitCooldown { get; set; }
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
    public float UnitCooldown { get; set; }
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
    public float UnitAttackRange { get; set; }
    public float UnitAttackDistance { get; set; }
    public float UnitDetectRange { get; set; }
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
