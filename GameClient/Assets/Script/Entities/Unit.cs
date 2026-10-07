using Core;
using System;
using System.Collections.Generic;
using UnityEngine;

public interface IComponentCache
{
    public void InitializeComponent(Unit owner);
}

public interface IAfterInit
{
    public void AfterInitialize();
}

public enum ETeamType
{
    Friendly,
    Enemy
}
 
public class Unit : MonoBehaviour
{
    public StateDataSO[] states;
    public UnitData UnitData { get; private set; }
    public bool IsEnemyUnit => TeamType == ETeamType.Enemy;
    public ETeamType TeamType { get; private set; }
    public EStateType CurrentState { get; private set; }

    private Dictionary<Type, IComponentCache> _cachedComponent = new();
    private UnitStateMachine _stateMachine;

    public virtual void Initialize(UnitData unitData, ETeamType teamType)
    { 
        UnitData = unitData;
        TeamType = teamType;
        InitializeComponentCaches();
        AfterInitializeComponentCaches();

        _stateMachine ??= new UnitStateMachine(this, states);
        ChangeState(EStateType.Move);
        gameObject.SetLayer(IsEnemyUnit ? GameDefine.EnemyLayer : GameDefine.UnitLayer);
        transform.localScale = new Vector3(IsEnemyUnit ? -1 : 1, 1, 1);
    }

    public void UpdateUnit()
    {

    }

    private void Update()
    {
        _stateMachine?.UpdateStateMachine();
    }


    public void ChangeState(EStateType stateType, bool forced = false)
    {
        CurrentState = stateType;
        _stateMachine.ChangeState(stateType, forced);
    }

    protected void InitializeComponentCaches()
    {
        var componentCaches = GetComponentsInChildren<IComponentCache>();
        foreach(var cache in componentCaches)
        {
            cache.InitializeComponent(this);
            _cachedComponent.Add(cache.GetType(), cache);
        }
    }

    private void AfterInitializeComponentCaches()
    {
        var afterInitComponents = GetComponentsInChildren<IAfterInit>();
        foreach (var cache in afterInitComponents)
        {
            cache.AfterInitialize();
        }
    }

    public T Get<T>() where T : IComponentCache
    {
        return (T)_cachedComponent.GetValueOrDefault(typeof(T));
    }

    public bool TryGet<T>(out T compo) where T : IComponentCache
    {
        compo = default;
        if (_cachedComponent.TryGetValue(typeof(T), out var cache))
        {
            compo = (T)cache;
            return true;
        }

        return false;
    }
}
