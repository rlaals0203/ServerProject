public class RequestPassiveUpgradeProcess : BasePacketProcess
{
    public EPassiveType PassiveType;
    public int Level;

    public async Task<EPacketResult> ProcessAsync(UserBaseData userBaseData, DbManager dbManager, EPassiveType passiveType)
    {
        if(userBaseData == null)
        {
            Result = EPacketResult.InvalidRequest;
            return Result;
        }

        if(userBaseData.UserPassiveData.TryGetValue(passiveType.ToString(), out var passive) == false)
        {
            Result = EPacketResult.InvalidRequest;
            return Result;
        }

        if(GameDefine.PassiveMaxLevel <= passive)
        {
            Result = EPacketResult.InvalidRequest;
            return Result;
        }

        userBaseData.UserPassiveData[passiveType.ToString()]++;
        await dbManager.SaveDataAsync(userBaseData, EDbType.BaseUserData, userBaseData.UserId);
        return Result;
    }
}