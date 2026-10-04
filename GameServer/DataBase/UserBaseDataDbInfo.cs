using MongoDB.Driver;

public class UserBaseDataDbInfo : DbDataInfoBase<UserBaseData>
{
    public UserBaseDataDbInfo(string dbKey, string collectionName, string databaseName, MongoClient mongoClient, ILoggerFactory loggerFactory)
        : base(dbKey, collectionName, databaseName, mongoClient, loggerFactory)
    {
    }
}