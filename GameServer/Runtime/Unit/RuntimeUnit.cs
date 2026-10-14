using System.Numerics;

public class RuntimeUnit : ObjectBase
{
    public string RuntimeId { get; }
    public bool IsDead { get; private set; }
    public bool IsFreeze { get; private set; }
    public bool IsOpponent { get; }
    public UnitData UnitData { get; }
    public EStateType CurrentState { get; private set; }
    public event Action<RuntimeUnit> OnDead;

    private StateMachine _stateMachine;

    public RuntimeUnit(string runtimeId, UnitData unitData, bool isOpponent, RuntimeBattleField battleField, Vector3 position, ELayer layer)
        : base(battleField, position, layer)
    {
        RuntimeId = runtimeId;
        IsOpponent = isOpponent;
        UnitData = unitData;

        AddComponent(new StatComponent(this));
        AddComponent(new HealthComponent(this));
        AddComponent(new MovementComponent(this));
        AddComponent(new AbilityComponent(this));
        AddComponent(new AttackComponent(this));

        _stateMachine = new StateMachine(this);
        ChangeState(EStateType.Idle);
    }

    public override void Init()
    {
        base.Init();

        foreach(var component in _components.Values)
        {
            component.InitializeComponent();
        }
    }

    public override void Update(float dt)
    {
        foreach(var component in _components.Values)
        {
            component.UpdateComponent(dt);
        }

        if(IsFreeze)
        {
            return;
        }

        _stateMachine?.UpdateStateMachine(dt);
    }

    public void ChangeState(EStateType stateType)
    {
        CurrentState = stateType;
        _stateMachine?.ChangeState(stateType);
    }

    public void SetFreeze(bool isFreeze)
    {
        IsFreeze = isFreeze;
    }

    public void SetDead()
    {
        IsDead = true;
        OnDead?.Invoke(this);
    }
}