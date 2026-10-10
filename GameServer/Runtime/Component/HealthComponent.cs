public class HealthComponent : ComponentBase
{
    private float _currentHealth;
    private StatComponent _statCompo;
    public float CurrentHealth => _currentHealth;
    public bool IsDead { get; private set; }

    public event Action<float> OnDamaged;
    public event Action<RuntimeUnit> OnDead;

    public HealthComponent(RuntimeUnit owner) : base(owner) { }

    public override void InitializeComponent()
    {
        base.InitializeComponent();
        _statCompo = _owner.Get<StatComponent>();
    }

    public void ApplyDamage(float damage)
    {
        _currentHealth -= damage;
        OnDamaged?.Invoke(_currentHealth);

        if(_currentHealth <= 0)
        {
            IsDead = true;
            OnDead?.Invoke(_owner);
        }
    }
}