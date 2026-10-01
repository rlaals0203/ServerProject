using System.Collections.Concurrent;
using MongoDB.Driver;

public class GameDataManager
{
    protected readonly IMongoClient MongoClient;
    private readonly ILogger<GameDataManager> logger = null;
    protected readonly ILoggerFactory LoggerFactory;
    protected readonly ConcurrentDictionary<string, UserBaseData> LoginUserList;


    public GameDataManager(IMongoClient mongoClient, ILogger<GameDataManager> logger, ILoggerFactory loggerFactory)
    {
        MongoClient = mongoClient;
        LoggerFactory = loggerFactory;
        this.logger = logger;
    }

    public async Task<UserBaseData> RequestLoginUser(string userId, string password)
    {
        if(string.IsNullOrEmpty(userId))
        {
            logger.LogWarning($"GameDataManager.GetLoginUserBaseInfo. userId is null or empty.");
            return null;
        }

        IMongoDatabase mongoDatabase = MongoClient.GetDatabase("User");
        IMongoCollection<UserBaseData> userDataCollection = mongoDatabase.GetCollection<UserBaseData>("Users");

        var filter = Builders<UserBaseData>.Filter.Eq("UserId", userId) & Builders<UserBaseData>.Filter.Eq("Password", password);
        UserBaseData result = userDataCollection.Find(filter).FirstOrDefault();
        return result;
    }
}