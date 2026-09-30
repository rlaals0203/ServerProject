using System;
using UnityEngine;

public class StatCompo : MonoBehaviour, IComponentCache
{
    private Entity _owner;
    private StatData _statData;

    public void InitializeComponent(Entity owner)
    {
        _owner = owner;
        _statData = new StatData();
        InitializeStat();
    }

    private void InitializeStat()
    {

    }

    public StatBase GetStat(EStatType statType)
    {
        return _statData.GetStat(statType);
    }

    public double GetStatValue(EStatType statType)
    {
        return _statData.GetStatValue(statType);
    }

    public double GetStatValueWithScaled(EStatType statType, float scale)
    {
        return _statData.GetStatValueWithScaled(statType, scale);
    }

    public void AddStatModifier(EStatType statType, Type modifier, double value)
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

        targetStat.AddStatModifier(modifier, value);
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
}
