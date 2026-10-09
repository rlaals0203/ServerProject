public class UnitMoveState : UnitAttackableState
{
    protected UnitMoveState(RuntimeUnit owner) : base(owner)
    {
    }

    public override void UpdateState(float dt)
    {
        base.UpdateState(dt);
        _movementCompo.UpdateComponent(dt);
    }
}