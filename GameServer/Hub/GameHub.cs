using Microsoft.AspNetCore.SignalR;

public class GameHub : Hub
{
    private GameRoomService _roomService;
    private MatchingService _matchingService;

    public GameHub(MatchingService matchingService, GameRoomService roomService)
    {
        _roomService = roomService;
        _matchingService = matchingService;
    }

    public async Task RequestJoinMatch(string userId)
    {
        string connectionId = Context.ConnectionId;
        PvpMatchingProcess process = new PvpMatchingProcess();
        var resultData = await _matchingService.ProcessAsync(userId, connectionId);

        await Clients.Caller.SendAsync(JoinMatchResultData.CONNECTION, resultData);

        if(resultData.MatchResult == EMatchResult.Matched)
        {
            await Clients.Client(process.WaitingUser.ConnectionId).SendAsync(JoinMatchResultData.CONNECTION, resultData);
        } 
    }
}