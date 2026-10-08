using UnityEngine;
public class UnitKnockbackState : UnitState
{
    private const float knockbackTime = 0.6f;
    private float _currentTime;
    private BoxCollider2D _unitCollider;

    public UnitKnockbackState(Unit owner, int animationHash) : base(owner, animationHash)
    {
        _unitCollider = owner.GetComponent<BoxCollider2D>();
    }

    public override void EnterState()
    {
        base.EnterState();
        _currentTime = Time.time;
        _unitCollider.enabled = _owner.IsFreeze;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(Time.time - _currentTime >= knockbackTime)
        {
            _owner.ChangeState(EStateType.Idle);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        _unitCollider.enabled = true;
    }
}