public class PvpRankingService
{
    private PvpRankingData _cachedRanking;
    private DbManager _dbManager;

    public PvpRankingService(DbManager dbManager)
    {
        _dbManager = dbManager;
    }

    private async Task<PvpRankingData> GetRankingDataFromDb()
    {
        return await _dbManager.LoadDataAsync<PvpRankingData>(EDbType.PvpRankingData, "PvpRanking") ?? new PvpRankingData() { Ranks = new()};
    }

    public async Task<UserPvpRankData> UpdateRanking(string userId, int point)
    {
        _cachedRanking ??= await GetRankingDataFromDb();

        if(_cachedRanking.Ranks.TryGetValue(userId, out var rankData))
        {
            rankData.Point += point;
        }
        else
        {
            rankData = new UserPvpRankData()
            {
                UserId = userId,
                Point = point
            };

            _cachedRanking.Ranks.Add(userId, rankData);
        }

        await _dbManager.SaveDataAsync(EDbType.PvpRankingData, _cachedRanking, "PvpRanking");
        return rankData;
    }

    public async Task<PvpRankingData> GetRankingData()
    {
        _cachedRanking ??= await GetRankingDataFromDb();
        return _cachedRanking;
    }
}