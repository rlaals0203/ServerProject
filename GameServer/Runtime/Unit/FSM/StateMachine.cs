public enum EStateType
{
    Idle,
    Move,
    Attack,
    Hitback,
    Knockback
}

public class StateMachine
{
    public UnitState CurrentState { get; private set; }
    private Dictionary<EStateType, UnitState> _states = new();

    public StateMachine(RuntimeUnit owner)
    {
        _states.Add(EStateType.Idle, new UnitIdleState(owner));
        _states.Add(EStateType.Move, new UnitMoveState(owner));
        _states.Add(EStateType.Attack, new UnitAttackState(owner));
        _states.Add(EStateType.Knockback, new UnitKnockbackState(owner));
    }

    public void ChangeState(EStateType newStateType, bool forced = false)
    {
        UnitState newState = _states.GetValueOrDefault(newStateType);

        if (!forced && CurrentState == newState)
            return;

        CurrentState?.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }

    public void UpdateStateMachine(float dt)
    {
        CurrentState?.UpdateState(dt);
    }
}