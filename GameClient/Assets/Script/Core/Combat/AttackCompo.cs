using UnityEngine;
public abstract class AttackCompo : MonoBehaviour, IComponentCache
{
    [SerializeField] protected float _detectRange;
    protected IDamageable _target;
    protected Unit _entity;
    private AnimatorTriggerCompo _animatorTrigger;

    public virtual void InitializeComponent(Unit entity)
    {
        _entity = entity;
        _animatorTrigger = entity.Get<AnimatorTriggerCompo>();
        _animatorTrigger.OnDamageCastTrigger += Attack;
    }
    private void OnDestroy()
    {
        _animatorTrigger.OnDamageCastTrigger -= Attack;
    }

    public abstract void Attack();

    public bool IsTargetDeadOrEmpty()
    {
        if (_target == null)
            return true;
        if (_target.IsDead)
        {
            _target = null;
            return true;
        }
        return false;
    }

#if UNITY_EDITOR
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectRange);
    }
#endif
}