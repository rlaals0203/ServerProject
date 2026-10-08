public class UpdateUnitData
{
    public UnitData UnitData;

    public UpdateUnitData(UnitStatData statData, int level, EUnitClassType classType = EUnitClassType.First)
    {
        UnitAttackData attackData = new UnitAttackData
        {
            UnitAttackPower = statData.UnitAttackData.UnitAttackPower,
            UnitAttackSpeed = statData.UnitAttackData.UnitAttackSpeed,
            AttackRange = statData.UnitAttackData.AttackRange,
            UnitAttackDistance = statData.UnitAttackData.UnitAttackDistance,
            UnitDetectRange = statData.UnitAttackData.UnitDetectRange,
        };

        UnitData unitData = new UnitData()
        {
            UnitStatData = statData,
            UnitLevel = level,
            UnitHealth = statData.UnitHealth,
            UnitCost = statData.UnitCost,
            UnitCooldown = statData.UnitCooldown,
            UnitMoveSpeed = statData.UnitMoveSpeed,
            UnitHitbackCount = statData.UnitHitbackCount,
            UnitDropMoneyAmount = statData.UnitDropMoneyAmount,
            UnitAttackData = attackData
        };

        UnitData = unitData;
    }
}