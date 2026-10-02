using System;
using System.Collections.Generic;

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
    public Dictionary<int, UnitData> DeployUnits { get; set; }
}

[Serializable]
public class UnitDeployPacketResultData : StandardPacketResultData
{
    public Dictionary<int, UnitData> DeployUnits { get; set; }
    public UnitDeployPacketResultData(EPacketResult result) : base(result) { }
}



