using GameServer;
using Microsoft.AspNetCore.Mvc;

public partial class BasePacketController : ControllerBase
{
    [HttpPost(UserLoginPacketData.ROUTE)]
    public async Task<ActionResult<UserLoginPacketResultData>> PostUserLoginRequest([FromBody]UserLoginPacketData request)
    {
        RequestLoginPacketProcess loginProcess = new RequestLoginPacketProcess();
        await loginProcess.ProcessAsync(gameDataManager, request.UserId, request.Password);
        
        UserLoginPacketResultData resultData = new UserLoginPacketResultData(loginProcess.Result)
        {
            UserData = loginProcess.UserData,
            IsNewUser = loginProcess.IsNewUser
        };

        return resultData;
    }

    [HttpPost(UnitUpgradePacketData.ROUTE)]
    public async Task<ActionResult<UnitUpgradePacketResultData>> PostUserLoginRequest([FromBody]UnitUpgradePacketData request)
    {
        UserBaseData userBaseData = await gameDataManager.RequestGetLoginUserAsync(request.UserId);
        RequestUnitUpgradePacketProcess unitUpgradeProcess = new RequestUnitUpgradePacketProcess();
        await unitUpgradeProcess.ProcessAsync(userBaseData, request.UnitData, request.TargetLevel);
        
        UnitUpgradePacketResultData resultData = new UnitUpgradePacketResultData(unitUpgradeProcess.Result)
        {
            UnitData = unitUpgradeProcess.UnitData,
            TargetLevel = unitUpgradeProcess.TargetLevel
        };
        
        return resultData;
    }

    [HttpPost(UnitDeployPacketData.ROUTE)]
    public async Task<ActionResult<UnitDeployPacketResultData>> PostUserLoginRequest([FromBody]UnitDeployPacketData request)
    {
        UserBaseData userBaseData = await gameDataManager.RequestGetLoginUserAsync(request.UserId);
        RequestUnitDeployPacketProcess unitDeployProcess = new RequestUnitDeployPacketProcess();
        await unitDeployProcess.ProcessAsync(userBaseData, request.DeployUnits);
        
        UnitDeployPacketResultData resultData = new UnitDeployPacketResultData(unitDeployProcess.Result)
        {
            DeployUnits = unitDeployProcess.DeployUnits
        };
        
        return resultData;
    }
}
