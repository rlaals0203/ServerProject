using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorCompo : MonoBehaviour, IComponentCache
{
    [field: SerializeField] public Animator Animator { get; private set; }
    private Unit _entity;
    public virtual void InitializeComponent(Unit entity)
    {
        _entity = entity;
    }

    public void SetParam(string name, bool value) => Animator.SetBool(name, value);
    public void SetParam(int hash, float value, float dampTime) => Animator.SetFloat(hash, value, dampTime, Time.deltaTime);
    public void SetParam(int hash, float value) => Animator.SetFloat(hash, value);
    public void SetParam(int hash, int value) => Animator.SetInteger(hash, value);
    public void SetParam(int hash, bool value) => Animator.SetBool(hash, value);
    public void SetParam(int hash) => Animator.SetTrigger(hash);
    public void SetLayerWeight(int layerIndex, float weight) => Animator.SetLayerWeight(layerIndex, weight);
    public void OffAnimator()
    {
        Animator.enabled = false;
    }
}
