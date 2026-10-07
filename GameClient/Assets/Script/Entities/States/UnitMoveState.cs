public class UnitMoveState : UnitAttackableState
{
    public UnitMoveState(Unit entity, int animationHash) : base(entity, animationHash)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _movementCompo.MovingUnit();
    }
}