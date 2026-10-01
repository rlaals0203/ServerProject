using System.Security.Cryptography.X509Certificates;

namespace GameServer
{
    public class RequestUnitUpgradeProcess : BasePacketProcess
    {
        public UnitData UnitData;
        public int TargetLevel;

        public async Task<EPacketResult> ProcessAsync(UnitData unitData, int targetLevel)
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
