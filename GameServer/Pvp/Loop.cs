// using Microsoft.AspNetCore.SignalR;
// using VolleyBallServer.Hubs;
// using VolleyBallServer.Messages;
// using VolleyBallServer.Repositories;

// namespace VolleyBallServer.Services
// {
//     public class GameLoopService : BackgroundService
//     {
//         private readonly GameRoomService _gameRoomService;
//         private readonly IHubContext<GameHub> _hubContext;
//         private readonly IServiceScopeFactory _scopeFactory;
//         private int _tick;

//         private const float DT = 0.033f;
//         private const int TargetScore = 13;

//         public GameLoopService(GameRoomService gameRoomService, IHubContext<GameHub> hubContext, IServiceScopeFactory scopeFactory)
//         {
//             _gameRoomService = gameRoomService;
//             _hubContext = hubContext;
//             _scopeFactory = scopeFactory;
//         }

//         protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//         {
//             while (!stoppingToken.IsCancellationRequested)
//             {
//                 _tick++;
//                 _gameRoomService.UpdateRooms(DT);

//                 var scoreMessages = _gameRoomService.GetScoreMessages();

//                 foreach (var scoreMessage in scoreMessages)
//                 {
//                     await _hubContext.Clients.Group(scoreMessage.RoomId)
//                         .SendAsync("GetScore", scoreMessage, stoppingToken);

//                     await TryGameEnd(scoreMessage, stoppingToken);
//                 }

//                 foreach (var roomId in _gameRoomService.GetRoomIds())
//                 {
//                     if (_gameRoomService.TryCreateStateMessage(roomId, _tick, out var stateMessage))
//                     {
//                         await _hubContext.Clients.Group(roomId)
//                             .SendAsync("GameState", stateMessage, stoppingToken);
//                     }
//                 }

//                 await Task.Delay((int)(DT * 1000), stoppingToken);
//             }
//         }

//         private async Task TryGameEnd(ScoreMessage scoreMessage, CancellationToken stoppingToken)
//         {
//             if (scoreMessage.ScoreA < TargetScore && scoreMessage.ScoreB < TargetScore)
//                 return;

//             var gameEndMessage = new GameEndMessage
//             {
//                 WinnerUserId = scoreMessage.ScoredUserId,
//                 LoserUserId = scoreMessage.ConcededUserId
//             };

//             await _hubContext.Clients.Group(scoreMessage.RoomId)
//                 .SendAsync("GameEnd", gameEndMessage, stoppingToken);

//             try
//             {
//                 await ApplyGameResultAsync(gameEndMessage);
//             }
//             catch (Exception exception)
//             {
//                 Console.WriteLine($"Game result update failed:{exception.Message}");
//             }
//             finally
//             {
//                 _gameRoomService.RemoveRoom(scoreMessage.RoomId);
//             }
//         }

//         private async Task ApplyGameResultAsync(GameEndMessage gameEndMessage)
//         {
//             using var scope = _scopeFactory.CreateScope();
//             var userRepository = scope.ServiceProvider.GetRequiredService<UserRepository>();

//             var winner = await userRepository.GetUserById(gameEndMessage.WinnerUserId);
//             var loser = await userRepository.GetUserById(gameEndMessage.LoserUserId);

//             if (winner != null)
//             {
//                 winner.TotalPlayTime++;
//                 winner.WinCount++;
//                 await userRepository.UpdateAsync(winner);
//             }

//             if (loser != null)
//             {
//                 loser.TotalPlayTime++;
//                 loser.LoseCount++;
//                 await userRepository.UpdateAsync(loser);
//             }
//         }
//     }
// }