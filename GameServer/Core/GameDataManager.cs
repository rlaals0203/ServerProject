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

    // public async Task<UserBaseData> RequestGetLoginUserAsync(string userId)
    // {
    //     if(string.IsNullOrEmpty(userId))
    //     {
    //         logger.LogWarning($"GameDataManager.GetLoginUserBaseInfo. userId is null or empty.");
    //         return null;
    //     }

    //     var foundUser = await UserRepository.GetUserAsync(userId);
    //     if(foundUser == null)
    //     {
    //         logger.LogWarning($"GameDataManager.GetLoginUserBaseInfo. user is null.");
    //         return null;
    //     }

    //     return foundUser;
    // }

    // public async Task<UserBaseData> RequestCreateUserAsync(string userId, string password)
    // {
    //     UserBaseData user = await UserRepository.CreateUserAsync(userId, password);
    //     RequestUsrDataCheck(user);
    //     return user;
    // }

    // public async void RequestUsrDataCheck(UserBaseData userData)
    // {
    //     await UserRepository.UserDataCheck(userData);
    // }
}
