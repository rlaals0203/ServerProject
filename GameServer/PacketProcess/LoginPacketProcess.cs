namespace GameServer
{
    public class RequestLoginProcess : BasePacketProcess
    {
        public string UserId;
        public string Password;

        public async Task<EPacketResult> ProcessAsync(string userId, string password)
        {
            if(string.IsNullOrEmpty(userId))
            {
                Result = EPacketResult.Fail;
                return Result;
            }

            if(string.IsNullOrEmpty(password))
            {
                Result = EPacketResult.Fail;
                return Result;
            }

            UserId = userId;
            Password = password;
            return Result;
        }
    }
}
