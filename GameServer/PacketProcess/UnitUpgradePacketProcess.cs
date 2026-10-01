namespace GameServer
{
    public class RequestUnitUpgradeProcess : BasePacketProcess
    {
        public UnitData UnitData;
        public int TargetLevel;

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

            
            return Result;
        }
    }
}
