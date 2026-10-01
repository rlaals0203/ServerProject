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
    public float Chance;
    public float Duration;
}