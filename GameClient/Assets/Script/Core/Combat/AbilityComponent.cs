using System.Linq;
using UnityEngine;

public class AbilityComponent : MonoBehaviour, IComponentCache
{
    private MovementCompo _movementCompo;
    private StatCompo _statCompo;
    private Unit _owner;


    public void InitializeComponent(Unit owner)
    {
        _owner = owner;
        _statCompo = owner.Get<StatCompo>();
        _movementCompo = owner.Get<MovementCompo>();
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
        if((abilityData.AbilityType & ESpecialAbilityType.Freeze) != 0)
        {
            FreezeUnit();
        }
    }

    private void FreezeUnit()
    {
        Debug.Log("freeze");
        _movementCompo.SetMove(false);
    }
}