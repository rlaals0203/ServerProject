public class UnitMoveState : UnitAttackableState
{
    public UnitMoveState(Unit entity, int animationHash) : base(entity, animationHash)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_owner.CurrentState == EStateType.Attack)
            return;

        if (_attackCompo.CheckTarget())
        {
            _owner.ChangeState(EStateType.Idle);
            return;
        }

        _movementCompo.MovingUnit();
    }
}
