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

        if(stageNumber == -1)
        {
            Result = EPacketResult.InvalidRequest;
            return Result;
        }

        userBaseData.UserStageData = stageNumber + 1;
        await dbManager.SaveDataAsync(EDbType.BaseUserData, userBaseData, userBaseData.UserId);
        
        StageNumber = userBaseData.UserStageData;
        return Result;
    }
}