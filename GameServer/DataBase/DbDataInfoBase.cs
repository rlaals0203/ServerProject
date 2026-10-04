using MongoDB.Driver;

public class DbDataInfoBase<DbType>(MongoClient mongoClient, ILoggerFactory loggerFactory)
    :  DbDataBase(mongoClient, loggerFactory) where DbType : class
{
    public EPacketResult Result = EPacketResult.Ok;
    protected IMongoClient mongoClient = null;
    protected ILoggerFactory loggerFactory = null;
    protected MongoHelper<DbType> mongoHelper = null;
    protected string dbKey;
    protected string collectionName;
    protected string databaseName;

    public void Initialize(string dbKey, string collectionName, string databaseName)
    {
        this.dbKey = dbKey;
        this.collectionName = collectionName;
        this.databaseName = databaseName;

        mongoHelper = new MongoHelper<DbType>(mongoClient, databaseName, loggerFactory);
        mongoHelper.InitCollection(databaseName);
    }

    public async Task SaveAsync(DbType data)
    {
        await mongoHelper.SaveAsync(data, dbKey);
    }

    public async Task<DbType> LoadAsync()
    {
        return await mongoHelper.LoadAsync(dbKey);
    }
}