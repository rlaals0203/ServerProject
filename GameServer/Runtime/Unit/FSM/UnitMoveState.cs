public class UnitMoveState : UnitAttackableState
{
    public UnitMoveState(RuntimeUnit owner) : base(owner)
    {
    }

    public override void UpdateState(float dt)
    {
        base.UpdateState(dt);

        if (_attackCompo.CheckTarget())
        {
            _owner.ChangeState(EStateType.Idle);
            return;
        }

        _movementCompo.UpdateComponent(dt);
    }
}