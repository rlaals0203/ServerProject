public class UnitAttackableState : UnitState
{
    public UnitAttackableState(RuntimeUnit owner) : base(owner)
    {
    }

    public override void UpdateState(float dt)
    {
        base.UpdateState(dt);

        if(_attackCompo.CheckTarget() && _attackCompo.CanAttack())
        {
            _attackCompo.StartAttack();
            _owner.ChangeState(EStateType.Attack);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}