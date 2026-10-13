using System.Collections.Generic;

public static class GameDefine
{
    public static int UnitLayer = 6;
    public static int EnemyLayer = 7;


    //유닛 관련
    public static int UnitMaxLevel = 30;
    public static float UnitStatIncreasePerLevel = 0.2f;
    public static int MaxSlotCount = 5;


    //패시브 관련
    public static int PassiveMaxLevel = 20;
    public static int PassiveStartPrice = 1000;

    public static float DefaultMoneyPerSecond = 15f;
    public static float AddictionalMoneyPerSecond = 5f;


    //특수능력 관련
    public static float WeakenModifyStat = 0.5f;
    public static float SlowModifyStat = 0.9f;
    public static float StrongAgainstAttackModifyStat = 2f;
    public static float StrongAgainstHealthModifyStat = 0.5f;
    public static float ResistantModifyStat = 4f;
    public static float MassiveDamageStat = 4f;


    public static Dictionary<int, float> UpgradeExpScale = new Dictionary<int, float>()
    {
        { 0, 1f },
        { 1, 1.2f },
        { 2, 1.4f },
    };
}
