using System.Collections.Generic;

public class RuntimeUnitData
{
    public int RuntimeId { get; set; }
    public string UnitId { get; set; }
    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public bool IsOpponent { get; set; }
    public EStateType State { get; set; }
}

public class UpdateAllUnitDataMessage : StandardMessageData
{
    public const string ROUTE = "UpdateAllUnitData";
    public List<RuntimeUnitData> AllUnitDatas { get; set; }
}
