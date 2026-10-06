using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitSOList", menuName = "SO/UnitList")]
public class UnitSOList : ScriptableObject
{
    public List<UnitSO> UnitList;
    public Dictionary<string, UnitSO> UnitCache;
    public Dictionary<string, UnitData> UnitDataCache;

    [ContextMenu("CreateUnitCache")]
    public Dictionary<string, UnitSO> CreateUnitCache()
    {
        UnitCache = new();
        foreach (var unit in UnitList)
        {
            if(UnitCache.ContainsKey(unit.UnitId))
            {
                Debug.LogWarning($"duplication unit, UnitId: {unit.UnitId}, FileName: {name}");
                continue;
            }

            UnitCache.Add(unit.UnitId, unit);
        }

        return UnitCache;
    }

    [ContextMenu("CreateUnitDataCache")]
    public Dictionary<string, UnitData> CreateUnitDataCache()
    {
        UnitDataCache = new();
        foreach(var unit in UnitList)
        {
            if (UnitDataCache.ContainsKey(unit.UnitId))
            {
                Debug.LogWarning($"duplication unit, UnitId: {unit.UnitId}, FileName: {name}");
                continue;
            }

            UnitDataCache.Add(unit.UnitId, new MakeUnitData(unit).UnitData);
        }

        return UnitDataCache;
    }

    public UnitSO GetUnitById(string unitId)
    {
        UnitCache ??= CreateUnitCache();

        if (UnitCache.TryGetValue(unitId, out UnitSO unitData))
        {
            return unitData;
        }

        Debug.LogWarning($"{unitId} of Unit nout found");
        return null;
    }

    public UnitData GetUnitDataByid(string unitId)
    {
        UnitDataCache ??= CreateUnitDataCache();
        if (UnitDataCache.TryGetValue(unitId, out UnitData unitData))
        {
            return unitData;
        }

        Debug.LogWarning($"{unitId} of Unit nout found");
        return null;
    }
}
