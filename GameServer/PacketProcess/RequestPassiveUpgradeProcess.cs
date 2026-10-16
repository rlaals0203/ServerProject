public class RequestPassiveUpgradeProcess : BasePacketProcess
{
    public EPassiveType PassiveType;
    public int Level;
    public int UpgradePrice;

    public async Task<EPacketResult> ProcessAsync(UserBaseData userBaseData, DbManager dbManager, EPassiveType passiveType)
    {
        if(userBaseData == null)
        {
            Result = EPacketResult.InvalidRequest;
            return Result;
        }

        if(userBaseData.UserPassiveData.TryGetValue(passiveType.ToString(), out var passiveLevel) == false)
        {
            Result = EPacketResult.InvalidRequest;
            return Result;
        }

        if(GameDefine.PassiveMaxLevel <= passiveLevel)
        {
            Result = EPacketResult.InvalidRequest;
            return Result;
        }

        int upgradePrice = new GetUnitUpgradePrice(GameDefine.PassiveStartPrice, passiveLevel).Price;

        if(upgradePrice > userBaseData.UserMonetaData[EMonetaType.Exp.ToString()])
        {
            Result = EPacketResult.InvalidRequest;
            return Result;
        }

        userBaseData.UserMonetaData[EMonetaType.Exp.ToString()] -= upgradePrice;
        userBaseData.UserPassiveData[passiveType.ToString()]++;

        PassiveType = passiveType;
        Level = passiveLevel + 1;
        UpgradePrice = upgradePrice;

        await dbManager.SaveDataAsync(EDbType.BaseUserData, userBaseData, userBaseData.UserId);
        return Result;
    }
}