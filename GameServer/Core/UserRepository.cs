using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class UserRepository
{
    public IMongoCollection<UserBaseData> _users;

    public UserRepository(IOptions<MongoDbSettings> settings)
    {
        var mongoClient = new MongoClient(settings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
         _users = mongoDatabase.GetCollection<UserBaseData>(settings.Value.UserCollectionName);
    }

    public async Task<UserBaseData> GetUserAsync(string userId)
    {
        if(string.IsNullOrEmpty(userId))
        {
            return null;
        }

        return await _users.Find(user => user.UserId == userId).FirstOrDefaultAsync();
    }

    public async Task<UserBaseData> CreateUserAsync(string userId)
    {
        UserBaseData userBaseData = new UserBaseData
        {
            UserId = userId,
            UserUnitData = new UserUnitData()
        };

        await _users.InsertOneAsync(userBaseData);
        return userBaseData;
    }
    
    public async Task CreateUserAsync(UserBaseData userBaseData)
    {
        await _users.InsertOneAsync(userBaseData);
    }

    public async Task UpdateUserAsync(UserBaseData userBaseData)
    {
        var foundUser = await GetUserAsync(userBaseData.UserId);

        if(foundUser == null)
        {
            await CreateUserAsync(userBaseData);
            return;
        }

        await _users.ReplaceOneAsync(user => user.UserId == userBaseData.UserId, userBaseData);
    }
}