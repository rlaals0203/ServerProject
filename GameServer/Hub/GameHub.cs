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
        var resultData = await _matchingService.ProcessAsync(userId, connectionId);
        if(resultData.MatchResult != EMatchResult.Matched)
        {
            await Clients.Caller.SendAsync(JoinMatchResultData.CONNECTION, resultData);
            return;
        }

        MatchingUserData matchingInfo = new MatchingUserData()
        {
            UserId = userId,
            ConnectionId = connectionId
        };

        if(await _roomService.CreateRoom(matchingInfo, resultData.OpponentUser, resultData.RoomId) == false)
        {
            await Clients.Caller.SendAsync(JoinMatchResultData.CONNECTION, new JoinMatchResultData() { MatchResult = EMatchResult.Fail });
            await Clients.Client(resultData.OpponentUser.ConnectionId).SendAsync(JoinMatchResultData.CONNECTION, new JoinMatchResultData() { MatchResult = EMatchResult.Fail });
            return;
        }

        await Groups.AddToGroupAsync(connectionId, resultData.RoomId);
        await Groups.AddToGroupAsync(resultData.OpponentUser.ConnectionId, resultData.RoomId);

        resultData.IsOpponent = false;
        await Clients.Caller.SendAsync(JoinMatchResultData.CONNECTION, resultData);
        await Clients.Client(resultData.OpponentUser.ConnectionId).SendAsync(JoinMatchResultData.CONNECTION, new JoinMatchResultData()
        {
            MatchResult = EMatchResult.Matched,
            RoomId = resultData.RoomId,
            OpponentUser = matchingInfo,
            IsOpponent = true
        });
    }

    public bool RequestSpawnUnit(string unitId)
    {
        return _roomService.SpawnUnit(Context.ConnectionId, unitId);
    }
}