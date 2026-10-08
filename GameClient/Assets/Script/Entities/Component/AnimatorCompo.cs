using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorCompo : MonoBehaviour, IComponentCache
{
    [SerializeField] private UnitSOList unitSOList;
    [field: SerializeField] public Animator Animator { get; private set; }
    private Unit _owner;

    public virtual void InitializeComponent(Unit owner)
    {
        _owner = owner;
        RefreshAnimator(unitSOList.GetUnitById(owner.UnitData.UnitStatData.UnitId));
    }

    private void RefreshAnimator(UnitSO unitData)
    {
        Animator.runtimeAnimatorController = unitData.animator;
    }


    public void SetParam(string name, bool value) => Animator.SetBool(name, value);
    public void SetParam(int hash, float value, float dampTime) => Animator.SetFloat(hash, value, dampTime, Time.deltaTime);
    public void SetParam(int hash, float value) => Animator.SetFloat(hash, value);
    public void SetParam(int hash, int value) => Animator.SetInteger(hash, value);
    public void SetParam(int hash, bool value) => Animator.SetBool(hash, value);
    public void SetParam(int hash) => Animator.SetTrigger(hash);
    public void SetLayerWeight(int layerIndex, float weight) => Animator.SetLayerWeight(layerIndex, weight);
    public void SetAnimator(bool isOn)
    {
        Animator.speed = isOn ? 1 : 0;
    }
}
