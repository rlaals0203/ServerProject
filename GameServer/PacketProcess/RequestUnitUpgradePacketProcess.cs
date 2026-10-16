namespace GameServer
{
    public class RequestUnitUpgradePacketProcess : BasePacketProcess
    {
        public string UnitId;
        public int UnitLevel;
        public int UpgradePrice;

        public async Task<EPacketResult> ProcessAsync(UserBaseData userData, DbManager dbManager, string unitId, int targetLevel)
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

            int upgradePrice = new GetUnitUpgradePrice(unit.UnitStatData.UpgradePrice, unit.UnitLevel).Price;

            if(userData.UserMonetaData[EMonetaType.Exp.ToString()] < upgradePrice)
            {
                Result = EPacketResult.InvalidRequest;
                return Result;
            }

            unit.UnitLevel += availableLevel;
            UnitId = unitId;
            UnitLevel = unit.UnitLevel;
            UpgradePrice = upgradePrice;
            
            userData.UserMonetaData[EMonetaType.Exp.ToString()] -= upgradePrice;
            userData.UserUnitData.OwnUnitList[unitId] = new UpdateUnitData(unit.UnitStatData, UnitLevel).UnitData;
            await dbManager.SaveDataAsync(EDbType.BaseUserData, userData, userData.UserId);
            return Result;
        }
    }
}
