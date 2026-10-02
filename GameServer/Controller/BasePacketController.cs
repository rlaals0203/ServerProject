using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[Controller]")]
public partial class BasePacketController(ILogger<StandardPacketData> logger, GameDataManager gameDataManager, ILoggerFactory loggerFactory)
    : ControllerBase
{
    private readonly ILogger<StandardPacketData> logger = logger;
    protected readonly GameDataManager gameDataManager = gameDataManager;
    protected readonly ILoggerFactory loggerFactory = loggerFactory;
}