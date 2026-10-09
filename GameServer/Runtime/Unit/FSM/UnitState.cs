public class UnitState
{
    protected RuntimeUnit _owner;
    protected MovementComponent _movementCompo;
    protected HealthComponent _healthCompo;
    protected StatComponent _statCompo;

    protected UnitState(RuntimeUnit owner)
    {
        _owner = owner;
        _movementCompo = owner.Get<MovementComponent>();
        _healthCompo = owner.Get<HealthComponent>();
        _statCompo = owner.Get<StatComponent>();
    }

    public virtual void EnterState()
    {

    }

    public virtual void UpdateState(float dt) { }

    public virtual void ExitState()
    {
        
    }
}