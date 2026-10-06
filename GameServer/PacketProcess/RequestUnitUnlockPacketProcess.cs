namespace GameServer
{
    public class RequestUnitUnlockPacketProcess : BasePacketProcess
    {
        public string UnitId;

        public async Task<EPacketResult> ProcessAsync(UserBaseData userData, string unitId)
        {
            if(string.IsNullOrEmpty(unitId))
            {
                Result = EPacketResult.InvalidRequest;
                return Result;
            }

            if(userData.UserUnitData.OwnUnitList.TryGetValue(unitId, out var unit))
            {
                Result = EPacketResult.InvalidRequest;
                return Result;
            }

            return Result;
        }
    }
}
