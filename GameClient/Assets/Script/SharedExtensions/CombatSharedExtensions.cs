using System;
using System.Collections.Generic;
using System.Linq;

public struct CheckApplyAbility
{
    public Dictionary<SpecialAbilityData, UnitData> SucessAbilities;

    public CheckApplyAbility(UnitData[] targetList, UnitData attacker)
    {
        SucessAbilities = new();

        EUnitType[] targetTypes = attacker.UnitSpecialAbilityData.TargetTypes;
        SpecialAbilityData[] abilityDatas = attacker.UnitSpecialAbilityData.SpecialAbilityDatas;
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

public struct GetOppositeAbility
{
    public ESpecialAbilityType OppositeAbility;

    public GetOppositeAbility(ESpecialAbilityType abilityType)
    {
        switch(abilityType)
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