using UnityEngine;

public interface IDamageable
{
    GameObject GameObject { get; }
    int CurrentHealth { get; }
    int MaxHealth { get; }
    bool IsAlive { get; }

    void TakeDamage(int damage, GameObject damageSource);
}
