public class HealthComponent : ComponentBase, IDamageable
{
    private int _currentHealth;
    private StatComponent _statCompo;
    public int CurrentHealth => _currentHealth;

    public event Action<int> OnDamaged;
    public event Action<IDamageable> OnDead;

    public HealthComponent(RuntimeUnit owner) : base(owner) { }

    public override void InitializeComponent()
    {
        base.InitializeComponent();
        _statCompo = _owner.Get<StatComponent>();
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        OnDamaged?.Invoke(_currentHealth);

        if(_currentHealth <= 0)
        {
            OnDead?.Invoke(this);
        }
    }
}