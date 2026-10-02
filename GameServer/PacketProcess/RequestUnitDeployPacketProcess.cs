namespace GameServer
{
    public class RequestUnitDeployPacketProcess : BasePacketProcess
    {
        public UnitData[] ChangedDeck;

        public async Task<EPacketResult> ProcessAsync(UserBaseData userData, UnitData[] changedDeck)
        {
            return Result;
        }
    }
}
