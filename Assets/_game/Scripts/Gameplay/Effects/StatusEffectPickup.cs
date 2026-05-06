using UnityEngine;

public class StatusEffectPickup : PickableItem
{
    [SerializeField] private StatusEffectConfig _effect;

    public override bool Pick(GameObject picker)
    {
        if (_effect == null)
            return false;

        StatusEffectReceiver receiver = picker.GetComponentInParent<StatusEffectReceiver>();

        if (receiver == null)
            return false;

        receiver.Apply(_effect, gameObject);
        return true;
    }
}
