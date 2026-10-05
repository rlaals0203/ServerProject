using MongoDB.Driver;

public class DbManager
{
    private Dictionary<EDbType, DbBase> dbList = new();

    public DbManager(MongoClient mongoClient, ILoggerFactory loggerFactory)
    {
        dbList.Add(EDbType.BaseUserData, new UserBaseDataDbInfo(mongoClient, loggerFactory));
    }

    public async Task SaveDataAsync<DbType>(DbType data, EDbType dbType, string dbKey) where DbType : class
    {
        if(dbList.TryGetValue(dbType, out var dbData) == false)
        {
            return;
        }

        if(dbData == null)
        {
            return;
        }

        if(dbData is not DbDataBase<DbType> dbInfoData)
        {
            return;
        }

        await dbInfoData.SaveAsync(data, dbKey);
    }

    public async Task<DbType> LoadDataAsync<DbType>(EDbType dbType, string dbKey) where DbType : class
    {
        if(dbList.TryGetValue(dbType, out var dbData) == false)
        {
            return null;
        }

        if(dbData == null)
        {
            return null;
        }

        if(dbData is not DbDataBase<DbType> dbInfoData)
        {
            return null;
        }

        return await dbInfoData.LoadAsync(dbKey);
    }
}