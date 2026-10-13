public class RequestSpawnUnitData : StandardMessageData
{
    public const string ROUTE = "RequestUnitSpawn";
    public string UnitId { get; set; }
    public bool IsOpponent { get; set; }
}

public class RequestUpgradeWalletData : StandardMessageData
{
    public const string ROUTE = "RequestUpgradeWallet";
}