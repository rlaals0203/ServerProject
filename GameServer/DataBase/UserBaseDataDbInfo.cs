using MongoDB.Driver;

public class UserBaseDataDbInfo : DbDataBase<UserBaseData>
{
    private const string CollectionName = "Users";

    public UserBaseDataDbInfo(IMongoClient mongoClient, ILoggerFactory loggerFactory) : base(CollectionName, mongoClient, loggerFactory)
    {
    }
}