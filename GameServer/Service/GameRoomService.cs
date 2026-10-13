using Microsoft.AspNetCore.SignalR;

public class GameRoomService
{
    private DbManager _dbManager;
    private IHubContext<GameHub> _hubContext;
    private Dictionary<string, PvpRoom> _rooms = new();

    public GameRoomService(DbManager dbManager, IHubContext<GameHub> hubContext)
    {
        _dbManager = dbManager;
        _hubContext = hubContext;
    }

    public async Task CreateRoom(MatchingUserData userInfo, MatchingUserData opponentInfo, string roomId)
    {
        var userData = await _dbManager.LoadUserAsync(userInfo.UserId);
        var opponentData = await _dbManager.LoadUserAsync(opponentInfo.UserId);

        PvpRoom newRoom = new PvpRoom(userData, opponentData)
        {
            UserId = userInfo.UserId,
            UserConnectionId = userInfo.ConnectionId,
            OpponentUserId = opponentInfo.UserId,
            OpponentUserConnectionId = opponentInfo.ConnectionId,
            RoomId = roomId
        };
        
        _rooms.Add(opponentInfo.UserId, newRoom);
    }

    public async void UpdateRoom(float dt)
    {
        foreach(var room in _rooms)
        {
            room.Value.UpdateRoom(dt);
        }

        await SendMessageToClient();
    }

    public async Task DestroyRoom(string roomId)
    {
        if(_rooms.Count == 0)
            return;

        if(_rooms.TryGetValue(roomId, out var room))
        {
            room.DestroyRoom();
            _rooms.Remove(roomId);
        }
    }

    private async Task SendMessageToClient()
    {
        foreach(var room in _rooms.Values)
        {
            foreach(var message in room.TakeMessageQueue())
            {
                await _hubContext.Clients.Group(message.RoomId).SendAsync(message.Route, message);
            }
        }
    }
}