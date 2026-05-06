using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(ContactDamage))]
[RequireComponent(typeof(DamageCollisionHandler))]
public class DestructibleObstacle : MonoBehaviour
{
    [SerializeField] private List<StatusEffectPickup> _buffPrefabs = new List<StatusEffectPickup>();

    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        if (_health != null)
            _health.Died += OnDied;
    }

    private void OnDisable()
    {
        if (_health != null)
            _health.Died -= OnDied;
    }

    private void OnDied()
    {
        StatusEffectPickup buffPrefab = GetRandomBuffPrefab();

        if (buffPrefab == null)
            return;

        Instantiate(buffPrefab, transform.position, buffPrefab.transform.rotation);
    }

    private StatusEffectPickup GetRandomBuffPrefab()
    {
        int validPrefabsCount = 0;

        for (int i = 0; i < _buffPrefabs.Count; i++)
        {
            if (_buffPrefabs[i] != null)
                validPrefabsCount++;
        }

        if (validPrefabsCount == 0)
            return null;

        int selectedPrefabIndex = Random.Range(0, validPrefabsCount);

        for (int i = 0; i < _buffPrefabs.Count; i++)
        {
            StatusEffectPickup prefab = _buffPrefabs[i];

            if (prefab == null)
                continue;

            if (selectedPrefabIndex == 0)
                return prefab;

            selectedPrefabIndex--;
        }

        return null;
    }
}
