using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

public class GameRoomService
{
    private DbManager _dbManager;
    private IHubContext<GameHub> _hubContext;
    private PvpRankingService _rankingService;
    private ConcurrentDictionary<string, PvpRoom> _rooms = new();
    private ConcurrentDictionary<string, PvpRoom> _roomsByConnection = new();

    public GameRoomService(DbManager dbManager, IHubContext<GameHub> hubContext, PvpRankingService rankingService)
    {
        _dbManager = dbManager;
        _hubContext = hubContext;
        _rankingService = rankingService;
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

    public async Task SendCastleData(string connectionId)
    {
        if(_roomsByConnection.TryGetValue(connectionId, out var room) == false)
            return;

        await _hubContext.Clients.Client(connectionId).SendAsync(UpdateCastleDataMessage.ROUTE, room.GetCastleData());
    }

    public async Task UpdateRoom(float dt)
    {
        foreach(var room in _rooms)
        {
            room.Value.UpdateRoom(dt);
        }

        SendMessageToClient();
    }

    public async void SendMessageToClient()
    {
        await SendMessageToClientAsync();
    }

    public void DestroyRoom(string roomId)
    {
        if(_rooms.Count == 0)
            return;

        if(_rooms.TryRemove(roomId, out var room))
        {
            room.DestroyRoom();
        }
    }

    public async Task LeaveRoom(string connectionId)
    {
        if(_roomsByConnection.TryRemove(connectionId, out var room) == false)
            return;

        await _hubContext.Groups.RemoveFromGroupAsync(connectionId, room.RoomId);

        if(_roomsByConnection.TryGetValue(connectionId == room.UserConnectionId ? room.OpponentUserConnectionId : room.UserConnectionId, out var otherRoom) && otherRoom == room)
            return;

        DestroyRoom(room.RoomId);
    }

    private async Task SendMessageToClientAsync()
    {
        foreach(var room in _rooms.Values)
        {
            if(room.IsComplete && room.IsRankProcessed == false)
            {
                room.IsRankProcessed = true;
                UpdateRanking(room.IsOpponentWinner ? room.OpponentUserId : room.UserId, room.Point);
            }

            while(room.TryGetMessage(out var message))
            {
                await _hubContext.Clients.Group(message.RoomId).SendAsync(message.Route, message);
            }
        }
    }

    private async void UpdateRanking(string userId, int point)
    {
        await _rankingService.UpdateRanking(userId, point);
    }
}