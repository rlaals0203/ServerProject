using System;
using System.Collections.Generic;
using System.Linq;

public struct CheckApplyAbility
{
    public Dictionary<SpecialAbilityData, UnitData> SucessAbilities;

    public CheckApplyAbility(UnitData[] targetList, UnitData attacker)
    {
        EUnitType[] targetTypes = attacker.UnitStatData.UnitSpecialAbilityData.TargetTypes;
        SpecialAbilityData[] abilityDatas = attacker.UnitStatData.UnitSpecialAbilityData.SpecialAbilityDatas;
        SucessAbilities = new();

        if (targetTypes.Length == 0 || abilityDatas.Length == 0 || 
            new CheckHasAvailableAbility(abilityDatas).HasAbility == false)
        {
            return;
        }

        foreach(var abilityData in abilityDatas)
        {
            foreach(var target in targetList)
            {
                if(targetTypes.Intersect(target.UnitStatData.UnitType).Count() <= 0)
                {
                    continue; //대상 타겟이 아니면 스킵
                }

                if(target.UnitStatData.CachedSpecialAbility.Contains(new GetOppositeAbility(abilityData.AbilityType).OppositeAbility))
                {
                    continue; //타겟이 무효 능력 있으면 스킵
                }

                if((RandomUtility.GetRandomNumber(0, 101) / 100f) < abilityData.Chance)
                {
                    SucessAbilities.Add(abilityData, target);
                }
            }
        }    
    }
}

public struct CheckTargetWithAbility
{
    public Dictionary<UnitData, List<ESpecialAbilityType>> SucessAbilities;
    public Dictionary<UnitData, int> ApplyDamage;

    public CheckTargetWithAbility(UnitData[] targetList, UnitData attacker)
    {
        SucessAbilities = new();
        ApplyDamage = new();

        SpecialAbilityData[] abilityDatas = attacker.UnitStatData.UnitSpecialAbilityData.SpecialAbilityDatas;
        UnitData[] targets = targetList.Where(entity => entity.UnitStatData.UnitType.Intersect(entity.UnitStatData.UnitType).Any()).ToArray();
        CheckSucessAbility(abilityDatas, targets, attacker);

        foreach(var kvp in SucessAbilities)
        {
            float finalDaamge = attacker.UnitAttackData.UnitAttackPower; //우선 기본 공격력으로 세팅

            foreach(ESpecialAbilityType abilityType in kvp.Value)
            {
                if(abilityType == ESpecialAbilityType.StrongAgainst)
                {
                    finalDaamge *= GameDefine.StrongAgainstAttackModifyStat;
                }
                if(abilityType == ESpecialAbilityType.MassiveDamage)
                {
                    finalDaamge *= GameDefine.MassiveDamageStat;
                }

                if(attacker.UnitStatData.UnitType.Intersect(attacker.UnitStatData.UnitType).Any()) //상대가 내 속성에 대한 엄강, 맷집이 있는지 확인
                {
                    foreach(ESpecialAbilityType ability in kvp.Key.UnitStatData.CachedSpecialAbility)
                    {
                        if(ability == ESpecialAbilityType.StrongAgainst)
                        {
                            finalDaamge /= GameDefine.StrongAgainstHealthModifyStat;
                        }
                        if(ability == ESpecialAbilityType.Resistant)
                        {
                            finalDaamge /= GameDefine.ResistantModifyStat;
                        }
                    }
                }
            }

            ApplyDamage.Add(kvp.Key, (int)finalDaamge);
        }
    }


    private void CheckSucessAbility(SpecialAbilityData[] abilityDatas, UnitData[] targets, UnitData attacker)
    {
        foreach (var abilityData in abilityDatas)
        {
            foreach (var target in targets)
            {
                if (target.UnitStatData.CachedSpecialAbility.Contains(new GetOppositeAbility(abilityData.AbilityType).OppositeAbility))
                {
                    continue;
                }

                ESpecialAbilityType attackerAbility = abilityData.AbilityType;
                ESpecialAbilityType targetAbility = abilityData.AbilityType;

                if ((attackerAbility & ESpecialAbilityType.StrongAgainst) != 0)
                {
                    AddSucessAbility(ESpecialAbilityType.StrongAgainst, target);
                }

                if ((attackerAbility & ESpecialAbilityType.MassiveDamage) != 0)
                {
                    AddSucessAbility(ESpecialAbilityType.MassiveDamage, target);
                }

                float finalDamage = attacker.UnitAttackData.UnitAttackPower;

                if ((attackerAbility & ESpecialAbilityType.StrongAgainst) != 0)
                {
                    finalDamage *= GameDefine.StrongAgainstAttackModifyStat;
                }
                if ((attackerAbility & ESpecialAbilityType.MassiveDamage) != 0)
                {
                    finalDamage *= GameDefine.MassiveDamageStat;
                }
                if ((targetAbility & ESpecialAbilityType.StrongAgainst) != 0)
                {
                    finalDamage /= GameDefine.StrongAgainstHealthModifyStat;
                }
                if ((targetAbility & ESpecialAbilityType.Resistant) != 0)
                {
                    finalDamage /= GameDefine.ResistantModifyStat;
                }
            }
        }
    }

    private void AddSucessAbility(ESpecialAbilityType abilityType, UnitData target)
    {
        if (SucessAbilities.TryGetValue(target, out var abilityList))
        {
            abilityList.Add(abilityType);
            return;
        }

        abilityList = new List<ESpecialAbilityType> { abilityType };
        SucessAbilities.Add(target, abilityList);
    }
}

public struct CheckHasAvailableAbility
{
    public bool HasAbility;

    public CheckHasAvailableAbility(SpecialAbilityData[] abilityData)
    {
        HasAbility = false;

        if (abilityData.Length == 0)
        {
            return;
        }

        foreach (var ability in abilityData)
        {
            if (ability.AbilityType != ESpecialAbilityType.SingleAttack &&
                ability.AbilityType != ESpecialAbilityType.AreaAttack &&
                ability.AbilityType != ESpecialAbilityType.LongDistance)
            {
                HasAbility = true;
                return;
            }
        }
    }
}

public struct GetOppositeAbility
{
    public ESpecialAbilityType OppositeAbility;

    public GetOppositeAbility(ESpecialAbilityType abilityType)
    {
        OppositeAbility = abilityType switch
        {
            ESpecialAbilityType.Weaken => ESpecialAbilityType.AntiWeaken,
            ESpecialAbilityType.Freeze => ESpecialAbilityType.AntiFreeze,
            ESpecialAbilityType.Slow => ESpecialAbilityType.AntiSlow,
            ESpecialAbilityType.Knockback => ESpecialAbilityType.AntiKnockback,
            _ => ESpecialAbilityType.None
        };
    }
}

public static class RandomUtility
{
    private static readonly Random _random = new Random();

    public static float GetRandomNumber(int min, int max)
    {
        return _random.Next(min, max);
    }
}