public class UnitDataDefine
{
    public Dictionary<string, UnitData> UnitCache;

    public Dictionary<string, UnitData> CreateUnitCache(List<UnitData> unitList)
    {
        UnitCache = new();
        foreach (var unit in unitList)
        {
            if(UnitCache.ContainsKey(unit.UnitId))
            {
                continue;
            }

            UnitCache.Add(unit.UnitId, unit);
        }

        return UnitCache;
    }

    public Dictionary<string, UnitData> GetUnitData()
    {
        return UnitCache;
    }

    public UnitData GetUnitById(string unitId)
    {
        if (UnitCache.TryGetValue(unitId, out UnitData unitData))
        {
            return unitData;
        }

        return null;
    }
}