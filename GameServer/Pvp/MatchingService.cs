public class MatchingService
{
    private MatchingUserData _waitingUser;
    private readonly SemaphoreSlim _matchLock = new(1, 1);

    public async Task<JoinMatchResultData> ProcessAsync(string userId, string connectionId)
    {
        await _matchLock.WaitAsync();

        try
        {
            if(string.IsNullOrEmpty(userId))
            {
                return MakeJoinMatchResultData(EMatchResult.Fail);
            }

            if (_waitingUser == null)
            {
                _waitingUser = new MatchingUserData()
                {
                    UserId = userId,
                    ConnectionId = connectionId
                };

                return MakeJoinMatchResultData(EMatchResult.Waiting);
            }

            if (_waitingUser.UserId == userId)
            {
                return MakeJoinMatchResultData(EMatchResult.Fail);
            }

            var opponentUser = _waitingUser;

            if (opponentUser == null)
            {
                _waitingUser = null;
                return MakeJoinMatchResultData(EMatchResult.Fail);
            }

            _waitingUser = null;
            return MakeJoinMatchResultData(EMatchResult.Matched, opponentUser);
        }
        finally
        {
            _matchLock.Release();
        }
    }

    public void CancelMatch(string connectionId)
    {  
        if (_waitingUser != null && _waitingUser.ConnectionId == connectionId)    
        {                
            _waitingUser = null;
        }
    }

    private JoinMatchResultData MakeJoinMatchResultData(EMatchResult matchResult, MatchingUserData waitingUser = null)
    {
        var result = new JoinMatchResultData()
        {
            MatchResult = matchResult,
            OpponentUser = waitingUser
        };

        if(waitingUser != null)
        {
            result.RoomId = Guid.NewGuid().ToString();
        }

        return result;
    }
}