using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class AbilityComponent : MonoBehaviour, IComponentCache
{
    [SerializeField] private UnitAbilityStatusUI abilityUI;

    private MovementCompo _movementCompo;
    private AnimatorCompo _animatorCompo;
    private StatCompo _statCompo;
    private Unit _owner;

    private Dictionary<ESpecialAbilityType, AbilitySchedule> _abilitySchedule = new();


    public void InitializeComponent(Unit owner)
    {
        _owner = owner;
        _movementCompo = owner.Get<MovementCompo>();
        _animatorCompo = owner.Get<AnimatorCompo>();
        _statCompo = owner.Get<StatCompo>();

        _abilitySchedule.Add(ESpecialAbilityType.Weaken, new AbilitySchedule(ESpecialAbilityType.Weaken));
        _abilitySchedule.Add(ESpecialAbilityType.Freeze, new AbilitySchedule(ESpecialAbilityType.Freeze));
        _abilitySchedule.Add(ESpecialAbilityType.Slow, new AbilitySchedule(ESpecialAbilityType.Slow));

        foreach(var schedule in _abilitySchedule.Values)
        {
            schedule.OnStatusChange += HandleAbilityStatusChange;
        }
    }

    private void HandleAbilityStatusChange(ESpecialAbilityType abilityType, bool status)
    {
        if((abilityType & ESpecialAbilityType.Weaken) != 0)
        {
            ChangeWeakenStatus(status);
        }
        if ((abilityType & ESpecialAbilityType.Freeze) != 0)
        {
            ChangeFreezeStatus(status);
        }
        if ((abilityType & ESpecialAbilityType.Slow) != 0)
        {
            ChangeSlowStatus(status);
        }

        abilityUI.SetAbilityStatusUI(abilityType, status);
    }

    private void ChangeWeakenStatus(bool status)
    {
        if (status == true)
        {
            float modifyValue = GameDefine.WeakenModifyStat * _statCompo.GetStatValue(EStatType.AttackPower);
            _statCompo.AddStatModifier(EStatType.AttackPower, "weakenAbility", -modifyValue);
        }
        else
        {
            _statCompo.RemoveStatModifier(EStatType.AttackPower, "weakenAbility");
        }
    }

    private void ChangeFreezeStatus(bool status)
    {
        _animatorCompo.SetAnimator(!status);
        _movementCompo.SetMove(!status);
        _owner.SetFreeze(status);
    }

    private void ChangeSlowStatus(bool status)
    {
        if(status == true)
        {
            float modifyValue = GameDefine.SlowModifyStat * _statCompo.GetStatValue(EStatType.MoveSpeed);
            _statCompo.AddStatModifier(EStatType.MoveSpeed, "slowAbility", -modifyValue);
        }
        else
        {
            _statCompo.RemoveStatModifier(EStatType.MoveSpeed, "slowAbility");
        }
    }

    private void Update()
    {
        foreach(var schedule in _abilitySchedule.Values)
        {
            schedule.UpdateSchedule(Time.deltaTime);
        }
    }

    public float CheckAndApplySpecialAbilities(Unit target, float finalDamage)
    {
        UnitStatData ownerStat = _owner.UnitData.UnitStatData;
        UnitStatData targetStat = target.UnitData.UnitStatData;
        UnitSpecialAbilityData ownerAbilityStat = ownerStat.UnitSpecialAbilityData;
        UnitSpecialAbilityData targetAbilityStat = targetStat.UnitSpecialAbilityData;

        if(new CheckHasAvailableAbility(ownerAbilityStat.SpecialAbilityDatas).HasAbility == false)
        {
            return finalDamage; 
        }

        if (ownerAbilityStat.TargetTypes.Intersect(targetStat.UnitType).Count() == 0)
        {
            return finalDamage;
        }

        foreach (var abilityData in ownerAbilityStat.SpecialAbilityDatas)
        {
            if (targetStat.CachedSpecialAbility.Contains(new GetOppositeAbility(abilityData.AbilityType).OppositeAbility))
            {
                continue; //타겟이 무효 능력 있으면 스킵
            }

            ESpecialAbilityType attackerAbility = abilityData.AbilityType;

            if ((attackerAbility & ESpecialAbilityType.StrongAgainst) != 0)
            {
                finalDamage *= GameDefine.StrongAgainstAttackModifyStat;
            }
            if ((attackerAbility & ESpecialAbilityType.MassiveDamage) != 0)
            {
                finalDamage *= GameDefine.MassiveDamageStat;
            }
            if ((RandomUtility.GetRandomNumber(0, 101) / 100f) < abilityData.Chance)
            {
                target.Get<AbilityComponent>().ApplySpecialAbility(abilityData, _owner);
            }
        }

        foreach(var abilityData in targetAbilityStat.SpecialAbilityDatas) //타겟이 나에 대한 엄강, 맷집이 있는지 확인
        {
            ESpecialAbilityType targetAbility = abilityData.AbilityType;

            if ((targetAbility & ESpecialAbilityType.StrongAgainst) != 0)
            {
                finalDamage /= GameDefine.StrongAgainstAttackModifyStat;
            }
            if ((targetAbility & ESpecialAbilityType.Resistant) != 0)
            {
                finalDamage /= GameDefine.ResistantModifyStat;
            }
        }

        return finalDamage;
    }

    public void ApplySpecialAbility(SpecialAbilityData abilityData, Unit attacker)
    {
        if((abilityData.AbilityType & ESpecialAbilityType.Weaken) != 0)
        {
            _abilitySchedule[ESpecialAbilityType.Weaken].ModifyTime(abilityData.Duration);
        }
        if ((abilityData.AbilityType & ESpecialAbilityType.Freeze) != 0)
        {
            _abilitySchedule[ESpecialAbilityType.Freeze].ModifyTime(abilityData.Duration);
        }
        if ((abilityData.AbilityType & ESpecialAbilityType.Slow) != 0)
        {
            _abilitySchedule[ESpecialAbilityType.Slow].ModifyTime(abilityData.Duration);
        }
        if ((abilityData.AbilityType & ESpecialAbilityType.Knockback) != 0)
        {
            _movementCompo.PlayKnockback();
        }

        abilityUI.UpdateAbilityStatusUI(abilityData.AbilityType);
    }

    private void OnDestroy()
    {
        foreach (var timer in _abilitySchedule.Values)
        {
            timer.OnStatusChange -= HandleAbilityStatusChange;
        }
    }
}