using UnityEngine;

public class ContactDamage : MonoBehaviour, IDamageSource
{
    [SerializeField, Min(0)] private int _damage = 10;

    public int Damage => _damage;

    public bool TryDamage(IDamageable target)
    {
        if (target == null || target.IsAlive == false || target.GameObject == gameObject)
            return false;

        target.TakeDamage(_damage, gameObject);
        return true;
    }
}
