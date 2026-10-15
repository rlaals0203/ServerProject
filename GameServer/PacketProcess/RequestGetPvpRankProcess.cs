namespace GameServer
{

    public class RequestGetPvpRankProcess : BasePacketProcess
    {
        public PvpRankingData RankingData;

        public async Task<EPacketResult> ProcessAsync(PvpRankingService rankingService)
        {
            RankingData = await rankingService.GetRankingData();
            return Result;
        }
    }
}
