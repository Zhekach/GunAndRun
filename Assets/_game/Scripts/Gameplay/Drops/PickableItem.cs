using UnityEngine;

public abstract class PickableItem : MonoBehaviour, IPickable
{
    public abstract bool Pick(GameObject picker);
}
