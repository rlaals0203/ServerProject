public interface IRuntimeUnitCompo
{
    public void initialize(RuntimeUnit unit);
}

public class RuntimeUnit : RuntimeObjectBase
{
    public bool IsEnemyUnit => TeamType == ETeamType.Enemy;
    public ETeamType TeamType { get; private set; }

    public override void Update()
    {
        base.Update();
    }
}