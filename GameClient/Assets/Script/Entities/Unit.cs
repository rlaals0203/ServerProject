using System;
using System.Collections.Generic;
using UnityEngine;

public interface IComponentCache
{
    public void InitializeComponent(Unit owner);
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
    public ETeamType TeamType { get; private set; }
    public EStateType CurrentState { get; private set; }

    private Dictionary<Type, IComponentCache> _cachedComponent = new();
    private UnitStateMachine _stateMachine;

    public virtual void Initialize(UnitData unitData, ETeamType teamType)
    { 
        UnitData = unitData;
        TeamType = teamType;
        InitializeComponentCaches();

        _stateMachine ??= new UnitStateMachine(this, states);
        ChangeState(EStateType.Move);
    }

    private void Update()
    {
        _stateMachine?.UpdateStateMachine();
    }

    private void FixedUpdate()
    {
        _stateMachine?.FixedUpdateStateMachine();
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
