namespace GameServer
{
    public class RequestLoginPacketProcess : BasePacketProcess
    {
        public UserBaseData UserData;
        public bool IsNewUser;

        public async Task<EPacketResult> ProcessAsync(DbManager dbManager, UnitDataDefine unitData, string userId, string password)
        {
            if(string.IsNullOrEmpty(userId))
            {
                Result = EPacketResult.LoginFailed;
                return Result;
            }

            if(string.IsNullOrEmpty(password))
            {
                Result = EPacketResult.LoginFailed;
                return Result;
            }

            UserData = await dbManager.LoadDataAsync<UserBaseData>(EDbType.BaseUserData, userId);

            if(UserData == null)
            {
                UserData = new UserBaseData
                {
                    UserId = userId,
                    Password = password
                };

                IsNewUser = true;
                new UserDataCheck(UserData, unitData);
                await dbManager.SaveDataAsync(EDbType.BaseUserData, UserData, userId);
                return Result;
            }

            if(UserData.Password != password)
            {
                Result = EPacketResult.InvalidPassword;
                return Result;
            }
                
            IsNewUser = false;
            new UserDataCheck(UserData, unitData);
            await dbManager.SaveDataAsync(EDbType.BaseUserData, UserData, userId);
            return Result;
        }
    }
}
