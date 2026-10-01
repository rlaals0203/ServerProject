using MongoDB.Driver;

public class UserBaseInfo : DbInfoBase<UserBaseData>
{
    public UserBaseInfo(IMongoClient mongoClient, ILoggerFactory loggerFactory) : base(mongoClient, loggerFactory)
    {
        
    }
}