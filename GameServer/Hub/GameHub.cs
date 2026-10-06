using Microsoft.AspNetCore.SignalR;

public class GameHub : Hub
{
    private GameRoomService RoomService;

    public GameHub(GameRoomService roomService)
    {
        RoomService = roomService;
    }

    public async Task JoinMatchAsync(string userId)
    {
        string connectionId = Context.ConnectionId;
        EMatchResult result = await RoomService.JoinMatch(userId, connectionId);

        if(result == EMatchResult.Fail)
        {
            
        }
        if(result == EMatchResult.Waiting)
        {
            
        }
        if(result == EMatchResult.Matched)
        {
            
        }
    }
}