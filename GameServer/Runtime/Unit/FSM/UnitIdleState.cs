public class UnitIdleState : UnitAttackableState
{
    public UnitIdleState(RuntimeUnit owner) : base(owner)
    {
    }

    public override void UpdateState(float dt)
    {
        base.UpdateState(dt);

        if(_attackCompo.CheckTarget() == false)
        {
            _owner.ChangeState(EStateType.Move);
        }
    }
}