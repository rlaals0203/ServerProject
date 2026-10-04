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

    public async Task<UserBaseData> CreateUserAsync(string userId, string password)
    {
        UserBaseData userBaseData = new UserBaseData
        {
            UserId = userId,
            Password = password,
            UserUnitData = new UserUnitData(),
            UserMonetaData = new UserMonetaData()
        };

        await _users.InsertOneAsync(userBaseData);
        return userBaseData;
    }

    public async Task UpdateUserAsync(string userId)
    {
        UserBaseData foundUser = await GetUserAsync(userId);

        if(foundUser == null)
        {
            return;
        }

        await _users.ReplaceOneAsync(user => user.UserId == userId, foundUser);
    }

    public async Task UserDataCheck(UserBaseData userData)
    {
        new UserDataCheck(userData);
        await _users.ReplaceOneAsync(user => user.UserId == userData.UserId, userData);
    }
}
