using System;
using System.Collections.Generic;

public class MakeRequestLoginPacket
{
    public UserLoginPacketData UserLoginPacketData;

    public MakeRequestLoginPacket(string userId, string password)
    {
        UserLoginPacketData userLoginPacket = new UserLoginPacketData
        {
            UserId = userId,
            Password = password
        };

        UserLoginPacketData = userLoginPacket;
    }
}

public class MakeRequestUnitUpgradePacket
{
    public UnitUpgradePacketData UnitUpgradePacketData;

    public MakeRequestUnitUpgradePacket(UnitData unitData, int targetLevel)
    {
        UnitUpgradePacketData unitUpgradePacket = new UnitUpgradePacketData
        {
            UnitData = unitData,
            TargetLevel = targetLevel
        };

        UnitUpgradePacketData = unitUpgradePacket;
    }
}

public class MakeRequestUnitDeployPacket
{
    public UnitDeployPacketData UnitDeployPacketData;

    public MakeRequestUnitDeployPacket(Dictionary<int, UnitData> deployUnits)
    {
        UnitDeployPacketData unitDeployPacketData = new UnitDeployPacketData
        {
            DeployUnits = deployUnits
        };

        UnitDeployPacketData = unitDeployPacketData;
    }
}