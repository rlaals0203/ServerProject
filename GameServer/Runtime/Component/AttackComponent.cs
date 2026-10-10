public class AttackComponent : ComponentBase
{
    private RuntimeUnit _owner;
    private DamageCaster _damageCaster;

    public AttackComponent(RuntimeUnit owner) : base(owner)
    {
        _owner = owner;
    }

    public override void InitializeComponent()
    {
        base.InitializeComponent();
        _damageCaster = new DamageCaster(_owner);
    }

    public bool CheckTarget()
    {
        return _damageCaster.DetectTarget();
    }

    public void Attack()
    {
        _damageCaster.CastDamage();
    }
}