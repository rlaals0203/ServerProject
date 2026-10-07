using System.Collections.Generic;
using System;
using UnityEngine;

public class UnitStateMachine
{
    public UnitState CurrentState { get; private set; }

    private Dictionary<string, UnitState> _states;

    public UnitStateMachine(Unit entity, StateDataSO[] stateList)
    {
        _states = new Dictionary<string, UnitState>();
        foreach (StateDataSO state in stateList)
        {
            Type type = Type.GetType(state.className);
            Debug.Assert(type != null, $"Finding type is null : {state.className}");
            UnitState entityState = Activator.CreateInstance(type, entity, state.animationHash)
                                    as UnitState;
            _states.Add(state.stateName, entityState);
        }
    }

    public void ChangeState(string newStateName, bool forced = false)
    {
        UnitState newState = _states.GetValueOrDefault(newStateName);
        Debug.Assert(newState != null, $"State is null {newStateName}");

        if (!forced && CurrentState == newState)
            return;

        CurrentState?.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }

    public void UpdateStateMachine()
    {
        CurrentState?.UpdateState();
    }
}