namespace GameServer
{
    public class RequestUnitDeployPacketProcess : BasePacketProcess
    {
        public Dictionary<string, string> DeployUnits = new();

        public async Task<EPacketResult> ProcessAsync(UserBaseData userData, DbManager dbManager, Dictionary<int, string> deployUnits)
        {
            if(userData == null)
            {
                Result = EPacketResult.InvalidRequest;
                return Result;
            }

            if(userData.UserUnitData == null)
            {
                Result = EPacketResult.InvalidRequest;
                return Result;
            }

            if(deployUnits == null)
            {
                Result = EPacketResult.InvalidRequest;
                return Result;
            }

            if(deployUnits.Count > GameDefine.MaxSlotCount)
            {
                Result = EPacketResult.InvalidRequest;
                return Result;
            }

            UserUnitData userUnitData = userData.UserUnitData;

            for (int i = 0; i < deployUnits.Count; i++)
            {
                if (deployUnits.TryGetValue(i, out var unitId) == false)
                {
                    continue;
                }
                
                if(string.IsNullOrEmpty(unitId))
                {
                    continue;
                }

                if(userUnitData.OwnUnitList.ContainsKey(unitId) == false)
                {
                    continue;
                }
                
                if(DeployUnits.ContainsValue(unitId))
                {
                    continue;
                }

                DeployUnits[i.ToString()] = unitId;
            }

            userUnitData.DeployUnitList.Clear();

            foreach (var deployed in DeployUnits)
            {
                userUnitData.DeployUnitList[deployed.Key] = userUnitData.OwnUnitList[deployed.Value];
            }
            
            var dbResult = await dbManager.SaveDataAsync(EDbType.BaseUserData, userData, userData.UserId);

            if(dbResult == EDbResult.Fail)
            {
                Result = EPacketResult.Fail;
            }

            return Result;
        }
    }
}
