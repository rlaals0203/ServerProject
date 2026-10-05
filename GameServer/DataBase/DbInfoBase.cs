using MongoDB.Driver;

public class DbBase(MongoClient mongoClient, ILoggerFactory loggerFactory)
{
    protected IMongoClient mongoClient = mongoClient;
    protected ILoggerFactory loggerFactory = loggerFactory;
}