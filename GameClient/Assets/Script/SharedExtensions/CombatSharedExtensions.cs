using System;

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