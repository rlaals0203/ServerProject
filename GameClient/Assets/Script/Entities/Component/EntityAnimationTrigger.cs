using System;
using UnityEngine;

public class EntityAnimatorTrigger : MonoBehaviour, IComponentCache
{
    public Action OnAnimationEndTrigger;
    public Action OnAttackVFXTrigger;
    public Action<bool> OnManualRotationTrigger;
    public Action OnDamageCastTrigger;
    public Action<bool> OnDamageToggleTrigger;
    public Action OnCastSkillTrigger;
    private Entity _entity;
    public void InitializeComponent(Entity owner)
    {
        _entity = owner;
    }

    private void AnimationEnd()
    {
        OnAnimationEndTrigger?.Invoke();
    }

    private void CastSkill() => OnCastSkillTrigger?.Invoke();
    private void PlayAttackVFX() => OnAttackVFXTrigger?.Invoke();
    private void StartManualRotation() => OnManualRotationTrigger?.Invoke(true);
    private void StopManualRotation() => OnManualRotationTrigger?.Invoke(false);
    private void DamageCast() => OnDamageCastTrigger?.Invoke();
    private void StartDamageCast() => OnDamageToggleTrigger?.Invoke(true);
    private void StopDamageCast() => OnDamageToggleTrigger?.Invoke(false);
}