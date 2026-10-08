namespace GameServer
{
    public class RequestUnitDeployPacketProcess : BasePacketProcess
    {
        public Dictionary<int, string> DeployUnits;

        public async Task<EPacketResult> ProcessAsync(UserBaseData userData, Dictionary<int, string> deployUnits)
        {
            if(deployUnits == null)
            {
                Result = EPacketResult.InvalidRequest;
                return Result;
            }

            return Result;
        }
    }
}
