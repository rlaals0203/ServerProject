using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

public class GameRoomService
{
    private DbManager _dbManager;
    private IHubContext<GameHub> _hubContext;
    private ConcurrentDictionary<string, PvpRoom> _rooms = new();
    private ConcurrentDictionary<string, PvpRoom> _roomsByConnection = new();

    public GameRoomService(DbManager dbManager, IHubContext<GameHub> hubContext)
    {
        _dbManager = dbManager;
        _hubContext = hubContext;
    }

    public async Task<bool> CreateRoom(MatchingUserData userInfo, MatchingUserData opponentInfo, string roomId)
    {
        var userData = await _dbManager.LoadUserAsync(userInfo.UserId);
        var opponentData = await _dbManager.LoadUserAsync(opponentInfo.UserId);
        
        if(userData == null || opponentData == null)
            return false;

        PvpRoom newRoom = new PvpRoom(userData, opponentData)
        {
            UserId = userInfo.UserId,
            UserConnectionId = userInfo.ConnectionId,
            OpponentUserId = opponentInfo.UserId,
            OpponentUserConnectionId = opponentInfo.ConnectionId,
            RoomId = roomId
        };
        
        if(_rooms.TryAdd(roomId, newRoom) == false)
            return false;

        if(_roomsByConnection.TryAdd(userInfo.ConnectionId, newRoom) == false)
        {
            _rooms.TryRemove(roomId, out _);
            return false;
        }

        if(_roomsByConnection.TryAdd(opponentInfo.ConnectionId, newRoom) == false)
        {
            _roomsByConnection.TryRemove(userInfo.ConnectionId, out _);
            _rooms.TryRemove(roomId, out _);
            return false;
        }

        return true;
    }

    public bool SpawnUnit(string connectionId, string unitId)
    {
        if(_roomsByConnection.TryGetValue(connectionId, out var room) == false)
        {
            return false;
        }

        return room.SpawnUnit(connectionId, unitId);
    }

    public async Task UpdateRoom(float dt)
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

        if(_rooms.TryRemove(roomId, out var room))
        {
            _roomsByConnection.TryRemove(room.UserConnectionId, out _);
            _roomsByConnection.TryRemove(room.OpponentUserConnectionId, out _);
            room.DestroyRoom();
        }
    }

    private async Task SendMessageToClient()
    {
        foreach(var room in _rooms.Values)
        {
            while(room.TryTakeMessage(out var message))
            {
                await _hubContext.Clients.Group(message.RoomId).SendAsync(message.Route, message);
            }
        }
    }
}