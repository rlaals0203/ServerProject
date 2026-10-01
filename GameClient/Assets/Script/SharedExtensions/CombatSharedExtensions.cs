using System;
using System.Collections.Generic;
using System.Linq;

public struct CheckApplyAbility
{
    public Dictionary<SpecialAbilityData, UnitData> SucessAbilities;

    public CheckApplyAbility(UnitData[] targetList, UnitData attacker)
    {
        EUnitType[] targetTypes = attacker.UnitSpecialAbilityData.TargetTypes;
        SpecialAbilityData[] abilityDatas = attacker.UnitSpecialAbilityData.SpecialAbilityDatas;
        SucessAbilities = new();

        if (targetTypes.Length == 0 || new CheckHasAvailableAbility(abilityDatas).HasAbility == false)
        {
            return;
        }

        UnitData[] targets = targetList.Where(entity => targetTypes.Contains(entity.UnitType)).ToArray();

        foreach(var abilityData in abilityDatas)
        {
            foreach(var target in targets)
            {
                if(target.CachedSpecialAbility.Contains(new GetOppositeAbility(abilityData.AbilityType).OppositeAbility))
                {
                    continue;
                }

                bool isSucess = (RandomUtility.GetRandomNumber(0, 101) / 100f) < abilityData.Chance;
                if(isSucess)
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

        SpecialAbilityData[] abilityDatas = attacker.UnitSpecialAbilityData.SpecialAbilityDatas;
        UnitData[] targets = targetList.Where(entity => attacker.CachedTargetType.Contains(entity.UnitType)).ToArray();
        CheckSucessAbility(abilityDatas, targets);

        foreach(var kvp in SucessAbilities)
        {
            float finalDaamge = attacker.UnitAttackPower; //우선 기본 공격력으로 세팅

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

                if(kvp.Key.CachedTargetType.Contains(attacker.UnitType)) //상대가 내 속성에 대한 엄강, 맷집이 있는지 확인
                {
                    foreach(ESpecialAbilityType ability in kvp.Key.CachedSpecialAbility)
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


    private void CheckSucessAbility(SpecialAbilityData[] abilityDatas, UnitData[] targets)
    {
        foreach (var abilityData in abilityDatas)
        {
            foreach (var target in targets)
            {
                if (target.CachedSpecialAbility.Contains(new GetOppositeAbility(abilityData.AbilityType).OppositeAbility))
                {
                    continue;
                }

                if (abilityData.AbilityType == ESpecialAbilityType.Resistant)
                {
                    AddSucessAbility(ESpecialAbilityType.Resistant, target);
                }
                if (abilityData.AbilityType == ESpecialAbilityType.StrongAgainst)
                {
                    AddSucessAbility(ESpecialAbilityType.StrongAgainst, target);
                }
                if (abilityData.AbilityType == ESpecialAbilityType.MassiveDamage)
                {
                    AddSucessAbility(ESpecialAbilityType.MassiveDamage, target);
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
        switch (abilityType)
        {
            case ESpecialAbilityType.Weaken:
                OppositeAbility = ESpecialAbilityType.AntiWeaken;
                break;
            case ESpecialAbilityType.Freeze:
                OppositeAbility = ESpecialAbilityType.AntiFreeze;
                break;
            case ESpecialAbilityType.Slow:
                OppositeAbility = ESpecialAbilityType.AntiSlow;
                break;
            case ESpecialAbilityType.Knockback:
                OppositeAbility = ESpecialAbilityType.AntiKnockback;
                break;
            default:
                OppositeAbility = ESpecialAbilityType.None;
                break;
        }
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