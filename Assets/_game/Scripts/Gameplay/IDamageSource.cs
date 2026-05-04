public interface IDamageSource
{
    int Damage { get; }

    bool TryDamage(IDamageable target);
}
