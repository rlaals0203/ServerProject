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

public struct GetUnitLevel
{
    public int Level;

    public GetUnitLevel(string unitId)
    {
        if (GameManager.I.User.UserUnitData.OwnUnitList.TryGetValue(unitId, out var unit) == false)
        {
            Level = -1;
            return;
        }

        Level = unit.UnitLevel;
    }
}