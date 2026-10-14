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
}

[Serializable]
public class UpdateAllUnitDataMessage : StandardMessageData
{
    public const string ROUTE = "UpdateAllUnitData";
    public List<RuntimeUnitData> AllUnitDatas { get; set; }
}