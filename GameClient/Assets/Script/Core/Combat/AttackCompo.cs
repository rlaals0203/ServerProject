using UnityEngine;
public class AttackCompo : MonoBehaviour, IComponentCache
{
    [SerializeField] private DamageCaster damageCaster;
    private IDamageable _target;
    private Unit _owner;
    private StatCompo _statCompo;

    private AnimatorTriggerCompo _animatorTrigger;

    public virtual void InitializeComponent(Unit owner)
    {
        _owner = owner;
        _animatorTrigger = owner.Get<AnimatorTriggerCompo>();
        _statCompo = owner.Get<StatCompo>();
        _animatorTrigger.OnDamageCastTrigger += Attack;

        damageCaster.InitCaster(owner);
    }

    private void OnDestroy()
    {
        if (_animatorTrigger != null)
            _animatorTrigger.OnDamageCastTrigger -= Attack;
    }

    public bool CheckTarget()
    {
        return damageCaster.DetectTarget();
    }

    public void Attack()
    {

    }

    public bool IsTargetDeadOrEmpty()
    {
        if (_target == null)
            return true;

        return false;
    }
}