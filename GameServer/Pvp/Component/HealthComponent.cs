public class HealthComponent : ComponentBase, IDamageable
{
    private int _currentHealth;
    private StatComopnent _statCompo;
    public int CurrentHealth => _currentHealth;


    public event Action<int> OnDamaged;
    public event Action<IDamageable> OnDead;

    public HealthComponent(PvpUnit owner) : base(owner)
    {
        
    }

    public override void InitializeComponent()
    {
        base.InitializeComponent();
        _statCompo = Owner.Get<StatComopnent>();
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