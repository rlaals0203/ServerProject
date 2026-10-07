using System.Numerics;

public class RuntimeMoveComponent : RuntimeComponent, IRuntimeUnitCompo
{
    Vector3 Direction { get; set; }

    public void initialize(RuntimeUnit owner)
    {
        Direction = (owner.IsEnemyUnit ? 1 : -1) * new Vector3(1, 0, 0);
    }
}