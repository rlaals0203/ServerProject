namespace GameServer
{
    public class RequestUnitUnlockPacketProcess : BasePacketProcess
    {
        public string UnitId;

        public async Task<EPacketResult> ProcessAsync(UserBaseData userData, UnitDataDefine unitDataDefine, string unitId)
        {
            if(string.IsNullOrEmpty(unitId))
            {
                Result = EPacketResult.InvalidRequest;
                return Result;
            }

            if(userData.UserUnitData.OwnUnitList.TryGetValue(unitId, out var _))
            {
                Result = EPacketResult.InvalidRequest;
                return Result;
            }

            if(unitDataDefine.UnitData.TryGetValue(unitId, out var unitStatData) == false)
            {
                Result = EPacketResult.InvalidRequest;
                return Result;
            }

            UnitData unitData = new UpdateUnitData(unitStatData, 1).UnitData;
            userData.UserUnitData.OwnUnitList.Add(unitId, unitData);

            return Result;
        }
    }
}
