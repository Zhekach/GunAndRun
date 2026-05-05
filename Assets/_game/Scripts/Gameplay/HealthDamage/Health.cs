using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField, Min(1)] private int _maxHealth = 100;
    [SerializeField] private bool _destroyOnDeath = true;

    private int _currentHealth;
    private bool _isDead;

    public event Action<int, int> Changed;
    public event Action Died;

    public GameObject GameObject => gameObject;
    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;
    public bool IsAlive => _isDead == false;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage, GameObject damageSource)
    {
        if (_isDead || damage <= 0)
            return;

        _currentHealth = Mathf.Max(0, _currentHealth - damage);
        Changed?.Invoke(_currentHealth, _maxHealth);
        Debug.Log($"{name} Health: {_currentHealth} / {_maxHealth}");

        if (_currentHealth == 0)
            Die();
    }

    public void Heal(int value)
    {
        if (_isDead || value <= 0)
            return;

        _currentHealth = Mathf.Min(_maxHealth, _currentHealth + value);
        Changed?.Invoke(_currentHealth, _maxHealth);
    }

    public void ResetHealth()
    {
        _isDead = false;
        _currentHealth = _maxHealth;
        Changed?.Invoke(_currentHealth, _maxHealth);
    }

    private void Die()
    {
        _isDead = true;
        Died?.Invoke();

        if (_destroyOnDeath)
            Destroy(gameObject);
    }
}
