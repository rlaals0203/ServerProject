using MongoDB.Driver;

public abstract class DbDataBase<DbType> : DbBase where DbType : class
{
    protected MongoHelper<DbType> mongoHelper = null;

    public DbDataBase(string collectionName, IMongoClient mongoClient, ILoggerFactory loggerFactory) : base(mongoClient, loggerFactory)
    {
        mongoHelper = new MongoHelper<DbType>(mongoClient, GameDefine.DatabaseName, loggerFactory);
        mongoHelper.InitCollection(collectionName);
    }

    public async Task<EDbResult> SaveAsync(DbType data, string dbKey)
    {
        return await mongoHelper.SaveAsync(data, dbKey);
    }

    public async Task<DbType> LoadAsync(string dbKey)
    {
        return await mongoHelper.LoadAsync(dbKey);
    }
}