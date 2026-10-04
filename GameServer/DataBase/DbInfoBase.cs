using MongoDB.Driver;

public class DbDataBase(MongoClient mongoClient, ILoggerFactory loggerFactory)
{
    public EPacketResult Result = EPacketResult.Ok;
    protected IMongoClient mongoClient = mongoClient;
    protected ILoggerFactory loggerFactory = loggerFactory;
}