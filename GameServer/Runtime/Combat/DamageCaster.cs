public class DamageCaster
{
    private RuntimeUnit _owner;
    private RuntimeBattleField _battleField;
    private AbilityComponent _abilityCompo;
    private StatComponent _statCompo;
    private ELayer _targetLayer;

    private StatBase _attackPowerStat;
    private StatBase _attackRangeStat;
    private StatBase _attackDistStat;
    private StatBase _detectRangeStat;

    public DamageCaster(RuntimeUnit owner)
    {
        _owner = owner;
        _abilityCompo = owner.Get<AbilityComponent>();
        _statCompo = owner.Get<StatComponent>();
        _battleField = _owner.BattleField;

        _attackPowerStat = _statCompo.GetStat(EStatType.AttackPower);
        _attackRangeStat = _statCompo.GetStat(EStatType.AttackRange);
        _attackDistStat = _statCompo.GetStat(EStatType.AttackDistance);
        _detectRangeStat = _statCompo.GetStat(EStatType.DetectRange);

        _targetLayer = _owner.IsOpponent ? ELayer.Unit : ELayer.Enemy;
    }

    public void CastDamage()
    {
        var targets = _battleField.GetAllUnit(_targetLayer);

        foreach(var target in targets)
        {
            if (!IsInRange(target, false))
            {
                continue;
            }

            float finalDamage = _abilityCompo.CheckAndApplySpecialAbilities(target, _attackPowerStat.CurrentStat);

            if (target.TryGetComponent<HealthComponent>(out var healthCompo))
            {
                healthCompo.ApplyDamage(finalDamage);
            }
        }
    }

    public bool DetectTarget()
    {
        List<RuntimeUnit> targets = _battleField.GetAllUnit(_targetLayer);

        for (int i = 0; i < targets.Count; i++)
        {
            if (IsInRange(targets[i], true))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsInRange(RuntimeUnit target, bool isDetect)
    {
        float forward = _owner.IsOpponent ? 1f : -1f;
        float distance = (target.Position.X - _owner.Position.X) * forward;

        if (isDetect)
        {
            return distance >= 0f && distance <= _detectRangeStat.CurrentStat;
        }

        return distance >= _attackDistStat.CurrentStat &&
               distance <= _attackDistStat.CurrentStat + _attackRangeStat.CurrentStat;
    }
}