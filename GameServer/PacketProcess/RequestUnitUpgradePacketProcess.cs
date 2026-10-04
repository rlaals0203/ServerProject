namespace GameServer
{
    public class RequestUnitUpgradePacketProcess : BasePacketProcess
    {
        public string UnitId;
        public int UnitLevel;

        public async Task<EPacketResult> ProcessAsync(UserBaseData userData, string unitId, int targetLevel)
        {
            int availableLevel = targetLevel;

            if(string.IsNullOrEmpty(unitId))
            {
                Result = EPacketResult.InvalidRequest;
                return Result;
            }

            if(userData.UserUnitData.OwnUnitList.TryGetValue(unitId, out var unit) == false)
            {
                Result = EPacketResult.InvalidRequest;
                return Result;
            }

            if(unit == null)
            {
                Result = EPacketResult.NotFoundUnit;
                return Result;
            }

            if(unit.UnitLevel >= GameDefine.UnitMaxLevel)
            {
                Result = EPacketResult.MaxUnitLevel;
                return Result;
            }

            if(unit.UnitLevel + targetLevel > GameDefine.UnitMaxLevel)
            {
               availableLevel = GameDefine.UnitMaxLevel - unit.UnitLevel;
            }

            unit.UnitLevel += availableLevel;
            UnitId = unitId;
            UnitLevel = unit.UnitLevel;

            return Result;
        }
    }
}
