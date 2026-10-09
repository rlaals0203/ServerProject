using MongoDB.Driver;

public class UserBaseDataDbInfo : DbDataBase<UserBaseData>
{
    private const string CollectionName = "Users";

    public UserBaseDataDbInfo(IMongoClient mongoClient, ILoggerFactory loggerFactory) : base(CollectionName, mongoClient, loggerFactory)
    {
    }

    public Task<UpdateResult> AddUnitAsync(string userId, string unitId, UnitData unitData)
    {
        string path = $"UserUnitData.OwnUnitList.{unitId}";
        UpdateDefinition<UserBaseData> update = Builders<UserBaseData>.Update.Set(path, unitData);
        return mongoHelper.UpdateAsync(userId, update);
    }
    
    public Task<UpdateResult> SetUnitLevelAsync(string userId, string unitId, int unitLevel)
    {
        string path = $"UserUnitData.OwnUnitList.{unitId}.UnitLevel";
        UpdateDefinition<UserBaseData> update = Builders<UserBaseData>.Update.Set(path, unitLevel);
        return mongoHelper.UpdateAsync(userId, update);
    }
}