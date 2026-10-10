public class UnitState
{
    protected RuntimeUnit _owner;
    protected MovementComponent _movementCompo;
    protected HealthComponent _healthCompo;
    protected StatComponent _statCompo;
    protected AttackComponent _attackCompo;

    protected UnitState(RuntimeUnit owner) //반드시 컴포넌트 등록 후 statemachine 생성
    {
        _owner = owner;
        _movementCompo = owner.Get<MovementComponent>();
        _healthCompo = owner.Get<HealthComponent>();
        _statCompo = owner.Get<StatComponent>();
        _attackCompo = owner.Get<AttackComponent>();
    }

    public virtual void EnterState() { }

    public virtual void UpdateState(float dt) { }

    public virtual void ExitState() { }
}