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

                bool isSucess = new GetRandomNumber(0f, 1f).Result < abilityData.Chance;
                if(isSucess)
                {
                    SucessAbilities.Add(abilityData, target);
                }
            }
        }    
    }
}

public struct GetModifiedStatByAbility
{
    public Dictionary<UnitData, List<ESpecialAbilityType>> SucessAbilities;     //(유닛 데이터, 최종 배율)

    public GetModifiedStatByAbility(UnitData[] targetList, UnitData attacker)
    {
        SucessAbilities = new();

        SpecialAbilityData[] abilityDatas = attacker.UnitSpecialAbilityData.SpecialAbilityDatas;

        foreach (var abilityData in abilityDatas)
        {
            foreach (var target in targetList)
            {
                if (target.CachedSpecialAbility.Contains(new GetOppositeAbility(abilityData.AbilityType).OppositeAbility))
                {
                    continue;
                }

                if (abilityData.AbilityType == ESpecialAbilityType.Weaken)
                {
                    AddSucessAbility(ESpecialAbilityType.Weaken, target);
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

    public void AddSucessAbility(ESpecialAbilityType abilityType, UnitData target)
    {
        if (SucessAbilities.TryGetValue(target, out var abilityList))
        {
            abilityList.Add(abilityType);
            return;
        }

        abilityList = new List<ESpecialAbilityType> { abilityType };
        SucessAbilities[target].Add(abilityType);
    }
}

public struct CheckHasAvailableAbility
{
    public bool HasAbility;

    public CheckHasAvailableAbility(SpecialAbilityData[] abilityData)
    {
        if (abilityData.Length == 0)
        {
            HasAbility = false;
            return;
        }

        foreach (var ability in abilityData)
        {
            if (ability.AbilityType == ESpecialAbilityType.SingleAttack ||
                ability.AbilityType == ESpecialAbilityType.AreaAttack ||
                ability.AbilityType == ESpecialAbilityType.LongDistance)
            {
                HasAbility = false;
                return;
            }
        }

        HasAbility = true;
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

public struct GetRandomNumber
{
    public float Result;

    public GetRandomNumber(float min, float max)
    {
        Random random = new Random();
        float randomNumber = random.Next(1, 101) / 100f;
        Result = randomNumber;
    }
}