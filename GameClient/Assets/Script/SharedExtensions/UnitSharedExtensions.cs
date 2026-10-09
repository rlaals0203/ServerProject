public struct GetUnitUpgradePrice
{
    public int Price;

    public GetUnitUpgradePrice(int startPrice, int currentLevel)
    {
        int level = currentLevel % 10;
        int unitStep = currentLevel / 10;
        Price = (int)GameDefine.UpgradeExpScale[unitStep] * level * startPrice;
    }
}

public class UpdateUnitData
{
    public UnitData UnitData;

    public UpdateUnitData(UnitStatData statData, int unitLevel)
    {
        float statMultiplier = GameDefine.UnitStatIncreasePerLevel * (unitLevel - 1);

        UnitAttackData attackData = new UnitAttackData
        {
            UnitAttackPower = (int)(statData.UnitAttackData.UnitAttackPower * statMultiplier),
            UnitAttackSpeed = statData.UnitAttackData.UnitAttackSpeed,
            UnitAttackRange = statData.UnitAttackData.UnitAttackRange,
            UnitAttackDistance = statData.UnitAttackData.UnitAttackDistance,
            UnitDetectRange = statData.UnitAttackData.UnitDetectRange,
        };

        UnitData unitData = new UnitData()
        {
            UnitStatData = statData,
            UnitLevel = unitLevel,
            UnitHealth = (int)(statData.UnitHealth * statMultiplier),
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

public struct GetUnitLevel
{
    public int Level;

    public GetUnitLevel(string unitId)
    {
        if (GameManager.I.MainUser.UserUnitData.OwnUnitList.TryGetValue(unitId, out var unit) == false)
        {
            Level = -1;
            return;
        }

        Level = unit.UnitLevel;
    }
}
