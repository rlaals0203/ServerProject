namespace GameServer
{
    public class RequestUnitDeployPacketProcess : BasePacketProcess
    {
        public Dictionary<int, UnitData> DeployUnits;

        public async Task<EPacketResult> ProcessAsync(UserBaseData userData, Dictionary<int, UnitData> deployUnits)
        {
            return Result;
        }
    }
}
