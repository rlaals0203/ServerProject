using MongoDB.Driver;

public class GameDataManager
{
    protected readonly UserData UserData;
    protected readonly IMongoClient MongoClient;
    private readonly ILogger<GameDataManager> logger = null;
    protected readonly ILoggerFactory LoggerFactory;

    public GameDataManager(UserData userData, IMongoClient mongoClient, ILoggerFactory loggerFactory)
    {
        UserData = userData;
        MongoClient = mongoClient;
        LoggerFactory = loggerFactory;
    }
}