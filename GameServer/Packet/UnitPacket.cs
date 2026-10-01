[Serializable]
public class UnitUpgradePacketData : PacketBase
{
    public const string ROUTE = "RequestUpgradeUnit";
    public UnitData UnitData;
    public int TargetLevel;
}

[Serializable]
public class UnitUpgradePacketResultData : PacketResultBase
{
    public UnitData UnitData;
    public int TargetLevel;
    public UnitUpgradePacketResultData(EPacketResult result) : base(result) { }
}