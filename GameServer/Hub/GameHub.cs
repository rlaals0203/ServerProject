using Microsoft.AspNetCore.SignalR;

public class GameHub : Hub
{
    private GameRoomService RoomService;

    public GameHub(GameRoomService roomService)
    {
        RoomService = roomService;
    }

    public async Task RequestJoinMatch(string userId)
    {
        string connectionId = Context.ConnectionId;
        EMatchResult result = await RoomService.JoinMatch(userId, connectionId);
        JoinMatchResultData joinMatchResultData = new JoinMatchResultData()
        {
            MatchResult = result,
            UserId = userId,
        };

        await Clients.Caller.SendAsync("JoinMatchResult", joinMatchResultData);
        return;
    }
}