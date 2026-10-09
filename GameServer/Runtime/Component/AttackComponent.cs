public class AttackComponent : ComponentBase
{
    private RuntimeUnit _owner;
    private DamageCaster _damageCaster;

    public AttackComponent(RuntimeUnit owner) : base(owner)
    {
        _owner = owner;
        _damageCaster = new DamageCaster(owner);
    }

    public bool CheckTarget()
    {
        return _damageCaster.DetectTargets();
    }

    public void Attack()
    {
        _damageCaster.CastDamage();
    }
}