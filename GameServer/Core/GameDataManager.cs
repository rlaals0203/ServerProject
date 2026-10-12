using MongoDB.Driver;

public class GameDataManager
{
    protected readonly IMongoClient MongoClient;
    private readonly ILogger<GameDataManager> logger = null;
    protected readonly ILoggerFactory LoggerFactory;

    public GameDataManager(IMongoClient mongoClient, ILogger<GameDataManager> logger, ILoggerFactory loggerFactory)
    {
        MongoClient = mongoClient;
        LoggerFactory = loggerFactory;
        this.logger = logger;
    }
}
