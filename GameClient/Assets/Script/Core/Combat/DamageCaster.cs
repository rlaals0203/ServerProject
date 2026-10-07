using System.Linq;
using UnityEngine;
public class DamageCaster : MonoBehaviour
{
    [SerializeField] private ContactFilter2D contactFier;

    private bool _isSingleAttack;
    private Unit _owner;
    private StatCompo _statCompo;
    private StatBase _attackRangeStat;
    private StatBase _attackDistanceStat;
    private StatBase _detectRangeStat;

    private Vector3 _direction;

    private Collider2D[] _detectTargets = new Collider2D[16];

    public virtual void InitCaster(Unit owner)
    {
        _owner = owner;
        _statCompo = owner.Get<StatCompo>();

        _attackRangeStat = _statCompo.GetStat(EStatType.AttackRange);
        _attackDistanceStat = _statCompo.GetStat(EStatType.AttackDistance);
        _detectRangeStat = _statCompo.GetStat(EStatType.DetectRange);
        _direction = (_owner.TeamType == ETeamType.Friendly ? -1 : 1) * Vector3.right;
        _isSingleAttack = owner.UnitData.UnitSpecialAbilityData.SpecialAbilityDatas
            .Where(ability => ability.AbilityType == ESpecialAbilityType.SingleAttack).Count() > 0;

        contactFier.layerMask = owner.IsEnemyUnit ? LayerMask.GetMask("Unit") : LayerMask.GetMask("Enemy");
    }

    public bool CastDamage(Vector2 pos, UnitAttackData unitAttackData)
    {
        return true;
    }

    public bool DetectTarget()
    {
        Vector3 detectDistance = _direction * _detectRangeStat.CurrentStat;
        Vector3 detectPosition = transform.position + detectDistance * 0.5f;
        int count = Physics2D.OverlapBox(detectPosition, new Vector3(_detectRangeStat.CurrentStat, 2f), 0f, contactFier, _detectTargets);
        Debug.Log(count);
        return count > 0;
    }

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        if (_attackDistanceStat == null || _detectRangeStat == null || _attackRangeStat == null)
            return;

        Vector3 attackDistance = _direction * _attackDistanceStat.CurrentStat;
        Vector3 attackRange = _direction * _attackRangeStat.CurrentStat;
        Vector3 attackPosition = transform.position + attackDistance + attackRange * 0.5f;

        Vector3 detectDistance = _direction * _detectRangeStat.CurrentStat;
        Vector3 detectPosition = transform.position + detectDistance * 0.5f;
        detectPosition.y -= 0.02f;

        Gizmos.color = Color.red;
        Gizmos.DrawCube(attackPosition, new Vector3(_attackRangeStat.CurrentStat, 0.02f));

        Gizmos.color = Color.blue;
        Gizmos.DrawCube(detectPosition, new Vector3(_detectRangeStat.CurrentStat, 0.02f));
    }
#endif
}
