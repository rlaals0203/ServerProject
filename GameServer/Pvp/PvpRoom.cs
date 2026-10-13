using Microsoft.AspNetCore.SignalR;

public class PvpRoom
{
    public string UserId { get; set; }
    public string UserConnectionId { get; set; }
    public string OpponentUserId { get; set; }
    public string OpponentUserConnectionId { get; set; }
    public string RoomId { get; set; }
    private RuntimeBattleField _battleField;
    private UserBaseData _userData;
    private UserBaseData _opponentData;
    private readonly object _lock = new();

    public PvpRoom(UserBaseData userData, UserBaseData opponentdata)
    {
        _userData = userData;
        _opponentData = opponentdata;
        _battleField = new RuntimeBattleField(userData, opponentdata);
    }

    public bool SpawnUnit(string connectionId, string unitId)
    {
        if(string.IsNullOrEmpty(unitId))
            return false;

        bool isOpponent = connectionId == OpponentUserConnectionId;
        if(isOpponent == false && connectionId != UserConnectionId)
            return false;

        UserBaseData userData = isOpponent ? _opponentData : _userData;

        foreach(var unit in userData.UserUnitData.DeployUnitList.Values)
        {
            if(unit == null || unit.UnitStatData.UnitId != unitId)
                continue;

            if(userData.UserUnitData.OwnUnitList.TryGetValue(unitId, out var ownedUnit) == false)
                return false;

            if(ownedUnit == null)
                return false;

            lock(_lock)
            {
                return _battleField.SpawnUnit(ownedUnit, isOpponent);
            }
        }

        return false;
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

    public bool TryTakeMessage(out StandardMessageData message)
    {
        lock(_lock)
        {
            if(_battleField.TryTakeMessage(out message) == false)
                return false;

            message.RoomId = RoomId;
            return true;
        }
    }
}