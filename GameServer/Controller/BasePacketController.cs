using Microsoft.AspNetCore.Mvc;

[ApiController]
public partial class BasePacketController(ILogger<StandardPacketData> logger, DbManager dbManager, GameDataManager gameDataManager, 
    PvpRankingService pvpRankingService, UnitDataDefine unitDataDefine, ILoggerFactory loggerFactory)
    : ControllerBase
{
    private readonly ILogger<StandardPacketData> logger = logger;
    protected readonly DbManager dbManager = dbManager;
    protected readonly GameDataManager gameDataManager = gameDataManager;
    protected readonly PvpRankingService pvpRankingService = pvpRankingService;
    protected readonly UnitDataDefine unitDataDefine = unitDataDefine;
    protected readonly ILoggerFactory loggerFactory = loggerFactory;
}