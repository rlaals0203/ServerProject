[Serializable]
public class PVPBattleInfoDataMessage : StandardMessageData
{
    public const string ROUTE = "UpdateBattleFieldData";
    public List<RuntimeUnitData> AllUnitDatas { get; set; }
    public UpdateUnitCooldownData UnitCooldownData { get; set; }
    public float CurrentMoney { get; set; }
    public float OpponentCurrentMoney { get; set; }
}

[Serializable]
public class RuntimeUnitData
{
    public string RuntimeId { get; set; }
    public string UnitId { get; set; }
    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public bool IsOpponent { get; set; }
    public bool IsDead { get; set; }
    public EStateType State { get; set; }
    public ESpecialAbilityType AbilityStatus { get; set; }
    public int FreezeApplyCount { get; set; }
    public int HitbackCount { get; set; }
}


[Serializable]
public class UpdateUnitCooldownData
{
    public Dictionary<string, float>  UnitCooldowns { get; set; }
    public Dictionary<string, float>  OpponentUnitCooldowns { get; set; }
}

[Serializable]
public class UpdateCastleDataMessage : StandardMessageData
{
    public const string ROUTE = "UpdateCastleData";
    public CastleData UnitCastleData { get; set; }
    public CastleData OpponentCastleData { get; set; }
}

[Serializable]
public class CastleData
{
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }
    public float PositionX { get; set; }
    public float PositionY { get; set; }
}

[Serializable]
public class PlayHitEffectDataMessage : StandardMessageData
{
    public const string ROUTE = "PlayHitEffectData";
    public bool IsOpponent { get; set; }
    public float PosX { get; set; }
    public float PosY { get; set; }
}