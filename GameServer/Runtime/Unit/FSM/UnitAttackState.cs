public class UnitAttackState : UnitState
{
    private bool _isAttacked;
    private float _castTime;
    private float _attackAnimTime;

    public UnitAttackState(RuntimeUnit owner) : base(owner)
    {
        _owner = owner;
    }

    public override void EnterState()
    {
        base.EnterState();
        _castTime = _owner.UnitData.UnitAttackData.UnitCastTime;
        _attackAnimTime = _owner.UnitData.UnitAttackData.UnitAttackAnimTime;
        _isAttacked = false;
    }

    public override void UpdateState(float dt)
    {
        base.UpdateState(dt);
        _castTime -= dt;
        _attackAnimTime -= dt;
        
        if(_castTime <= 0 && _isAttacked == false)
        {
            _attackCompo.Attack();
            _isAttacked = true;
        }

        if(_attackAnimTime <= 0)
        {
            _owner.ChangeState(EStateType.Idle);
        }
    }
}