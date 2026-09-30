using System;
using System.Collections.Generic;

public class StatBase
{
    public float CurrentStat { get; private set; }
    public Dictionary<string, float> Modifiers { get; set; } = new();

    public void AddStatModifier(string modifyKey, float value)
    {
        if(Modifiers.ContainsKey(modifyKey))
        {
            return;
        }

        Modifiers.Add(modifyKey, value);
        CurrentStat += value;
    }

    public void ReplaceStatModifier(string modifyKey, float value)
    {
        if (Modifiers.TryGetValue(modifyKey, out float statValue))
        {
            Modifiers.Remove(modifyKey);
            CurrentStat -= statValue;
        }

        Modifiers.Add(modifyKey, value);
        CurrentStat += value;
    }

    public void RemoveStatModifier(string modifyKey)
    {
        if (Modifiers.TryGetValue(modifyKey, out float statValue))
        {
            Modifiers.Remove(modifyKey);
            CurrentStat -= statValue;
        }
    }

    public float GetStatFromModifier(string modifyKey)
    {
        return Modifiers.GetValueOrDefault(modifyKey);
    }
}

public delegate void OnStatChanged(EStatType statType, float after);

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
        if(string.IsNullOrEmpty(modifyKey))
        {
            return;
        }

        StatBase targetStat = GetStat(statType);
        if(targetStat == null)
        {
            return;
        }

        targetStat.AddStatModifier(modifyKey, value);
        OnStatChanged?.Invoke(statType, GetStatValue(statType));
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
        OnStatChanged?.Invoke(statType, GetStatValue(statType));
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
        OnStatChanged?.Invoke(statType, GetStatValue(statType));
    }
}