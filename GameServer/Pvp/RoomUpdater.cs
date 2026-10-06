public class RoomUpdater : IHostedService
{
    private GameRoomService GameRoomService;

    public RoomUpdater(GameRoomService gameRoomService)
    {
        GameRoomService = gameRoomService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return null;
    }
}