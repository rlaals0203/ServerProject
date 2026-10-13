using System.Numerics;

public class RuntimeBattleField
{
    private Dictionary<ELayer, List<RuntimeUnit>> _units = new();
    private MoneyHandler _moneyHandler;
    private Queue<StandardMessageData> _messageQueue = new();


    public RuntimeBattleField(UserBaseData userData, UserBaseData opponentData)
    {
        foreach (ELayer layer in Enum.GetValues<ELayer>())
        {
            _units[layer] = new List<RuntimeUnit>();
        }

        _moneyHandler = new MoneyHandler(userData, opponentData);
    }

    public void SpawnUnit(UnitData unitData, bool isOpponent, Vector3 position, ELayer layer)
    {
        if(unitData.UnitCost < _moneyHandler.GetMoney(isOpponent))
        {
            return;
        }

        RuntimeUnit newUnit = new RuntimeUnit(unitData, isOpponent, this, position, layer);
        
        newUnit.OnDead -= HandleUnitDead;
        newUnit.OnDead += HandleUnitDead;
        newUnit.Init();

        _units[layer].Add(newUnit);
    }

    private void HandleUnitDead(RuntimeUnit unit)
    {
        unit.OnDead -= HandleUnitDead;
        _units[unit.Layer].Remove(unit);
    }

    public void UpdateBattleField(float dt)
    {
        foreach(var unitList in _units.Values)
        {
            foreach(var unit in unitList)
            {
                unit?.Update(dt);
            }
        }

        AddMessage(UpdateAllUnitDataMessage.ROUTE, CreateUpdateAllUnitDataMessgae());
        _moneyHandler.Update(dt);

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
                    Position = unit.Position,
                    IsOpponent = unit.IsOpponent
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

    public Queue<StandardMessageData> TakeMessageQueue()
    {
        try
        {
            return _messageQueue;
        }
        finally
        {
            _messageQueue.Clear();
        }
    }

    public void ReleaseBattleField()
    {
        foreach(var layer in _units.Keys)
        {
            foreach(var unit in _units[layer])
            {
                unit.SetDead();
            }
        }
    }
}