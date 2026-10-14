using MINLib.Script.Core.Resource;
using UnityEngine;

public class PVPUnit : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private UnitAbilityStatusUI abilityUI;

    private Vector3 _targetPosition;
    private EStateType _currentState;
    private ESpecialAbilityType _abilityStatus;
    private int _freezeApplyCount;
    private bool _isOpponent;

    public void InitializePVPUnit(RuntimeUnitData unitData, UnitSO unitSO, bool isOpponent)
    {
        transform.localScale = new Vector3(unitData.IsOpponent != isOpponent ? -1f : 1f, 1f, 1f);
        _targetPosition = new Vector3(isOpponent ? -unitData.PositionX : unitData.PositionX, unitData.PositionY, 0f);
        transform.position = _targetPosition;
        animator.runtimeAnimatorController = unitSO.animator;
        animator.speed = 1f;
        _isOpponent = isOpponent;
        _currentState = (EStateType)(-1);
        UpdateState(unitData.State);
        _abilityStatus = 0;
        _freezeApplyCount = unitData.FreezeApplyCount;
        abilityUI.SetAbilityStatusUI(ESpecialAbilityType.Weaken | ESpecialAbilityType.Freeze | ESpecialAbilityType.Slow, false);
        UpdateAbilityStatus(unitData.AbilityStatus, unitData.FreezeApplyCount);
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

    public void UpdateAbilityStatus(ESpecialAbilityType status, int freezeApplyCount)
    {
        ESpecialAbilityType changed = _abilityStatus ^ status;

        if((changed & ESpecialAbilityType.Weaken) != 0)
            abilityUI.SetAbilityStatusUI(ESpecialAbilityType.Weaken, (status & ESpecialAbilityType.Weaken) != 0);
        if((changed & ESpecialAbilityType.Freeze) != 0)
        {
            bool isFreeze = (status & ESpecialAbilityType.Freeze) != 0;
            animator.speed = isFreeze ? 0f : 1f;
            abilityUI.SetAbilityStatusUI(ESpecialAbilityType.Freeze, isFreeze);
        }
        else if((status & ESpecialAbilityType.Freeze) != 0 && _freezeApplyCount != freezeApplyCount)
            abilityUI.UpdateAbilityStatusUI(ESpecialAbilityType.Freeze);
        if((changed & ESpecialAbilityType.Slow) != 0)
            abilityUI.SetAbilityStatusUI(ESpecialAbilityType.Slow, (status & ESpecialAbilityType.Slow) != 0);

        _abilityStatus = status;
        _freezeApplyCount = freezeApplyCount;
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