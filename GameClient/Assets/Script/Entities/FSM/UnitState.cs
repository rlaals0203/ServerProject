
public abstract class UnitState
{
    protected Unit _owner;
    protected AnimatorCompo _entityAnimator;
    protected AnimatorTriggerCompo _animatorTrigger;
    protected AttackCompo _attackCompo;
    protected MovementCompo _movementCompo;

    protected int _animationHash;
    protected bool _isTriggerCall;

    protected UnitState(Unit owner, int animationHash)
    {
        _owner = owner;
        _animationHash = animationHash;
        _entityAnimator = owner.Get<AnimatorCompo>();
        _animatorTrigger = owner.Get<AnimatorTriggerCompo>();
        _attackCompo = owner.Get<AttackCompo>();
        _movementCompo = owner.Get<MovementCompo>();
    }

    public virtual void EnterState()
    {
        _entityAnimator.SetParam(_animationHash, true);
        _isTriggerCall = false;
        _animatorTrigger.OnAnimationEndTrigger += AnimationEndTrigger;
    }

    public virtual void FixedUpdateState() { }
    public virtual void UpdateState() { }

    public virtual void ExitState()
    {
        _animatorTrigger.OnAnimationEndTrigger -= AnimationEndTrigger;
        _entityAnimator.SetParam(_animationHash, false);
    }

    public virtual void AnimationEndTrigger() => _isTriggerCall = true;
}