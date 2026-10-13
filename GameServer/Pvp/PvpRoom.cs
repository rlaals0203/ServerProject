using Microsoft.AspNetCore.SignalR;

public class PvpRoom
{
    public string UserId { get; set; }
    public string UserConnectionId { get; set; }
    public string OpponentUserId { get; set; }
    public string OpponentUserConnectionId { get; set; }
    private RuntimeBattleField _battleField;
    private IHubContext _hubContext;


    public PvpRoom()
    {
        _battleField = new RuntimeBattleField();
    }

    public void UpdateRoom(float dt)
    {
        _battleField.UpdateBattleField(dt);
    }

    public void DestroyRoom()
    {
        
    }
}