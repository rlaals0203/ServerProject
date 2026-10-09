using MongoDB.Driver;

public class DbManager
{
    private Dictionary<EDbType, DbBase> dbList = new();

    public DbManager(IMongoClient mongoClient, ILoggerFactory loggerFactory)
    {
        dbList.Add(EDbType.BaseUserData, new UserBaseDataDbInfo(mongoClient, loggerFactory));
    }

    public async Task<EDbResult> SaveDataAsync<DbType>(DbType data, EDbType dbType, string dbKey) where DbType : class
    {
        if(dbList.TryGetValue(dbType, out var dbData) == false)
        {
            return EDbResult.Fail;
        }

        if(dbData == null)
        {
            return EDbResult.Fail;
        }

        if(dbData is not DbDataBase<DbType> dbInfoData)
        {
            return EDbResult.Fail;
        }

        return await dbInfoData.SaveAsync(data, dbKey);
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

    public async Task<UserBaseData> LoadUserAsync(string userId)
    {
        if(dbList.TryGetValue(EDbType.BaseUserData, out var dbData) == false)
        {
            return null;
        }

        if(dbData == null)
        {
            return null;
        }

        if(dbData is not UserBaseDataDbInfo userDbData)
        {
            return null;
        }

        return await userDbData.LoadAsync(userId);
    }

    public Task<UpdateResult> AddUnitAsync(string userId, string unitId, UnitData unitData)
    {
        UserBaseDataDbInfo userDb = (UserBaseDataDbInfo)dbList[EDbType.BaseUserData];
        return userDb.AddUnitAsync(userId, unitId, unitData);
    }
    public Task<UpdateResult> SetUnitLevelAsync(string userId, string unitId, int unitLevel)
    {
        UserBaseDataDbInfo userDb = (UserBaseDataDbInfo)dbList[EDbType.BaseUserData];
        return userDb.SetUnitLevelAsync(userId, unitId, unitLevel);
    }
}