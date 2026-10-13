using Microsoft.AspNetCore.SignalR;

public class PvpRoom
{
    public string UserId { get; set; }
    public string UserConnectionId { get; set; }
    public string OpponentUserId { get; set; }
    public string OpponentUserConnectionId { get; set; }
    public string RoomId { get; set; }
    private RuntimeBattleField _battleField;
    private Queue<StandardMessageData> _messageQueue = new();

    public PvpRoom(UserBaseData userData, UserBaseData opponentdata)
    {
        _battleField = new RuntimeBattleField(userData, opponentdata);
    }

    public void UpdateRoom(float dt)
    {
        _battleField.UpdateBattleField(dt);
    }

    public void DestroyRoom()
    {
        _battleField.ReleaseBattleField();
    }

    public Queue<StandardMessageData> TakeMessageQueue() => _battleField.TakeMessageQueue();
}