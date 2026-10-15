public class RequestUserCompleteStageProcess : BasePacketProcess
{
    public int StageNumber;

    public async Task<EPacketResult> ProcessAsync(UserBaseData userBaseData, DbManager dbManager, int stageNumber)
    {
        if(userBaseData == null)
        {
            Result = EPacketResult.InvalidRequest;
            return Result;
        }

        userBaseData.UserStageData = stageNumber++;
        await dbManager.SaveDataAsync(userBaseData, EDbType.BaseUserData, userBaseData.UserId);
        
        StageNumber = userBaseData.UserStageData;
        return Result;
    }
}