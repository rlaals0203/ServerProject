using MongoDB.Driver;

public class GameDataManager
{
    protected readonly IMongoClient MongoClient;
    private readonly ILogger<GameDataManager> logger = null;
    protected readonly ILoggerFactory LoggerFactory;
    protected readonly UserRepository UserRepository;


    public GameDataManager(IMongoClient mongoClient, ILogger<GameDataManager> logger, ILoggerFactory loggerFactory, UserRepository userRepository)
    {
        MongoClient = mongoClient;
        LoggerFactory = loggerFactory;
        UserRepository = userRepository;
        this.logger = logger;
    }

    public async Task<UserBaseData> RequestGetLoginUserAsync(string userId)
    {
        if(string.IsNullOrEmpty(userId))
        {
            logger.LogWarning($"GameDataManager.GetLoginUserBaseInfo. userId is null or empty.");
            return null;
        }

        var foundUser = await UserRepository.GetUserAsync(userId);
        if(foundUser == null)
        {
            logger.LogWarning($"GameDataManager.GetLoginUserBaseInfo. user is null.");
            return null;
        }

        return foundUser;
    }

    public async Task<UserBaseData> RequestCreateUserAsync(string userId, string password)
    {
        return await UserRepository.CreateUserAsync(userId, password);
    }
}
