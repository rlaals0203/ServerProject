public class UnitIdleState : UnitAttackableState
{
    public UnitIdleState(Unit entity, int animationHash) : base(entity, animationHash)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _movementCompo.MovingUnit();
    }
}