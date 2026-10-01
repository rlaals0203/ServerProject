using GameServer;
using Microsoft.AspNetCore.Mvc;

[Route(UserLoginPacketData.ROUTE)]
public partial class BasePacketController : ControllerBase
{
    [HttpPost(UserLoginPacketData.ROUTE)]
    public async Task<ActionResult<UserLoginPacketResultData>> PostUserLoginRequest([FromBody]UserLoginPacketData userLoginPacketData)
    {
        UserBaseData userBaseData = await gameDataManager.RequestLoginUser();
        RequestLoginProcess loginProcess = new RequestLoginProcess();
        await loginProcess.ProcessAsync(userLoginPacketData.UserId, userLoginPacketData.Password);
        
        UserLoginPacketResultData resultData = new UserLoginPacketResultData(loginProcess.Result)
        {
            UserId = loginProcess.UserId,
            Password = loginProcess.Password
        };
        
        return resultData;
    }

    [HttpPost(UnitUpgradePacketData.ROUTE)]
    public async Task<ActionResult<UnitUpgradePacketResultData>> PostUserLoginRequest([FromBody]UnitUpgradePacketData unitUpgradePacketData)
    {
        RequestUnitUpgradeProcess unitUpgradeProcess = new RequestUnitUpgradeProcess();
        await unitUpgradeProcess.ProcessAsync(unitUpgradePacketData.UnitData, unitUpgradePacketData.TargetLevel);
        
        UnitUpgradePacketResultData resultData = new UnitUpgradePacketResultData(unitUpgradeProcess.Result)
        {
            UnitData = unitUpgradeProcess.UnitData,
            TargetLevel = unitUpgradeProcess.TargetLevel
        };
        
        return resultData;
    }
}
