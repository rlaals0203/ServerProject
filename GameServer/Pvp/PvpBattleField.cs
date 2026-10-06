public class PvpBattleField
{
    private List<PvpUnit> units = new();

    public void SpawnUnit(UnitData unitData, bool isOpponent)
    {
        PvpUnit newUnit = new PvpUnit(unitData, isOpponent);
        newUnit.HealthComponent.OnDamaged += HandleDamageUnit;
        newUnit.HealthComponent.OnDead += HandleDeadUnit;
        units.Add(newUnit);
    }

    public void UpdateBattleField(float dt)
    {
        foreach(var unit in units)
        {
            unit?.Update(dt);
        }
    }

    private void HandleDeadUnit(IDamageable deadEntity)
    {
        deadEntity.OnDead -= HandleDeadUnit;
        deadEntity.OnDamaged -= HandleDamageUnit;
    }

    private void HandleDamageUnit(int unitHealth)
    {
        
    }
}