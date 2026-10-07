public class UnitAttackState : UnitState
{
    private MovementCompo _movementCompo;

    public UnitAttackState(Unit entity, int animationHash) : base(entity, animationHash)
    {
        _movementCompo = entity.Get<MovementCompo>();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_isTriggerCall)
            _owner.ChangeState(EStateType.Idle);
    }
}