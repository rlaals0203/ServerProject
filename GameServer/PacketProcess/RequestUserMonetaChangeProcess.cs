public class RequestUserMonetaChangeProcess : BasePacketProcess
{
    public EMonetaType MonetaType;
    public int Amount;

    public async Task<EPacketResult> ProcessAsync(UserBaseData userBaseData, DbManager dbManager, EMonetaType monetaType, int Amount)
    {
        if(userBaseData == null)
        {
            Result = EPacketResult.InvalidRequest;
            return Result;
        }

        if(userBaseData.UserMonetaData.TryGetValue(monetaType, out var moneta) == false)
        {
            Result = EPacketResult.InvalidRequest;
            return Result;
        }

        userBaseData.UserMonetaData[monetaType] += Amount;
        MonetaType = monetaType;
        Amount = userBaseData.UserMonetaData[monetaType];

        await dbManager.SaveDataAsync(userBaseData, EDbType.BaseUserData, userBaseData.UserId);
        return Result;
    }
}