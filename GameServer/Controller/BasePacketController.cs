using Microsoft.AspNetCore.Mvc;

[ApiController]
public partial class BasePacketController(ILogger<StandardPacketData> logger, DbManager dbManager, GameDataManager gameDataManager, ILoggerFactory loggerFactory)
    : ControllerBase
{
    private readonly ILogger<StandardPacketData> logger = logger;
    protected readonly DbManager dbManager = dbManager;
    protected readonly GameDataManager gameDataManager = gameDataManager;
    protected readonly ILoggerFactory loggerFactory = loggerFactory;
}