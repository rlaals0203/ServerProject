using UnityEngine;
public class UnitKnockbackState : UnitState
{
    private const float knockbackTime = 0.8f;
    private float _currentTime;

    public UnitKnockbackState(Unit owner, int animationHash) : base(owner, animationHash)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        _currentTime = Time.time;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(Time.time - _currentTime >= knockbackTime)
        {
            _owner.ChangeState(EStateType.Idle);
        }
    }
}