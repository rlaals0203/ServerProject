using System;
using MINLib.Script.Core.Resource;
using UnityEngine;

public class PVPUnit : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private UnitAbilityStatusUI abilityUI;

    private Vector3 _targetPosition;
    private EStateType _currentState;
    private ESpecialAbilityType _abilityStatus;
    private PVPUnitHitback _hitback;
    private int _freezeApplyCount;
    private bool _isOpponent;

    public string RuntimeId { get; private set; }

    private void Awake()
    {
        _hitback = new PVPUnitHitback(this, animator.transform);
    }

    public void InitializePVPUnit(RuntimeUnitData unitData, UnitSO unitSO, bool isOpponent)
    {
        RuntimeId = unitData.RuntimeId;
        transform.localScale = new Vector3(unitData.IsOpponent != isOpponent ? -1f : 1f, 1f, 1f);
        _targetPosition = new Vector3(isOpponent ? -unitData.PositionX : unitData.PositionX, unitData.PositionY, 0f);
        transform.position = _targetPosition;
        animator.runtimeAnimatorController = unitSO.animator;
        animator.speed = 1f;
        _isOpponent = isOpponent;
        _freezeApplyCount = unitData.FreezeApplyCount;

        abilityUI.SetAbilityStatusUI(ESpecialAbilityType.Weaken | ESpecialAbilityType.Freeze | ESpecialAbilityType.Slow, false);
        _hitback.Initialize(unitData.HitbackCount);
        UpdateAbilityStatus(unitData.AbilityStatus, unitData.FreezeApplyCount);
        UpdateState(EStateType.Idle);
    }

    public void UpdateUnit(float posX, float posY)
    {
        _targetPosition = new Vector3(_isOpponent ? -posX : posX, posY, 0f);
    }

    private void Update()
    {
        float dt = Time.deltaTime;

        if (_hitback.IsDead == false)
            transform.position = Vector3.Lerp(transform.position, _targetPosition, Mathf.Min(1f, dt * 12f));

        _hitback.Update(dt, (_abilityStatus & ESpecialAbilityType.Freeze) != 0);
    }

    public void UpdateState(EStateType newState)
    {
        if (_currentState == newState)
            return;

        _currentState = newState;
        animator.SetBool("IDLE", _currentState == EStateType.Idle);
        animator.SetBool("MOVE", _currentState == EStateType.Move);
        animator.SetBool("ATTACK", _currentState == EStateType.Attack);
        animator.SetBool("KNOCKBACK", _currentState == EStateType.Knockback || _currentState == EStateType.Hitback);
    }

    public void UpdateHitback(int hitbackCount)
    {
        _hitback.UpdateHitback(hitbackCount);
    }

    public void PlayDeath(Action<PVPUnit> onComplete)
    {
        if (_hitback.IsDead)
            return;

        _hitback.PlayDeath(onComplete);
        animator.speed = 1f;
    }

    public void UpdateAbilityStatus(ESpecialAbilityType status, int freezeApplyCount)
    {
        ESpecialAbilityType changed = _abilityStatus ^ status;

        if ((changed & ESpecialAbilityType.Weaken) != 0)
        {
            abilityUI.SetAbilityStatusUI(ESpecialAbilityType.Weaken, (status & ESpecialAbilityType.Weaken) != 0);
        }

        if ((changed & ESpecialAbilityType.Freeze) != 0)
        {
            bool isFreeze = (status & ESpecialAbilityType.Freeze) != 0;
            animator.speed = isFreeze ? 0f : 1f;
            abilityUI.SetAbilityStatusUI(ESpecialAbilityType.Freeze, isFreeze);
        }
        else if ((status & ESpecialAbilityType.Freeze) != 0 && _freezeApplyCount != freezeApplyCount)
        {
            abilityUI.UpdateAbilityStatusUI(ESpecialAbilityType.Freeze);
        }
        if ((changed & ESpecialAbilityType.Slow) != 0)
        {
            abilityUI.SetAbilityStatusUI(ESpecialAbilityType.Slow, (status & ESpecialAbilityType.Slow) != 0);
        }

        _abilityStatus = status;
        _freezeApplyCount = freezeApplyCount;
    }

    public void Release()
    {
        _hitback.Release();
        ResourceManager.I.DespawnPool(gameObject);
    }
}