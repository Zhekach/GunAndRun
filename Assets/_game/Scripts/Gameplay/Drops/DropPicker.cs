using UnityEngine;

public class DropPicker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PickableItem pickable = other.GetComponentInParent<PickableItem>();

        if (pickable == null)
            return;

        if (pickable.Pick(gameObject))
            Destroy(pickable.gameObject);
    }
}
