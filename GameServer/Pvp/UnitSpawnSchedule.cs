public class UnitSpawnHandler
{
    private Dictionary<string, float> _spawnSchedule = new();
    private Dictionary<string, float> _opponentSpawnSchedule = new();
    private Dictionary<string, UnitData> _unitDatas = new();

    public UnitSpawnHandler(UserUnitData userUnitData, UserUnitData opponentUnitData)
    {
        foreach(var data in userUnitData.DeployUnitList)
        {
            if(string.IsNullOrEmpty(data.Key))
                continue;

            if(data.Value == null)
                continue;

            _spawnSchedule[data.Key] = 0f;
            _unitDatas.TryAdd(data.Key, data.Value);
        }

        foreach(var data in opponentUnitData.DeployUnitList)
        {
            if(string.IsNullOrEmpty(data.Key))
                continue;

            if(data.Value == null)
                continue;

            _opponentSpawnSchedule[data.Key] = 0f;
            _unitDatas.TryAdd(data.Key, data.Value);
        }
    }

    public void Update(float dt)
    {
        foreach(var kvp in _spawnSchedule)
        {
            _spawnSchedule[kvp.Key] -= dt;
        }

        foreach(var kvp in _spawnSchedule)
        {
            _opponentSpawnSchedule[kvp.Key] -= dt;
        }
    }

    public void SetSpawnStatus(string unitId, bool isOpponent)
    {
        if(_unitDatas.TryGetValue(unitId, out var unitData) == false)
            return;

        if(isOpponent)
            _opponentSpawnSchedule[unitId] = unitData.UnitCooldown;
        else
            _spawnSchedule[unitId] = unitData.UnitCooldown;
    }

    public bool CheckCanSpawn(string unitId, bool isOpponent)
    {
        if(isOpponent)
        {
            if(_opponentSpawnSchedule.TryGetValue(unitId, out var remainTime))
            {
                if(remainTime <= 0f)
                    return true;
            }
        }
        else
        {
            if(_spawnSchedule.TryGetValue(unitId, out var remainTime))
            {
                if(remainTime <= 0f)
                    return true;
            }
        }

        return false;
    }

    public Dictionary<string, float> GetRemainCooldown(bool isOpponent)
    {
        return isOpponent ? _opponentSpawnSchedule : _spawnSchedule;
    }
}