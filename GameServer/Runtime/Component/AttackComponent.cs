public class AttackComponent : ComponentBase
{
    private DamageCaster _damageCaster;
    private float _nextAttackTime;

    public AttackComponent(RuntimeUnit owner) : base(owner)
    {
        _owner = owner;
    }

    public override void InitializeComponent()
    {
        base.InitializeComponent();
        _damageCaster = new DamageCaster(_owner);
        _nextAttackTime = 0f;
    }

    public override void UpdateComponent(float dt)
    {
        base.UpdateComponent(dt);
        _nextAttackTime -= dt;
    }

    public bool CheckTarget()
    {
        return _damageCaster.DetectTarget();
    }

    public bool CanAttack() => _nextAttackTime <= 0f;

    public void StartAttack()
    {
        _nextAttackTime = _owner.UnitData.UnitAttackData.UnitAttackSpeed;
    }

    public void Attack()
    {
        if(_owner.IsDead)
            return;

        _damageCaster.CastDamage();
    }
}