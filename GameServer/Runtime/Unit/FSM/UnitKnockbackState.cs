public class UnitKnockbackState : UnitState
{
    public UnitKnockbackState(RuntimeUnit owner) : base(owner)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        _owner.Collider.Enabled = false;
    }

    public override void UpdateState(float dt)
    {
        base.UpdateState(dt);

        if(_movementCompo.UpdateKnockback(dt))
        {
            _owner.ChangeState(EStateType.Idle);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        _owner.Collider.Enabled = true;
    }
}