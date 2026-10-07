public interface IDamageable
{
    bool IsDead { get; }
    void Hit(float damage, bool isCritical);
}