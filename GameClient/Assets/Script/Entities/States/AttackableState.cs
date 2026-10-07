public class UnitAttackableState : UnitState
{
    public UnitAttackableState(Unit entity, int animationHash) : base(entity, animationHash)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(_attackCompo.CheckTarget())
        {
            _owner.ChangeState(EStateType.Attack);
        }
    }
}