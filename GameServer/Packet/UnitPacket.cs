[Serializable]
public class UnitUpgradePacketData : PacketDataBase
{
    public const string ROUTE = "RequestUpgradeUnit";
    public UnitData UnitData;
    public int TargetLevel;
}

[Serializable]
public class UnitUpgradePacketResultData : PacketResultDataBase
{
    public UnitData UnitData;
    public int TargetLevel;
    public UnitUpgradePacketResultData(EPacketResult result) : base(result) { }
}