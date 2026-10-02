namespace GameServer
{
    public class RequestUnitUpgradePacketProcess : BasePacketProcess
    {
        public UnitData UnitData;
        public int TargetLevel;

        public async Task<EPacketResult> ProcessAsync(UserBaseData userData, UnitData unitData, int targetLevel)
        {
            if(unitData == null)
            {
                Result = EPacketResult.NotFoundUnit;
                return Result;
            }

            return Result;
        }
    }
}
