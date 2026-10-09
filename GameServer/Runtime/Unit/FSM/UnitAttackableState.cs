public class UnitAttackableState : UnitState
{
    private float _lastAttackTime;
    private float _attackInterval;

    public UnitAttackableState(RuntimeUnit owner) : base(owner)
    {
        _attackInterval = owner.UnitData.UnitAttackData.UnitAttackSpeed;
    }

    public override void UpdateState(float dt)
    {
        base.UpdateState(dt);
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}