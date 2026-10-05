using MongoDB.Driver;

public class DbBase(IMongoClient mongoClient, ILoggerFactory loggerFactory)
{
    protected IMongoClient mongoClient = mongoClient;
    protected ILoggerFactory loggerFactory = loggerFactory;
}