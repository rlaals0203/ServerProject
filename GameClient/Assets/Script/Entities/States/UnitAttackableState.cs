using UnityEngine;

public class UnitAttackableState : UnitState
{
    private float _lastAttackTime;
    private float _attackInterval;

    public UnitAttackableState(Unit owner, int animationHash) : base(owner, animationHash)
    {
        _attackInterval = owner.UnitData.UnitAttackData.UnitAttackSpeed;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(Time.time - _lastAttackTime >= _attackInterval && _attackCompo.CheckTarget())
        {
            _owner.ChangeState(EStateType.Attack);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        _lastAttackTime = Time.time;
    }
}