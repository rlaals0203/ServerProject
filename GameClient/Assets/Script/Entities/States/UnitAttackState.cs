public class UnitAttackState : UnitState
{
    public UnitAttackState(Unit entity, int animationHash) : base(entity, animationHash)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_isTriggerCall)
            _owner.ChangeState(EStateType.Idle);
    }
}