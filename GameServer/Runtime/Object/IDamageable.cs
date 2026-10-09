public interface IDamageable
{
    public void ApplyDamage(int damage);
    public event Action<int> OnDamaged;
    public event Action<IDamageable> OnDead;
}