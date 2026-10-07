using System;
using UnityEngine;

public class StatCompo : MonoBehaviour, IComponentCache
{
    private Unit _owner;
    private StatData _statData;

    public void InitializeComponent(Unit owner)
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
