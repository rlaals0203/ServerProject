public class PvpBattleField
{
    private List<RuntimeUnit> units = new();

    public void SpawnUnit(UnitData unitData, bool isOpponent)
    {
        RuntimeUnit newUnit = new RuntimeUnit(unitData, isOpponent);
        units.Add(newUnit);
    }

    public void UpdateBattleField(float dt)
    {
        foreach(var unit in units)
        {
            unit?.Update(dt);
        }
    }
}