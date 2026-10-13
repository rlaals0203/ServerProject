// using Microsoft.AspNetCore.SignalR;
// using VolleyBallServer.DTOs;
// using VolleyBallServer.Messages;
// using VolleyBallServer.Services;

// namespace VolleyBallServer.Hubs
// {
//     public class GameHub : Hub
//     {
//         private readonly MatchService _matchService;
//         private readonly GameRoomService _gameRoomService;

//         public GameHub(MatchService matchService, GameRoomService gameRoomService)
//         {
//             _matchService = matchService;
//             _gameRoomService = gameRoomService;
//         }

//         public async Task JoinMatch(string userId)
//         {
//             var currentConnectionId = Context.ConnectionId; //현재 연결된 SignalR ID

//             //현재 유저를 대기열에 넣거나, 대기 중인 상대와 매칭
//             var matchResult = await _matchService.JoinMatchAsync(userId, currentConnectionId);

//             //먼저 들어왔다면 대기 상태
//             if (matchResult.MatchStatus == MatchStatus.Waiting)
//             {
//                 await Clients.Caller.SendAsync("Waiting");
//                 return;
//             }

//             //매칭 요청 실패 시, FailedDTO 전송
//             if (matchResult.MatchStatus == MatchStatus.Failed)
//             {
//                 await Clients.Caller.SendAsync("MatchFailed", new MatchFailedDTO
//                 {
//                     Message = matchResult.Message,
//                     KeyCount = matchResult.MyKeyCount
//                 });

//                 return;
//             }

//             if(matchResult.MatchStatus == MatchStatus.Matched) //매치 성사
//             {
//                 var opponent = matchResult.Opponent; //상대 유저

//                 if(opponent == null) //상대 유저 정보 오류 시, FailedDTO 전송
//                 {
//                     await Clients.Caller.SendAsync("MatchFailed", new MatchFailedDTO
//                     {
//                         Message = "매칭 상대 정보가 없습니다.",
//                         KeyCount = matchResult.MyKeyCount
//                     });
//                     return;
//                 }

//                 //roomId 그룹에 두 연결을 등록
//                 await Groups.AddToGroupAsync(currentConnectionId, matchResult.RoomId);
//                 await Groups.AddToGroupAsync(opponent.ConnectionId, matchResult.RoomId);

//                 //GameRoomService를 이용해 방 생성
//                 _gameRoomService.CreateRoom(matchResult.RoomId, userId, currentConnectionId,
//                     opponent.UserId, opponent.ConnectionId);

//                 //유저들에게 각각 MatchFoundDTO 전송
//                 await Clients.Client(currentConnectionId).SendAsync("MatchFound", new MatchFoundDTO
//                 {
//                     RoomId = matchResult.RoomId,
//                     MyUserId = userId,
//                     OpponentUserId = opponent.UserId,
//                     KeyCount = matchResult.MyKeyCount
//                 });


//                 await Clients.Client(opponent.ConnectionId).SendAsync("MatchFound", new MatchFoundDTO
//                 {
//                     RoomId = matchResult.RoomId,
//                     MyUserId = opponent.UserId,
//                     OpponentUserId = userId,
//                     KeyCount = matchResult.OpponentKeyCount
//                 });
//             }
//         }

//         public override async Task OnDisconnectedAsync(Exception? exception)
//         {
//             _matchService.CancelMatch(Context.ConnectionId);
//             _gameRoomService.RemoveConnection(Context.ConnectionId);
//             await base.OnDisconnectedAsync(exception);
//         }

//         public Task SendInput(PlayerInputMessage input)
//         {
//             var success = _gameRoomService.UpdateInput(Context.ConnectionId, input);

//             if (!success)
//             {
//                 Console.WriteLine("Input update failed");
//             }

//             return Task.CompletedTask;
//         }

//         public async Task<bool> SendChatMessage(ChatMessage chatMessage)
//         {
//             if (string.IsNullOrWhiteSpace(chatMessage.Message))
//                 return false;

//             if (!_gameRoomService.TryGetRoomId(Context.ConnectionId, out var roomId) || roomId == null)
//                 return false;

//             chatMessage.RoomId = roomId;
//             chatMessage.Message = chatMessage.Message.Trim();
//             chatMessage.SentAt = DateTime.UtcNow.ToString("O");

//             await Clients.Group(roomId).SendAsync("ReceiveChatMessage", chatMessage);
//             return true;
//         }
//     }
// }