public class GameRoomService
{
    private Dictionary<string, PvpRoom> _rooms = new();
    private Queue<UserConnectionInfo> _matchUserQueue = new();

    public async Task<EMatchResult> JoinMatch(string userId, string connectionId)
    {
        if(string.IsNullOrEmpty(userId))
        {
            return EMatchResult.Fail;
        }

        if(string.IsNullOrEmpty(connectionId))
        {
            return EMatchResult.Fail;
        }

        UserConnectionInfo connectionInfo = new UserConnectionInfo()
        {
            UserId = userId,
            ConnectionId = connectionId
        };

        if(connectionInfo == null)
        {
            return EMatchResult.Fail;
        }

        if(_matchUserQueue.Contains(connectionInfo))
        {
            return EMatchResult.Fail;
        }

        if(_matchUserQueue.Count == 0)
        {
            _matchUserQueue.Enqueue(connectionInfo);
            return EMatchResult.Waiting;
        }

        UserConnectionInfo opponentInfo = _matchUserQueue.Peek();
        await CreateRoom(connectionInfo, opponentInfo);
        return EMatchResult.Matched;
    }

    public async Task CreateRoom(UserConnectionInfo userInfo, UserConnectionInfo opponentInfo)
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
            //room.Value?.UpdateRoom();
        }
    }
}