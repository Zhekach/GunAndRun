using UnityEngine;

public class PickupTrigger : MonoBehaviour
{
    private IPickable[] _pickables;

    private void Awake()
    {
        _pickables = GetComponents<IPickable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_pickables == null || _pickables.Length == 0)
            return;

        GameObject picker = other.GetComponentInParent<PlayerRunner>()?.gameObject;

        if (picker == null)
            return;

        for (int i = 0; i < _pickables.Length; i++)
        {
            if (_pickables[i] != null && _pickables[i].Pick(picker))
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}
