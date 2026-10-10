using System.Collections.Concurrent;
using System.Numerics;

public class RuntimeBattleField
{
    private Dictionary<ELayer, List<RuntimeUnit>> _units = new();
    private StageSpawner _stageSpawner;

    public RuntimeBattleField()
    {
        foreach (ELayer layer in Enum.GetValues<ELayer>())
        {
            _units[layer] = new List<RuntimeUnit>();
        }
    }

    public RuntimeBattleField(StageData stageData, UnitDataDefine unitDataDefine) : this()
    {
        if (stageData != null && unitDataDefine != null)
        {
            _stageSpawner = new StageSpawner(this, stageData, unitDataDefine);
        }
    }

    public void SpawnUnit(UnitData unitData, bool isOpponent, Vector3 position, ELayer layer)
    {
        RuntimeUnit newUnit = new RuntimeUnit(unitData, isOpponent, this, position, layer);
        newUnit.Init();
        
        _units[layer].Add(newUnit);
    }

    public void UpdateBattleField(float dt)
    {
        _stageSpawner?.Update(dt);

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

    public void OnEnemyCastleHit()
    {
        _stageSpawner?.OnEnemyCastleHit();
    }
}
