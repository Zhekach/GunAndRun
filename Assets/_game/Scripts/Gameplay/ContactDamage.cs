using System;
using UnityEngine;

public class ContactDamage : MonoBehaviour, IDamageSource
{
    [SerializeField, Min(0)] private int _damage = 10;
    [SerializeField] private GameObject _owner;

    public event Action DamageDealt;

    public int Damage => _damage;

    public bool TryDamage(IDamageable target)
    {
        if (target == null || target.IsAlive == false || target.GameObject == gameObject || target.GameObject == _owner)
            return false;

        target.TakeDamage(_damage, gameObject);
        DamageDealt?.Invoke();
        return true;
    }

    public void Initialize(int damage, GameObject owner)
    {
        _damage = Mathf.Max(0, damage);
        _owner = owner;
    }
}
