[Serializable]
public class UnitUpgradePacketData : StandardPacketData
{
    public const string ROUTE = "RequestUpgradeUnit";
    public UnitData UnitData;
    public int TargetLevel;
}

[Serializable]
public class UnitUpgradePacketResultData : StandardPacketResultData
{
    public UnitData UnitData;
    public int TargetLevel;
    public UnitUpgradePacketResultData(EPacketResult result) : base(result) { }
}

[Serializable]
public class UnitDeployPacketData : StandardPacketData
{
    public const string ROUTE = "RequestUnitDeploy";
    public UnitData[] ChangedDeck { get; set; }
}

[Serializable]
public class UnitDeployPacketResultData : StandardPacketResultData
{
    public UnitData[] ChangedDeck { get; set; }
    public UnitDeployPacketResultData(EPacketResult result) : base(result) { }
}



