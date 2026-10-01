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