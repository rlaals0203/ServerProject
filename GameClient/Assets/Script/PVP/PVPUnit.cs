using System.ComponentModel;
using UnityEngine;

public class PVPUnit : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private Vector3 _targetPosition;
    private EStateType _currentState;
    private bool _isOpponent;

    public void InitializePVPUnit(RuntimeUnitData unitData, UnitSO unitSO, Transform parent, bool isOpponent)
    {
        transform.localScale = new Vector3(unitData.IsOpponent != isOpponent ? -1f : 1f, 1f, 1f);
        transform.position = new Vector3(isOpponent ? -unitData.PositionX : unitData.PositionX, unitData.PositionY, 0f);
        animator.runtimeAnimatorController = unitSO.animator;
        _isOpponent = isOpponent;
    }

    public void UpdateUnit(float dt, float posX, float posY)
    {
        _targetPosition = new Vector3(_isOpponent ? -posX : posX, posY, 0f);
        transform.position = Vector3.Lerp(transform.position, _targetPosition, Mathf.Min(1f, dt * 12f));
    }

    public void UpdateState(EStateType newState)
    {
        if (_currentState != newState)
        {
            ChanegState(newState);
        }
    }

    public void ChanegState(EStateType stateType)
    {
        _currentState = stateType;

        animator.SetBool("IDLE", _currentState == EStateType.Idle);
        animator.SetBool("MOVE", _currentState == EStateType.Move);
        animator.SetBool("ATTACK", _currentState == EStateType.Attack);
        animator.SetBool("KNOCKBACK", _currentState == EStateType.Knockback || _currentState == EStateType.Hitback);
    }

    public void Release()
    {

    }
}