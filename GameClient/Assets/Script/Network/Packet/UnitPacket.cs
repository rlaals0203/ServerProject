using System;
using System.Collections.Generic;

[Serializable]
public class UnitUnlockPacketData : StandardPacketData
{
    public const string ROUTE = "RequestUnitUnlock";
    public string UnitId { get; set; }
}

[Serializable]
public class UnitUnlockPacketResultData : StandardPacketResultData
{
    public string UnitId { get; set; }
    public UnitUnlockPacketResultData(EPacketResult result) : base(result) { }
}

[Serializable]
public class UnitUpgradePacketData : StandardPacketData
{
    public const string ROUTE = "RequestUnitUpgrade";
    public string UnitId { get; set; }
    public int TargetLevel { get; set; }
}

[Serializable]
public class UnitUpgradePacketResultData : StandardPacketResultData
{
    public string UnitId { get; set; }
    public int UnitLevel { get; set; }
    public UnitUpgradePacketResultData(EPacketResult result) : base(result) { }
}

[Serializable]
public class UnitDeployPacketData : StandardPacketData
{
    public const string ROUTE = "RequestUnitDeploy";
    public Dictionary<string, string> DeployUnits { get; set; }
}

[Serializable]
public class UnitDeployPacketResultData : StandardPacketResultData
{
    public Dictionary<string, string> DeployUnits { get; set; }
    public UnitDeployPacketResultData(EPacketResult result) : base(result) { }
}



