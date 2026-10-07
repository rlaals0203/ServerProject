using System.Collections.Generic;

public class BattleManager
{
    public List<Unit> allUnits;

    public void UpdateUnits()
    {
        foreach(var unit in allUnits)
        {
            unit.UpdateUnit();
        }
    }
}