namespace GameServer
{

    public class RequestGetPvpRankProcess : BasePacketProcess
    {
        public PvpRankingData RankingData;

        public async Task<EPacketResult> ProcessAsync()
        {
            return Result;
        }
    }
}
