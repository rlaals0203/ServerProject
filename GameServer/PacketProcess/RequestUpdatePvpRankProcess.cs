namespace GameServer
{
    public class RequestUpdatePvpRankProcess : BasePacketProcess
    {
        public int Point;

        public async Task<EPacketResult> ProcessAsync(UserBaseData userData, PvpRankingService pvpRankingService, int point)
        {
            if(userData == null)
            {
                Result = EPacketResult.InvalidRequest;
                return Result;
            }

            var rankData = await pvpRankingService.UpdateRanking(userData.UserId, point);

            if(rankData == null)
            {
                Result = EPacketResult.InvalidRequest;
                return Result;
            }

            Point = rankData.Point;
            return Result;
        }
    }
}
