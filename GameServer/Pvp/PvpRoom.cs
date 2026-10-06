public class PvpRoom
{
    public string UserId { get; set; }
    public string UserConnectionId { get; set; }
    public string OpponentUserId { get; set; }
    public string OpponentUserConnectionId { get; set; }
    private PvpBattleField BattleField;

    public PvpRoom()
    {
        BattleField = new PvpBattleField();
    }

    public void UpdateRoom(float dt)
    {
        BattleField.UpdateBattleField(dt);
    }

    public void DestroyRoom()
    {
        
    }
}