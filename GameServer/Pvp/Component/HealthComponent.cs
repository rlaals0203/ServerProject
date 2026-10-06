public class HealthComponent : IDamageable
{
    private int _currentHealth;
    public int CurrentHealth => _currentHealth;

    public event Action<int> OnDamaged;
    public event Action<IDamageable> OnDead;

    public HealthComponent(UnitData unitData)
    {
        _currentHealth = unitData.UnitHealth;
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