public class PvpMatchingProcess
{
    public MatchingUserData WaitingUser;
    public string RoomId;

    private MatchingUserData _waitingUser;
    private readonly SemaphoreSlim _matchLock = new(1, 1);

    public async Task<EMatchResult> ProcessAsync(string userId, string connectionId)
    {
        await _matchLock.WaitAsync();

        try
        {
            if(string.IsNullOrEmpty(userId))
            {
                return EMatchResult.Fail;
            }

            if (_waitingUser == null)
            {
                return EMatchResult.Waiting;
            }

            _waitingUser = new MatchingUserData()
            {
                UserId = userId,
                ConnectionId = connectionId
            };

            if (_waitingUser.UserId == userId)
            {
                return EMatchResult.Fail;
            }

            var opponentUser = _waitingUser;
            _waitingUser = null;

            if (opponentUser == null)
            {
                return EMatchResult.Fail;
            }

            WaitingUser = _waitingUser;
            RoomId = Guid.NewGuid().ToString();
            return EMatchResult.Matched;
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
}