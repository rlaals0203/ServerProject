using System.Linq;
using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    [SerializeField] private ContactFilter2D contactFier;

    private bool _isSingleAttack;
    private Unit _owner;

    private AbilityComponent _abilityCompo;
    private StatCompo _statCompo;
    private StatBase _attackPowerStat;
    private StatBase _attackRangeStat;
    private StatBase _attackDistanceStat;
    private StatBase _detectRangeStat;

    private Vector3 _direction;

    private Collider2D[] _detectTargets = new Collider2D[32];
    private Collider2D[] _attackTargets = new Collider2D[32];

    public virtual void InitCaster(Unit owner)
    {
        _owner = owner;
        _abilityCompo = owner.Get<AbilityComponent>();
        _statCompo = owner.Get<StatCompo>();

        _attackPowerStat = _statCompo.GetStat(EStatType.AttackPower);
        _attackRangeStat = _statCompo.GetStat(EStatType.AttackRange);
        _attackDistanceStat = _statCompo.GetStat(EStatType.AttackDistance);
        _detectRangeStat = _statCompo.GetStat(EStatType.DetectRange);

        _direction = (_owner.TeamType == ETeamType.Friendly ? -1 : 1) * Vector3.right;
        _isSingleAttack = owner.UnitData.UnitStatData.UnitSpecialAbilityData.SpecialAbilityDatas
            .Where(ability => ability.AbilityType == ESpecialAbilityType.SingleAttack).Count() > 0;

        contactFier.layerMask = owner.IsOpponent ? LayerMask.GetMask("Unit") : LayerMask.GetMask("Enemy");
    }

    public bool CastDamage()
    {
        Vector3 attackDistance = _direction * _attackDistanceStat.CurrentStat;
        Vector3 attackRange = _direction * _attackRangeStat.CurrentStat;
        Vector3 attackPosition = transform.position + attackDistance + attackRange * 0.5f;

        int count = Physics2D.OverlapBox(attackPosition, new Vector3(_attackRangeStat.CurrentStat, 2f), 0f, contactFier, _attackTargets);

        if (_isSingleAttack)
            count = 1;

        if (count > 0)
        {
            for(int i = 0; i < count; i++)
            {
                var target = _attackTargets[i];

                if (target == null)
                    continue;

                float finalDamage = _attackPowerStat.CurrentStat;

                if(target.TryGetComponent<Unit>(out var targetUnit))
                {
                    finalDamage = _abilityCompo.CheckAndApplySpecialAbilities(targetUnit, (int)_attackPowerStat.CurrentStat);
                }

                if (target.TryGetComponent<IDamageable>(out var damageable))
                {
                    damageable.OnHit(finalDamage);
                }
            }

            return true;
        }

        return false;
    }

    public bool DetectTarget()
    {
        Vector3 detectDistance = _direction * _detectRangeStat.CurrentStat;
        Vector3 detectPosition = transform.position + detectDistance * 0.5f;
        int count = Physics2D.OverlapBox(detectPosition, new Vector3(_detectRangeStat.CurrentStat, 2f), 0f, contactFier, _detectTargets);
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
