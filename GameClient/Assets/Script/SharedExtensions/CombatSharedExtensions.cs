using Amazon.Runtime.Internal.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using static UnityEngine.GraphicsBuffer;

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

public struct GetModifiedStatByAbility
{
    public Dictionary<UnitData, List<SpecialAbilityData>> SucessAbilities;     //(유닛 데이터, 최종 배율)

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
                    AddSucessAbility(ESpecialAbilityType.Weaken, target, SucessAbilities);
                }
            }
        }
    }
    public void AddSucessAbility(ESpecialAbilityType abilityType, UnitData target, Dictionary<UnitData, List<SpecialAbilityData>> SucessAbilities)
    {
        if (SucessAbilities.TryGetValue(target, out var abilityList))
        {
            abilityList.Add(new SpecialAbilityData
            {
                AbilityType = abilityType
            });

            return;
        }

        SucessAbilities[target] = new List<SpecialAbilityData>();
        SucessAbilities[target].Add(new SpecialAbilityData
        {
            AbilityType = abilityType
        });
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