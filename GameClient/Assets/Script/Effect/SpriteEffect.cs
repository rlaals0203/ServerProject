using UnityEngine;

public class SpriteEffect : EffectBase
{
    [SerializeField] private Animator animator;

    public override void PlayEffect(Vector3 position, Quaternion rotation, float duration)
    {
        base.PlayEffect(position, rotation, duration);
        animator.Play("Effect");
    }

    public override void StopEffect()
    {

    }
}