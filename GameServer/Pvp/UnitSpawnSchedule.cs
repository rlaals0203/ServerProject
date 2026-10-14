public class UnitSpawnHandler
{
    private Dictionary<string, float> _spawnSchedule = new();
    private Dictionary<string, float> _opponentSpawnSchedule = new();

    public UnitSpawnHandler(UserUnitData userUnitData, UserUnitData opponentUnitData)
    {
        foreach(var data in userUnitData.DeployUnitList.Values)
        {
            if(data == null)
                continue;

            _spawnSchedule[data.UnitStatData.UnitId] = 0f;
        }

        foreach(var data in opponentUnitData.DeployUnitList.Values)
        {
            if(data == null)
                continue;

            _opponentSpawnSchedule[data.UnitStatData.UnitId] = 0f;
        }
    }

    public void Update(float dt)
    {
        foreach(var kvp in _spawnSchedule)
        {
            _spawnSchedule[kvp.Key] = MathF.Max(0f, kvp.Value - dt);
        }

        foreach(var kvp in _opponentSpawnSchedule)
        {
            _opponentSpawnSchedule[kvp.Key] = MathF.Max(0f, kvp.Value - dt);
        }
    }

    public void SetSpawnStatus(string unitId, float cooldown, bool isOpponent)
    {
        if(isOpponent)
            _opponentSpawnSchedule[unitId] = cooldown;
        else
            _spawnSchedule[unitId] = cooldown;
    }

    public bool CheckCanSpawn(string unitId, bool isOpponent)
    {
        var schedule = isOpponent ? _opponentSpawnSchedule : _spawnSchedule;
        return schedule.TryGetValue(unitId, out var remainTime) && remainTime <= 0f;
    }

    public Dictionary<string, float> GetRemainCooldown(bool isOpponent)
    {
        return isOpponent ? _opponentSpawnSchedule : _spawnSchedule;
    }
}