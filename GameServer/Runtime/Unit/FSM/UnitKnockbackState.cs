public class UnitKnockbackState : UnitState
{
    private const float knockbackTime = 0.6f;
    private float _currentTime;

    public UnitKnockbackState(RuntimeUnit owner) : base(owner)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        _currentTime = knockbackTime;
        _owner.Collider.Enabled = false;
    }

    public override void UpdateState(float dt)
    {
        base.UpdateState(dt);

        _currentTime -= dt;
        if(_currentTime <= 0f)
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