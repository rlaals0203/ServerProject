using Microsoft.AspNetCore.SignalR;

public class GameHub : Hub
{
    private GameRoomService RoomService;

    public GameHub(GameRoomService roomService)
    {
        RoomService = roomService;
    }

    public async Task JoinMatch(string userId)
    {
        string connectionId = Context.ConnectionId;
        EMatchResult result = await RoomService.JoinMatch(userId, connectionId);
        MatchResultData matchResultData = new MatchResultData()
        {
            MatchResult = result,
            UserId = userId,
        };

        if(result == EMatchResult.Fail)
        {
            await Clients.Caller.SendAsync("MatchFail", matchResultData);
            return;
        }
        if(result == EMatchResult.Waiting)
        {
            await Clients.Caller.SendAsync("MatchWaiting", matchResultData);
            return;
        }
        if(result == EMatchResult.Matched)
        {
            await Clients.Caller.SendAsync("MatchSucess", matchResultData);
            return;
        }
    }
}