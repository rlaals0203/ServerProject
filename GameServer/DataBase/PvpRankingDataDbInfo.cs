using MongoDB.Driver;

public class PvpRankingDataDbInfo : DbDataBase<PvpRankingData>
{
    private const string CollectionName = "PvpRanking";

    public PvpRankingDataDbInfo(IMongoClient mongoClient, ILoggerFactory loggerFactory) : base(CollectionName, mongoClient, loggerFactory)
    {
    }
}