public class GameRoomService
{
    private DbManager _dbManager; 
    private Dictionary<string, PvpRoom> _rooms = new();
    private const float DT = 0.16666f;

    public GameRoomService(DbManager dbManager)
    {
        _dbManager = dbManager;
    }

    public async Task CreateRoom(MatchingUserData userInfo, MatchingUserData opponentInfo)
    {
        PvpRoom newRoom = new PvpRoom
        {
            UserId = userInfo.UserId,
            UserConnectionId = userInfo.ConnectionId,
            OpponentUserId = opponentInfo.UserId,
            OpponentUserConnectionId = opponentInfo.ConnectionId
        };

        _rooms.Add(opponentInfo.UserId, newRoom);
    }

    public async Task DestroyRoom(string roomId)
    {
        if(_rooms.Count == 0)
        {
            return;
        }

        if(_rooms.TryGetValue(roomId, out var room))
        {
            room.DestroyRoom();
            _rooms.Remove(roomId);
        }
    }

    public async Task UpdateRoom()
    {
        foreach(var room in _rooms)
        {
            room.Value.UpdateRoom(DT);
        }
    }
}