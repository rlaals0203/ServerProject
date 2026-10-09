public class StatComponent : ComponentBase
{
    private StatData _statData;

    public StatComponent(RuntimeUnit owner) : base(owner)
    {
        _statData = new StatData();
    }

    public override void InitializeComponent()
    {
        base.InitializeComponent();
        InitializeStat(_owner);
    }

    private void InitializeStat(RuntimeUnit unit)
    {
        if (unit == null)
            return;

        if(unit.UnitData == null)
            return;

        _statData.InitializeStat(unit.UnitData);
    }

    public StatBase GetStat(EStatType statType)
    {
        return _statData.GetStat(statType);
    }

    public float GetStatValue(EStatType statType)
    {
        return GetStat(statType).CurrentStat;
    }

    public float GetStatValueWithScaled(EStatType statType, float scale)
    {
        return GetStatValue(statType) * scale;
    }

    public void AddStatModifier(EStatType statType, string modifyKey, float value)
    {
        if (string.IsNullOrEmpty(modifyKey))
        {
            return;
        }

        StatBase targetStat = GetStat(statType);
        if (targetStat == null)
        {
            return;
        }

        targetStat.AddStatModifier(modifyKey, value);
    }

    public void ReplaceStatModifier(EStatType statType, string modifyKey, float value)
    {
        if (string.IsNullOrEmpty(modifyKey))
        {
            return;
        }

        StatBase targetStat = GetStat(statType);
        if (targetStat == null)
        {
            return;
        }

        targetStat.ReplaceStatModifier(modifyKey, value);
    }

    public void RemoveStatModifier(EStatType statType, string modifyKey)
    {
        if (string.IsNullOrEmpty(modifyKey))
        {
            return;
        }

        StatBase targetStat = GetStat(statType);
        if (targetStat == null)
        {
            return;
        }

        targetStat.RemoveStatModifier(modifyKey);
    }
}