using System.Numerics;

public class PvpUnit : ObjectBase
{
    public bool IsOpponent;
    public HealthComponent HealthComponent;
    private UnitData _unitData;

    public PvpUnit(UnitData unitData, bool isOpponent)
    {
        IsOpponent = isOpponent;
        HealthComponent = new HealthComponent(unitData);

        _unitData = unitData;
    }

    public override void Init()
    {
        base.Init();
    }

    public override void Update(float dt)
    {
        base.Update(dt);
        MoveUnit(dt);
    }

    private void MoveUnit(float dt)
    {
        int direction = IsOpponent ? -1 : 1;
        Position += new Vector3(_unitData.UnitMoveSpeed * direction * dt, 0, 0);
    }
}