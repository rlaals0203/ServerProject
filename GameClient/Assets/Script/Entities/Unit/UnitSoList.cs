using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitSOList", menuName = "SO/UnitList")]
public class UnitSOList : ScriptableObject
{
    public List<UnitSO> UnitList;
    private Dictionary<string, UnitSO> _unitCache;

    [ContextMenu("CreateUnitCache")]
    public Dictionary<string, UnitSO> CreateUnitCache()
    {
        _unitCache = new();
        foreach (var unit in UnitList)
        {
            if(_unitCache.ContainsKey(unit.UnitId))
            {
                Debug.LogWarning($"duplication unit, UnitId: {unit.UnitId}, FileName: {name}");
                continue;
            }

            _unitCache.Add(unit.UnitId, unit);
        }

        return _unitCache;
    }

    public Dictionary<string, UnitSO> GetUnitData()
    {
        _unitCache ??= CreateUnitCache();
        return _unitCache;
    }

    public UnitSO GetUnitById(string unitId)
    {
        _unitCache ??= CreateUnitCache();

        if (_unitCache.TryGetValue(unitId, out UnitSO unitData))
        {
            return unitData;
        }

        Debug.LogWarning($"{unitId} of Unit nout found");
        return null;
    }
}
