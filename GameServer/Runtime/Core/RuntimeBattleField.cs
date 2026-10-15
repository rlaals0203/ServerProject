using System.Numerics;

public class RuntimeBattleField
{
    private Dictionary<ELayer, Dictionary<string, RuntimeUnit>> _units = new();
    private Queue<string> _deadUnitQueue = new();
    private Queue<StandardMessageData> _messageQueue = new();
    private MoneyHandler _moneyHandler;
    private UnitSpawnHandler _unitSpawnHandler;

    private Castle _unitCastle;
    private Castle _opponentCastle;

    private int _runtimeId = 2;
    private float _maxUnitCastleHealth;
    private float _maxOpponentCastleHealth;

    private const float SpawnX = 6.8f;
    private const float SpawnY = -2f;


    public RuntimeBattleField(UserBaseData userData, UserBaseData opponentData)
    {
        foreach (ELayer layer in Enum.GetValues<ELayer>())
        {
            _units[layer] = new Dictionary<string, RuntimeUnit>();
        }

        _moneyHandler = new MoneyHandler(userData, opponentData);
        _unitSpawnHandler = new UnitSpawnHandler(userData.UserUnitData, opponentData.UserUnitData);
        InitailizeCastle(userData, opponentData);
        AddMessage(UpdateCastleDataMessage.ROUTE, CreateCastleDataMessage());
    }

    private void InitailizeCastle(UserBaseData userData, UserBaseData opponentData)
    {
        _maxUnitCastleHealth = userData.UserPassiveData[EPassiveType.BaseDefense.ToString()] * 500;
        _maxOpponentCastleHealth = opponentData.UserPassiveData[EPassiveType.BaseDefense.ToString()] * 500;
        _unitCastle = new Castle("0", false, this, GetSpawnPos(false), ELayer.Unit);
        _unitCastle.InitCastle(_maxUnitCastleHealth);
        _opponentCastle = new Castle("1", true, this, GetSpawnPos(true), ELayer.Enemy);
        _opponentCastle.InitCastle(_maxOpponentCastleHealth);

        _units[ELayer.Unit].Add("0", _unitCastle);
        _units[ELayer.Enemy].Add("1", _opponentCastle);

        _unitCastle.OnHit += HandleCastleHit;
        _opponentCastle.OnHit += HandleCastleHit;
    }

    private void HandleCastleHit(RuntimeUnit unit)
    {
        if(unit is not Castle)
            return;

        AddMessage(UpdateCastleDataMessage.ROUTE, CreateCastleDataMessage());
    }

    public bool SpawnUnit(UnitData unitData, bool isOpponent)
    {
        string unitId = unitData.UnitStatData.UnitId;

        if(_unitSpawnHandler.CheckCanSpawn(unitId, isOpponent) == false)
            return false;

        if(_moneyHandler.TrySpendMoney(isOpponent, unitData.UnitCost) == false)
            return false;

        ELayer layer = isOpponent ? ELayer.Enemy : ELayer.Unit;
        Vector3 position = GetSpawnPos(isOpponent);
        RuntimeUnit newUnit = new RuntimeUnit(_runtimeId.ToString(), unitData, isOpponent, this, position, layer);
        _runtimeId++;

        newUnit.OnDead += HandleUnitDead;
        newUnit.OnHit += HandleUnitHit;
        newUnit.Init();

        _units[layer].Add(newUnit.RuntimeId, newUnit);
        _unitSpawnHandler.SetSpawnStatus(unitId, unitData.UnitCooldown, isOpponent);
        return true;
    }

    private void HandleUnitHit(RuntimeUnit unit)
    {
        PlayHitEffectDataMessage message = new()
        {
            IsOpponent = unit.IsOpponent,
            PosX = unit.Position.X,
            PosY = unit.Position.Y
        };

        AddMessage(PlayHitEffectDataMessage.ROUTE, message);
    }

    private void HandleUnitDead(RuntimeUnit unit)
    {
        unit.OnDead -= HandleUnitDead;
        _deadUnitQueue.Enqueue(unit.RuntimeId);
    }

