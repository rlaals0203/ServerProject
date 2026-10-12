using UnityEngine;

public class UnitAttackableState : UnitState
{
    public UnitAttackableState(Unit owner, int animationHash) : base(owner, animationHash)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(_attackCompo.CanAttack() && _attackCompo.CheckTarget())
        {
            _attackCompo.StartAttack(_owner.UnitData.UnitAttackData.UnitAttackSpeed);
            _owner.ChangeState(EStateType.Attack);
        }
    }
}
