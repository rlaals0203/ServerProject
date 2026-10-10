using System.Collections.Concurrent;
using System.Numerics;

public class RuntimeBattleField
{
    private Dictionary<ELayer, List<RuntimeUnit>> _units = new();

    public RuntimeBattleField()
    {
        foreach (ELayer layer in Enum.GetValues<ELayer>())
        {
            _units[layer] = new List<RuntimeUnit>();
        }
    }

    public void SpawnUnit(UnitData unitData, bool isOpponent, Vector3 position, ELayer layer)
    {
        RuntimeUnit newUnit = new RuntimeUnit(unitData, isOpponent, position, layer);
        newUnit.Init();
        
        _units[layer].Add(newUnit);
    }

    public void UpdateBattleField(float dt)
    {
        foreach(var unitList in _units.Values)
        {
            foreach(var unit in unitList)
            {
                unit?.Update(dt);
            }
        }
    }

    public List<RuntimeUnit> GetAllUnit(ELayer layer)
    {
        return _units[layer];
    }
}