using GameServer;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route(UserLoginPacketData.ROUTE)]
public class RequestUserLoginController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<UserLoginPacketResultData>> PostUserLoginRequest([FromBody]UserLoginPacketData userLoginPacketData)
    {
        RequestLoginProcess loginProcess = new RequestLoginProcess();
        await loginProcess.ProcessAsync(userLoginPacketData.UserId, userLoginPacketData.Password);
        
        UserLoginPacketResultData resultData = new UserLoginPacketResultData(loginProcess.Result)
        {
            UserId = loginProcess.UserId,
            Password = loginProcess.Password
        };
        
        return resultData;
    }
}

[ApiController]
[Route(UnitUpgradePacketData.ROUTE)]
public class RequestUnitUpgradeController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<UnitUpgradePacketResultData>> PostUserLoginRequest([FromBody]UnitUpgradePacketData userLoginPacketData)
    {
        RequestLoginProcess loginProcess = new RequestLoginProcess();
        await loginProcess.ProcessAsync(userLoginPacketData.UserId, userLoginPacketData.Password);
        
        UnitUpgradePacketResultData resultData = new UnitUpgradePacketResultData(loginProcess.Result)
        {
            UserId = loginProcess.UserId,
            Password = loginProcess.Password
        };
        
        return resultData;
    }
}