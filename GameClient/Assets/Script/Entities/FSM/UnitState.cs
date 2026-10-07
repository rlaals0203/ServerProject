public abstract class UnitState
{
    protected Unit _entity;
    protected AnimatorCompo _entityAnimator;
    protected AnimatorTriggerCompo _animatorTrigger;
    protected int _animationHash;
    protected bool _isTriggerCall;

    protected UnitState(Unit entity, int animationHash)
    {
        _entity = entity;
        _animationHash = animationHash;
        _entityAnimator = entity.Get<AnimatorCompo>();
        _animatorTrigger = entity.Get<AnimatorTriggerCompo>();
    }

    public virtual void EnterState()
    {
        _entityAnimator.SetParam(_animationHash, true);
        _isTriggerCall = false;
        _animatorTrigger.OnAnimationEndTrigger += AnimationEndTrigger;
    }

    public virtual void UpdateState() { }

    public virtual void ExitState()
    {
        _animatorTrigger.OnAnimationEndTrigger -= AnimationEndTrigger;
        _entityAnimator.SetParam(_animationHash, false);
    }

    public virtual void AnimationEndTrigger() => _isTriggerCall = true;
}