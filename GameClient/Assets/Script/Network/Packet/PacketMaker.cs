using System;
using System.Collections.Generic;

public class MakeRequestUnitUnlockPacket
{
    public UnitUnlockPacketData UnitUnlockPacket;

    public MakeRequestUnitUnlockPacket(string unitId)
    {
        UnitUnlockPacketData unitUnlockPacket = new UnitUnlockPacketData()
        {
            UnitId = unitId
        };

        UnitUnlockPacket = unitUnlockPacket;
    }
}

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

    public MakeRequestUnitUpgradePacket(string unitId, int targetLevel)
    {
        UnitUpgradePacketData unitUpgradePacket = new UnitUpgradePacketData
        {
            UnitId = unitId,
            TargetLevel = targetLevel
        };

        UnitUpgradePacketData = unitUpgradePacket;
    }
}

public class MakeRequestUnitDeployPacket
{
    public UnitDeployPacketData UnitDeployPacketData;

    public MakeRequestUnitDeployPacket(Dictionary<int, string> deployUnits)
    {
        UnitDeployPacketData unitDeployPacketData = new UnitDeployPacketData
        {
            DeployUnits = deployUnits
        };

        UnitDeployPacketData = unitDeployPacketData;
    }
}