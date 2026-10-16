using Microsoft.AspNetCore.SignalR;

public class PvpRoom
{
    public int Point => _battleField.Point;
    private RuntimeBattleField _battleField;
    private UserBaseData _userData;
    private UserBaseData _opponentData;
    private readonly object _lock = new();

    public string UserId { get; set; }
    public string UserConnectionId { get; set; }
    public string OpponentUserId { get; set; }
    public string OpponentUserConnectionId { get; set; }
    public string RoomId { get; set; }
    public bool IsRankProcessed { get; set; }
    public bool IsComplete => _battleField.IsComplete;
    public bool IsOpponentWinner => _battleField.IsOpponentWinner;

    public PvpRoom(UserBaseData userData, UserBaseData opponentdata)
    {
        _userData = userData;
        _opponentData = opponentdata;
        _battleField = new RuntimeBattleField(userData, opponentdata);
    }

    public bool SpawnUnit(string connectionId, string unitId)
    {
        bool isOpponent = connectionId == OpponentUserConnectionId;
        
        if(isOpponent == false && connectionId != UserConnectionId)
            return false;

        UserBaseData userData = isOpponent ? _opponentData : _userData;
        UnitData targetUnit = null;

        foreach(var unit in userData.UserUnitData.DeployUnitList.Values)
        {
            if(unit.UnitStatData.UnitId == unitId)
            {
                targetUnit = unit;
                break;
            }
        }
        
        if(targetUnit == null)
            return false;

        lock(_lock)
        {
            return _battleField.SpawnUnit(targetUnit, isOpponent);
        }
    }

    public void UpdateRoom(float dt)
    {
        lock(_lock)
        {
            _battleField.UpdateBattleField(dt);
        }
    }

    public void DestroyRoom()
    {
        lock(_lock)
        {
            _battleField.ReleaseBattleField();
        }
    }

    public UpdateCastleDataMessage GetCastleData()
    {
        lock(_lock)
        {
            return _battleField.CreateCastleDataMessage();
        }
    }

    public bool TryGetMessage(out StandardMessageData message)
    {
        lock(_lock)
        {
            if(_battleField.TryGetMessage(out message) == false)
                return false;

            message.RoomId = RoomId;
            return true;
        }
    }
}