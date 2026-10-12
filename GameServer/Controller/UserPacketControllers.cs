using GameServer;
using Microsoft.AspNetCore.Mvc;

public partial class BasePacketController : ControllerBase
{
    [HttpPost(UserLoginPacketData.ROUTE)]
    public async Task<ActionResult<UserLoginPacketResultData>> PostUserLoginRequest([FromBody]UserLoginPacketData request)
    {
        RequestLoginPacketProcess loginProcess = new RequestLoginPacketProcess();
        await loginProcess.ProcessAsync(dbManager, unitDataDefine, request.UserId, request.Password);
        
        UserLoginPacketResultData resultData = new UserLoginPacketResultData(loginProcess.Result)
        {
            UserData = loginProcess.UserData,
            IsNewUser = loginProcess.IsNewUser
        };

        return resultData;
    }

    [HttpPost(UnitUnlockPacketData.ROUTE)]
    public async Task<ActionResult<UnitUnlockPacketResultData>> PostUnitDeployRequest([FromBody]UnitUnlockPacketData request)
    {
        UserBaseData userBaseData = await dbManager.LoadUserAsync(request.LoginUserId);
        RequestUnitUnlockPacketProcess process = new RequestUnitUnlockPacketProcess();
        await process.ProcessAsync(userBaseData, unitDataDefine, request.UnitId);
        
        UnitUnlockPacketResultData resultData = new UnitUnlockPacketResultData(process.Result)
        {
            UnitId = process.UnitId
        };

        await dbManager.SaveDataAsync(userBaseData, EDbType.BaseUserData, request.LoginUserId);
        return resultData;
    }

    [HttpPost(UnitUpgradePacketData.ROUTE)]
    public async Task<ActionResult<UnitUpgradePacketResultData>> PostUnitUpgradeRequest([FromBody]UnitUpgradePacketData request)
    {
        UserBaseData userBaseData = await dbManager.LoadUserAsync(request.LoginUserId);
        RequestUnitUpgradePacketProcess process = new RequestUnitUpgradePacketProcess();
        await process.ProcessAsync(userBaseData, dbManager, request.UnitId, request.TargetLevel);
        
        UnitUpgradePacketResultData resultData = new UnitUpgradePacketResultData(process.Result)
        {
            UnitId = process.UnitId,
            UnitLevel = process.UnitLevel,
            UpgradePrice = process.UpgradePrice
        };
        
        return resultData;
    }

    [HttpPost(UnitDeployPacketData.ROUTE)]
    public async Task<ActionResult<UnitDeployPacketResultData>> PostUnitDeployRequest([FromBody]UnitDeployPacketData request)
    {
        UserBaseData userBaseData = await dbManager.LoadUserAsync(request.LoginUserId);
        RequestUnitDeployPacketProcess unitDeployProcess = new RequestUnitDeployPacketProcess();
        await unitDeployProcess.ProcessAsync(userBaseData, dbManager, request.DeployUnits);
        
        UnitDeployPacketResultData resultData = new UnitDeployPacketResultData(unitDeployProcess.Result)
        {
            DeployUnits = unitDeployProcess.DeployUnits
        };
        
        return resultData;
    }

    [HttpPost(UserMonetaChangePacketData.ROUTE)]
    public async Task<ActionResult<UserMonetaChangePacketResultData>> PostUserMonetaChangeRequest([FromBody]UserMonetaChangePacketData request)
    {
        UserBaseData userBaseData = await dbManager.LoadUserAsync(request.LoginUserId);
        RequestUserMonetaChangeProcess process = new RequestUserMonetaChangeProcess();
        await process.ProcessAsync(userBaseData, dbManager, request.MonetaType, request.Amount);

        UserMonetaChangePacketResultData resultData = new UserMonetaChangePacketResultData(process.Result)
        {
            MonetaType = process.MonetaType,
            Amount = process.Amount
        };

        return resultData;
    }

    [HttpPost(PassiveUpgradePacketData.ROUTE)]
    public async Task<ActionResult<PassiveUpgrdePacketResultData>> PostPassiveUpgrdeRequest([FromBody]PassiveUpgradePacketData request)
    {
        UserBaseData userBaseData = await dbManager.LoadUserAsync(request.LoginUserId);
        RequestPassiveUpgradeProcess process = new RequestPassiveUpgradeProcess();
        await process.ProcessAsync(userBaseData, dbManager, request.PassiveType);

        PassiveUpgrdePacketResultData resultData = new PassiveUpgrdePacketResultData(process.Result)
        {
            PassiveType = process.PassiveType,
            Level = process.Level,
        };

        return resultData;
    }
}
