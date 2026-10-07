public class UnitMoveState : UnitState
{
    private MovementCompo _movementCompo;

    public UnitMoveState(Unit entity, int animationHash) : base(entity, animationHash)
    {
        _movementCompo = entity.Get<MovementCompo>();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _movementCompo.MoveEntity();
    }
}