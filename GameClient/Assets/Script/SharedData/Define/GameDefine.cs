using System.Collections.Generic;

public static class GameDefine
{
    public static int UnitLayer = 6;
    public static int EnemyLayer = 7;

    public static int UnitMaxLevel = 30;
    public static float UnitStatIncreasePerLevel = 0.2f;
    public static int PassiveMaxLevel = 20;
    public static int MaxSlotCount = 5;

    public static float WeakenModifyStat = 0.5f;
    public static float SlowModifyStat = 0.9f;
    public static float StrongAgainstAttackModifyStat = 2f;
    public static float StrongAgainstHealthModifyStat = 0.5f;
    public static float ResistantModifyStat = 4f;
    public static float MassiveDamageStat = 4f;

    public const float UnitStatGrowth20Percent = 0.20f;
    public const float UnitStatGrowth10Percent = 0.10f;
    public const float UnitStatGrowth5Percent = 0.05f;

    public static Dictionary<int, float> UpgradeExpScale = new Dictionary<int, float>()
    {
        { 0, 1f },
        { 1, 1.2f },
        { 2, 1.4f },
    };

    public static float GetUnitLevelStatMultiplier(EUnitGradeType gradeType, int level)
    {
        int unitLevel = System.Math.Max(1, level);
        float multiplier = 1f;

        for (int targetLevel = 2; targetLevel <= unitLevel; targetLevel++)
        {
            multiplier += GetUnitLevelStatGrowth(gradeType, targetLevel);
        }

        return multiplier;
    }

    private static float GetUnitLevelStatGrowth(EUnitGradeType gradeType, int level)
    {
        int firstGrowthEndLevel = gradeType == EUnitGradeType.Rare ? 70 : 60;
        int secondGrowthEndLevel = gradeType == EUnitGradeType.Rare ? 90 : gradeType == EUnitGradeType.Normal ? 110 : 80;

        if (level <= firstGrowthEndLevel)
        {
            return UnitStatGrowth20Percent;
        }

        return level <= secondGrowthEndLevel ? UnitStatGrowth10Percent : UnitStatGrowth5Percent;
    }
}
