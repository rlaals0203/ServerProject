using System;
using UnityEngine;

public class AnimatorTriggerCompo : MonoBehaviour, IComponentCache
{
    public Action OnAnimationEndTrigger;
    public Action OnDamageCastTrigger;
    public void InitializeComponent(Unit owner) { }

    private void AnimationEnd() => OnAnimationEndTrigger?.Invoke();
    private void DamageCast() => OnDamageCastTrigger?.Invoke();
}