
using System;

public abstract class UnitState
{
    protected Unit _owner;
    protected AnimatorCompo _entityAnimator;
    protected AnimatorTriggerCompo _animatorTrigger;
    protected AttackCompo _attackCompo;
    protected MovementCompo _movementCompo;
    protected HealthCompo _healthCompo;

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
        _healthCompo = owner.Get<HealthCompo>();
    }

    public virtual void EnterState()
    {
        _entityAnimator.SetParam(_animationHash, true);
        _isTriggerCall = false;
        _animatorTrigger.OnAnimationEndTrigger += AnimationEndTrigger;
        _healthCompo.OnHitback += OnUnitHitback;
    }

    public virtual void UpdateState() { }

    public virtual void ExitState()
    {
        _animatorTrigger.OnAnimationEndTrigger -= AnimationEndTrigger;
        _healthCompo.OnHitback -= OnUnitHitback;

        _entityAnimator.SetParam(_animationHash, false);
    }

    private void OnUnitHitback()
    {
        _owner.ChangeState(EStateType.Knockback);
    }

    public virtual void AnimationEndTrigger() => _isTriggerCall = true;
}