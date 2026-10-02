using MongoDB.Driver;

public class DbInfoBase<DbType>(IMongoClient mongoClient, ILoggerFactory loggerFactory) where DbType : class
{
    public EPacketResult Result = EPacketResult.Ok;
    protected IMongoClient mongoClient = mongoClient;
    protected ILoggerFactory loggerFactory = loggerFactory;
    protected string dbKey = "";
}