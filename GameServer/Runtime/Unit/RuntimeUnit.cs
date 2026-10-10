using System.Numerics;

public class RuntimeUnit : ObjectBase
{
    public bool IsDead { get; private set; }
    public bool IsFreeze { get; private set; }
    public bool IsOpponent { get; }
    public UnitData UnitData { get; }

    private StateMachine _stateMachine;

    public RuntimeUnit(UnitData unitData, bool isOpponent, RuntimeBattleField battleField, Vector3 position, ELayer layer) 
        : base(battleField, position, layer)
    {
        IsOpponent = isOpponent;
        UnitData = unitData;

        AddComponent(new HealthComponent(this));
        AddComponent(new StatComponent(this));
        AddComponent(new MovementComponent(this));
        AddComponent(new AbilityComponent(this));
        AddComponent(new AttackComponent(this));

        _stateMachine = new StateMachine(this);
        _stateMachine.ChangeState(EStateType.Idle);
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
        base.Update(dt);

        if(IsFreeze)
        {
            return;
        }

        _stateMachine?.UpdateStateMachine(dt);
    }

    public void ChangeState(EStateType stateType)
    {
        _stateMachine?.ChangeState(stateType);
    }

    public void SetFreeze(bool isFreeze)
    {
        IsFreeze = isFreeze;
    }
}