public class PvpRankingService
{
    private PvpRankingData _cachedRanking;
    private DbManager _dbManager;

    public PvpRankingService(DbManager dbManager)
    {
        _dbManager = dbManager;
    }

    public async Task<UserPvpRankData> UpdateRanking(string userId, int point)
    {
        if(_cachedRanking.Ranks.TryGetValue(userId, out var rankData))
        {
            rankData.Point += point;
            return rankData;
        }

        rankData = new UserPvpRankData()
        {
            UserId = userId,
            Point = point
        };

        _cachedRanking.Ranks.Add(userId, rankData);
        return rankData;
    }

    public async Task<PvpRankingData> GetRankingData()
    {
        return _cachedRanking;
    }
}