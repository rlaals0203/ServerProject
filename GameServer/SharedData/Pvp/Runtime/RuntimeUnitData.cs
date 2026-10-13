using System.Numerics;

public class RuntimeUnitData
{
    public Vector3 Position { get; set; }
    public bool IsOpponent { get; set; }
}

public class UpdateAllUnitDataMessage : StandardMessageData
{
    public const string ROUTE = "UpdateAllUnitData";
    public List<RuntimeUnitData> AllUnitDatas { get; set; }
}