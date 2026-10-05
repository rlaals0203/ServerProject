using MongoDB.Driver;

public class UserBaseDataDbInfo : DbDataBase<UserBaseData>
{
    private const string CollectionName = "Users";

    public UserBaseDataDbInfo(MongoClient mongoClient, ILoggerFactory loggerFactory) : base(CollectionName, mongoClient, loggerFactory)
    {
    }
}