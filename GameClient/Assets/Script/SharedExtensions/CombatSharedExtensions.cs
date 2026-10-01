using System;
using System.Collections.Generic;
using System.Linq;
using static UnityEngine.GraphicsBuffer;

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
                    finalDaamge *= GameDefine.StrongAgainstModifyStat;
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
                            finalDaamge /= GameDefine.StrongAgainstModifyStat;
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