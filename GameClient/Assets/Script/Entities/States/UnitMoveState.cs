public class UnitMoveState : UnitAttackableState
{
    public UnitMoveState(Unit entity, int animationHash) : base(entity, animationHash)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_attackCompo.CheckTarget())
        {
            _owner.ChangeState(EStateType.Idle);
        }

        _movementCompo.MovingUnit();
    }
}