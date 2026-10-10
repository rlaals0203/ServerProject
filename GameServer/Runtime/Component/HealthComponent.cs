public class HealthComponent : ComponentBase
{
    private StatComponent _statCompo;
    private StatBase _healthStat;

    private int _damageStacked;
    private int _minHitbackHealth;

    public event Action OnDamaged;
    public event Action OnHitback;
    public event Action<RuntimeUnit> OnDead;

    public HealthComponent(RuntimeUnit owner) : base(owner) { }

    public override void InitializeComponent()
    {
        base.InitializeComponent();
        _statCompo = _owner.Get<StatComponent>();

        _healthStat = _statCompo.GetStat(EStatType.Health);
        _minHitbackHealth = (int)_healthStat.CurrentStat / _owner.UnitData.UnitHitbackCount;
    }

    public void ApplyDamage(float damage)
    {
        _healthStat.SubstractStatValue(damage);
        OnDamaged?.Invoke();

        _damageStacked += (int)damage;

        if(_damageStacked >= _minHitbackHealth)
        {
           _damageStacked = 0;
           OnHitback?.Invoke();
        }

        if (_healthStat.CurrentStat <= 0)
        {
            OnDead?.Invoke(_owner);
        }
    }

    public float GetCurrentHealth()
    {
        return _healthStat.CurrentStat;
    }
}