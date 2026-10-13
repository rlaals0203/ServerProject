public struct GetUnitUpgradePrice
{
    public int Price;

    public GetUnitUpgradePrice(int startPrice, int currentLevel)
    {
        int step = currentLevel / 10;
        int level = (currentLevel % 10) + 1;

        if(step == 0)
        {
            level--;
        }

        if(step >= GameDefine.UpgradeExpScale.Count || GameDefine.UpgradeExpScale.Count < 0)
        {
            Price = int.MaxValue;
            return;
        }

        Price = (int)(GameDefine.UpgradeExpScale[step] * level * startPrice);
    }
}

public class UpdateUnitData
{
    public UnitData UnitData;

    public UpdateUnitData(UnitStatData statData, int unitLevel)
    {
        float statMultiplier = 1f + GameDefine.UnitStatIncreasePerLevel * (unitLevel - 1);
        
        UnitAttackData attackData = new UnitAttackData
        {
            UnitAttackPower = (int)(statData.UnitAttackData.UnitAttackPower * statMultiplier),
            UnitCastTime = statData.UnitAttackData.UnitCastTime,
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
