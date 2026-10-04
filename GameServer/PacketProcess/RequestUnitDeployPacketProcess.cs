namespace GameServer
{
    public class RequestUnitDeployPacketProcess : BasePacketProcess
    {
        public Dictionary<int, string> DeployUnits;

        public async Task<EPacketResult> ProcessAsync(UserBaseData userData, Dictionary<int, string> deployUnits)
        {
            return Result;
        }
    }
}