    public void UpdateBattleField(float dt)
    {
        _moneyHandler.Update(dt);
        _unitSpawnHandler.Update(dt);

        foreach(var unitList in _units.Values)
        {
            foreach(var unit in unitList.Values)
            {
                unit?.Update(dt);
            }
        }

        AddMessage(PVPBattleInfoDataMessage.ROUTE, CreatePVPBattleFieldInfoDataMessage());

        while(_deadUnitQueue.Count != 0)
        {
            var deadUnitId = _deadUnitQueue.Dequeue();
            
            foreach(var layer in _units.Keys)
            {
                _units[layer].Remove(deadUnitId);
            }
        }
    }

    private UpdateCastleDataMessage CreateCastleDataMessage()
    {
        return new UpdateCastleDataMessage()
        {
            UnitCastleData = new CastleData()
            {
                CurrentHealth = _unitCastle.Get<HealthComponent>().GetCurrentHealth(),
                MaxHealth = _maxUnitCastleHealth,
                PositionX = _unitCastle.Position.X,
                PositionY = _unitCastle.Position.Y
            },
            OpponentCastleData = new CastleData()
            {
                CurrentHealth = _opponentCastle.Get<HealthComponent>().GetCurrentHealth(),
                MaxHealth = _maxOpponentCastleHealth,
                PositionX = _opponentCastle.Position.X,
                PositionY = _opponentCastle.Position.Y
            }
        };
    }

    private Vector3 GetSpawnPos(bool isOpponent)
    {
        return new Vector3(isOpponent ? -SpawnX : SpawnX, SpawnY, 0f);
    }

    public Dictionary<string, RuntimeUnit> GetAllUnit(ELayer layer)
    {
        return _units[layer];
    }

    private List<RuntimeUnitData> GetAllUnitData()
    {
        List<RuntimeUnitData> allUnitData = new();

        foreach(var layer in _units.Keys)
        {
            foreach(var unit in _units[layer].Values)
            {
                if(unit.RuntimeId == "0" || unit.RuntimeId == "1") //성이면 스킵
                    continue;

                AbilityComponent abilityCompo = unit.Get<AbilityComponent>();
                MovementComponent movementCompo = unit.Get<MovementComponent>();
                RuntimeUnitData unitData = new RuntimeUnitData()
                {
                    RuntimeId = unit.RuntimeId,
                    UnitId = unit.UnitData.UnitStatData.UnitId,
                    PositionX = unit.Position.X,
                    PositionY = unit.Position.Y,
                    IsOpponent = unit.IsOpponent,
                    IsDead = unit.IsDead,
                    State = unit.CurrentState,
                    AbilityStatus = abilityCompo.AbilityStatus,
                    FreezeApplyCount = abilityCompo.FreezeApplyCount,
                    HitbackCount = movementCompo.HitbackCount
                };

                allUnitData.Add(unitData);
            }
        }

        return allUnitData;
    }

    private PVPBattleInfoDataMessage CreatePVPBattleFieldInfoDataMessage()
    {
        UpdateUnitCooldownData cooldownMessage = new()
        {
            UnitCooldowns = _unitSpawnHandler.GetRemainCooldown(false),
            OpponentUnitCooldowns = _unitSpawnHandler.GetRemainCooldown(true)
        };

        PVPBattleInfoDataMessage pvpBattleInfoData = new PVPBattleInfoDataMessage()
        {
            AllUnitDatas = GetAllUnitData(),
            UnitCooldownData = cooldownMessage,
            CurrentMoney = _moneyHandler.GetMoney(false),
            OpponentCurrentMoney = _moneyHandler.GetMoney(true)
        };

        return pvpBattleInfoData;
    }

    public void AddMessage<TMessage>(string route, TMessage message)
        where TMessage : StandardMessageData
    {
        message.Route = route;
        _messageQueue.Enqueue(message);
    }

    public bool TryGetMessage(out StandardMessageData message)
    {
        return _messageQueue.TryDequeue(out message);
    }

    public void ReleaseBattleField()
    {
        foreach(var unitList in _units.Values)
        {
            foreach(var unit in unitList.Values)
            {
                unit.SetDead();
                unit.Release();
            }
        }
    }
}