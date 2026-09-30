using System;
using System.Collections.Generic;

public class StatBase
{
    public double CurrentStat { get; private set; }
    public Dictionary<Type, double> Modifiers { get; set; } = new();

    public void AddStatModifier(Type modifier, double value)
    {
        if(Modifiers.ContainsKey(modifier))
        {
            return;
        }

        Modifiers.Add(modifier, value);
        CurrentStat += value;
    }

    public void ReplaceStatModifier(Type modifier, double value)
    {
        RemoveStatModifier(modifier);
        AddStatModifier(modifier, value);
    }

    public void RemoveStatModifier(Type modifier)
    {
        if (Modifiers.TryGetValue(modifier, out double statValue))
        {
            Modifiers.Remove(modifier);
            CurrentStat -= statValue;
        }
    }

    public double GetStatFromModifier(Type modifier)
    {
        return Modifiers.GetValueOrDefault(modifier);
    }

    public void ModifyStatWithoutModifier(double value)
    {
        CurrentStat -= value;
    }
}

public delegate void OnStatChanged(EStatType statType, double after);

public class StatData
{
    public Dictionary<EStatType, StatBase> _statDatas = new();
    public event OnStatChanged OnStatChanged;

    public StatData()
    {
        foreach(EStatType statType in Enum.GetValues(typeof(EStatType)))
        {
            _statDatas.Add(statType, new StatBase());
        }
    }

    public StatBase GetStat(EStatType statType)
    {
        return _statDatas.GetValueOrDefault(statType);
    }

    public double GetStatValue(EStatType statType)
    {
        return GetStat(statType).CurrentStat;
    }

    public double GetStatValueWithScaled(EStatType statType, float scale)
    {
        return GetStatValue(statType) * scale;
    }

    public void AddStatModifier(EStatType statType, Type modifier, double value)
    {
        if(modifier == null || value == 0)
        {
            return;
        }

        StatBase targetStat = GetStat(statType);
        if(targetStat == null)
        {
            return;
        }

        targetStat.AddStatModifier(modifier, value);
        OnStatChanged?.Invoke(statType, value);
    }

    public void ReplaceStatModifier(EStatType statType, Type modifier, double value)
    {
        if (modifier == null || value == 0)
        {
            return;
        }

        StatBase targetStat = GetStat(statType);
        if (targetStat == null)
        {
            return;
        }

        targetStat.ReplaceStatModifier(modifier, value);
        OnStatChanged?.Invoke(statType, value);
    }

    public void RemoveStatModifier(EStatType statType, Type modifier)
    {
        if (modifier == null)
        {
            return;
        }

        StatBase targetStat = GetStat(statType);
        if (targetStat == null)
        {
            return;
        }

        targetStat.RemoveStatModifier(modifier);
    }

    public void ModifyStatWithoutModifier(EStatType statType, double value)
    {
        GetStat(statType).ModifyStatWithoutModifier(value);
    }
}