using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[RequireComponent(typeof(Health))]
public class DropOnDeath : MonoBehaviour
{
    [SerializeField] private List<PickableItem> _dropPrefabs = new List<PickableItem>();

    private Health _health;
    private IObjectResolver _resolver;

    [Inject]
    private void Construct(IObjectResolver resolver)
    {
        _resolver = resolver;
    }

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
        PickableItem dropPrefab = GetRandomDropPrefab();

        if (dropPrefab == null)
            return;

        if (_resolver != null)
            _resolver.Instantiate(dropPrefab.gameObject, transform.position, dropPrefab.transform.rotation);
    }

    private PickableItem GetRandomDropPrefab()
    {
        int validPrefabsCount = 0;

        for (int i = 0; i < _dropPrefabs.Count; i++)
        {
            if (_dropPrefabs[i] != null)
                validPrefabsCount++;
        }

        if (validPrefabsCount == 0)
            return null;

        int selectedPrefabIndex = Random.Range(0, validPrefabsCount);

        for (int i = 0; i < _dropPrefabs.Count; i++)
        {
            PickableItem prefab = _dropPrefabs[i];

            if (prefab == null)
                continue;

            if (selectedPrefabIndex == 0)
                return prefab;

            selectedPrefabIndex--;
        }

        return null;
    }
}
