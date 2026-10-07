using System.Collections.Generic;
using System;
using UnityEngine;

public class UnitStateMachine
{
    public UnitState CurrentState { get; private set; }

    private Dictionary<EStateType, UnitState> _states;

    public UnitStateMachine(Unit entity, StateDataSO[] stateList)
    {
        _states = new Dictionary<EStateType, UnitState>();
        foreach (StateDataSO state in stateList)
        {
            Type type = Type.GetType(state.className);
            Debug.Assert(type != null, $"Finding type is null : {state.className}");
            UnitState entityState = Activator.CreateInstance(type, entity, state.animationHash)
                                    as UnitState;
            _states.Add(state.stateType, entityState);
        }
    }

    public void ChangeState(EStateType newStateType, bool forced = false)
    {
        UnitState newState = _states.GetValueOrDefault(newStateType);
        Debug.Assert(newState != null, $"State is null {newStateType}");

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