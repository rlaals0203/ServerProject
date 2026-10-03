namespace GameServer
{
    public class RequestLoginPacketProcess : BasePacketProcess
    {
        public UserBaseData UserData;
        public bool IsNewUser;

        public async Task<EPacketResult> ProcessAsync(GameDataManager gameDataManager, string userId, string password)
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

            UserBaseData foundUser = await gameDataManager.RequestGetLoginUserAsync(userId);

            if(foundUser != null)
            {
                if(foundUser.Password != password)
                {
                    Result = EPacketResult.InvalidPassword;
                    return Result;
                }
                
                UserData = foundUser;
                IsNewUser = false;
                Result = EPacketResult.Ok;
                return Result;
            }

            UserData = await gameDataManager.RequestCreateUserAsync(userId, password);
            IsNewUser = true;
            Result = EPacketResult.Ok;
            return Result;
        }
    }
}
