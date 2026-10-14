using MINLib.Script.Core.Resource;
using UnityEngine;

public class PVPUnit : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private Vector3 _targetPosition;
    private EStateType _currentState;
    private bool _isOpponent;

    public void InitializePVPUnit(RuntimeUnitData unitData, UnitSO unitSO, bool isOpponent)
    {
        transform.localScale = new Vector3(unitData.IsOpponent != isOpponent ? -1f : 1f, 1f, 1f);
        _targetPosition = new Vector3(isOpponent ? -unitData.PositionX : unitData.PositionX, unitData.PositionY, 0f);
        transform.position = _targetPosition;
        animator.runtimeAnimatorController = unitSO.animator;
        _isOpponent = isOpponent;
        _currentState = (EStateType)(-1);
        UpdateState(unitData.State);
    }

    public void UpdateUnit(float posX, float posY)
    {
        _targetPosition = new Vector3(_isOpponent ? -posX : posX, posY, 0f);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPosition, Mathf.Min(1f, Time.deltaTime * 12f));
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
        ResourceManager.I.DespawnPool(gameObject);
    }
}