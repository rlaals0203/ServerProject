using System.Numerics;

public class RuntimeBattleField
{
    private Dictionary<ELayer, List<RuntimeUnit>> _units = new();
    private MoneyHandler _moneyHandler;
    private Queue<StandardMessageData> _messageQueue = new();
    private Dictionary<string, float> _userSpawnTimes = new();
    private Dictionary<string, float> _opponentSpawnTimes = new();
    private float _currentTime;
    private int _nextRuntimeId;

    private const float SpawnX = 7f;
    private const float SpawnY = -1.8f;


    public RuntimeBattleField(UserBaseData userData, UserBaseData opponentData)
    {
        foreach (ELayer layer in Enum.GetValues<ELayer>())
        {
            _units[layer] = new List<RuntimeUnit>();
        }

        _moneyHandler = new MoneyHandler(userData, opponentData);
    }

    public bool SpawnUnit(UnitData unitData, bool isOpponent)
    {
        string unitId = unitData.UnitStatData.UnitId;
        var spawnTimes = isOpponent ? _opponentSpawnTimes : _userSpawnTimes;
        if(spawnTimes.TryGetValue(unitId, out var nextSpawnTime) && _currentTime < nextSpawnTime)
            return false;

        if(_moneyHandler.TrySpendMoney(isOpponent, unitData.UnitCost) == false)
            return false;

        ELayer layer = isOpponent ? ELayer.Enemy : ELayer.Unit;
        Vector3 position = new Vector3(isOpponent ? -SpawnX : SpawnX, SpawnY, 0f);
        RuntimeUnit newUnit = new RuntimeUnit(++_nextRuntimeId, unitData, isOpponent, this, position, layer);
        
        newUnit.OnDead += HandleUnitDead;
        newUnit.Init();

        _units[layer].Add(newUnit);
        spawnTimes[unitId] = _currentTime + unitData.UnitCooldown;
        return true;
    }

    private void HandleUnitDead(RuntimeUnit unit)
    {
        unit.OnDead -= HandleUnitDead;
        _units[unit.Layer].Remove(unit);
    }

    public void UpdateBattleField(float dt)
    {
        _currentTime += dt;
        _moneyHandler.Update(dt);

        foreach(var unitList in _units.Values)
        {
            foreach(var unit in unitList)
            {
                unit?.Update(dt);
            }
        }

        AddMessage(UpdateAllUnitDataMessage.ROUTE, CreateUpdateAllUnitDataMessgae());

    }

    public List<RuntimeUnit> GetAllUnit(ELayer layer)
    {
        return _units[layer];
    }

    private UpdateAllUnitDataMessage CreateUpdateAllUnitDataMessgae()
    {
        List<RuntimeUnitData> allUnitData = new();

        foreach(var layer in _units.Keys)
        {
            foreach(var unit in _units[layer])
            {
                RuntimeUnitData unitData = new RuntimeUnitData()
                {
                    RuntimeId = unit.RuntimeId,
                    UnitId = unit.UnitData.UnitStatData.UnitId,
                    PositionX = unit.Position.X,
                    PositionY = unit.Position.Y,
                    IsOpponent = unit.IsOpponent,
                    State = unit.CurrentState
                };

                allUnitData.Add(unitData);
            }
        }
        
        return new UpdateAllUnitDataMessage()
        {
            AllUnitDatas = allUnitData
        };
    }

    public void AddMessage<TMessage>(string route, TMessage message)
        where TMessage : StandardMessageData
    {
        message.Route = route;
        _messageQueue.Enqueue(message);
    }

    public bool TryTakeMessage(out StandardMessageData message)
    {
        return _messageQueue.TryDequeue(out message);
    }

    public void ReleaseBattleField()
    {
        foreach(var unitList in _units.Values)
        {
            while(unitList.Count > 0)
            {
                unitList[unitList.Count - 1].SetDead();
            }
        }
    }
}