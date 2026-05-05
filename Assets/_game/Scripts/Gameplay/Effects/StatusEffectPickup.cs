using UnityEngine;

public class StatusEffectPickup : MonoBehaviour
{
    [SerializeField] private StatusEffectConfig _effect;

    private void OnTriggerEnter(Collider other)
    {
        if (_effect == null)
            return;

        StatusEffectReceiver receiver = other.GetComponentInParent<StatusEffectReceiver>();

        if (receiver == null)
            return;

        receiver.Apply(_effect, gameObject);

        Destroy(gameObject);
    }
}