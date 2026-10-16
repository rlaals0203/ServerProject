public class RequestUserMonetaChangeProcess : BasePacketProcess
{
    public EMonetaType MonetaType;
    public int Amount;

    public async Task<EPacketResult> ProcessAsync(UserBaseData userBaseData, DbManager dbManager, EMonetaType monetaType, int amount)
    {
        if(userBaseData == null)
        {
            Result = EPacketResult.InvalidRequest;
            return Result;
        }

        if(userBaseData.UserMonetaData.TryGetValue(monetaType.ToString(), out var moneta) == false)
        {
            Result = EPacketResult.InvalidRequest;
            return Result;
        }

        userBaseData.UserMonetaData[monetaType.ToString()] += amount;
        Amount = moneta + amount;
        MonetaType = monetaType;

        await dbManager.SaveDataAsync(EDbType.BaseUserData, userBaseData, userBaseData.UserId);
        return Result;
    }
}