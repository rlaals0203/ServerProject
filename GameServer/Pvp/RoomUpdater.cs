public class RoomUpdater : BackgroundService
{
    private GameRoomService _gameRoomService;
    private const float DT = 0.166666f;

    public RoomUpdater(GameRoomService gameRoomService)
    {
        _gameRoomService = gameRoomService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _gameRoomService.UpdateRoom(DT);
        await Task.Delay((int)(DT * 1000), stoppingToken);
    }
}