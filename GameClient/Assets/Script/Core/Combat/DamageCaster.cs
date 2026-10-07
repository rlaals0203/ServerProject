using UnityEngine;
public class DamageCaster : MonoBehaviour
{
    [SerializeField] private ContactFilter2D contactFier;

    private bool isSingleAttack;
    private Unit _owner;
    private StatCompo _statCompo;
    private StatBase _attackRangeStat;
    private StatBase _attackDistanceStat;
    private StatBase _detectRangeStat;

    private Vector3 _direction;

    public virtual void InitCaster(Unit owner)
    {
        _owner = owner;
        _statCompo = owner.Get<StatCompo>();

        _attackRangeStat = _statCompo.GetStat(EStatType.AttackRange);
        _attackDistanceStat = _statCompo.GetStat(EStatType.AttackDistance);
        _detectRangeStat = _statCompo.GetStat(EStatType.DetectRange);

        _direction = (owner.TeamType == ETeamType.Enemy ? 1 : -1) * Vector3.right;
    }

    public bool CastDamage(Vector2 pos, UnitAttackData unitAttackData)
    {
        return true;
    }

    public bool DetectTarget()
    {
        return true;
    }

    private void Overlap()
    {

    }


#if UNITY_EDITOR
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 direction = (_owner.TeamType == ETeamType.Friendly ? -1 : 1) * Vector3.right;
        Vector3 attackPosition = transform.position + (direction * _attackDistanceStat.CurrentStat);
        Gizmos.DrawCube(attackPosition, new Vector3(_attackRangeStat.CurrentStat, 0.1f));

        Gizmos.color = Color.blue;
        Vector3 detectRange = new Vector3(_detectRangeStat.CurrentStat, 0.1f);
        Vector3 offset = new Vector3(detectRange.x * 0.5f * direction.x, 0.1f);
        detectRange.y -= 0.5f;
        Gizmos.DrawCube(transform.position + offset, new Vector3(_attackRangeStat.CurrentStat, 0.1f));
    }
#endif

    public Transform TargetTrm => _owner.transform;
}
