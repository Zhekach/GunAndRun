using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(ContactDamage))]
[RequireComponent(typeof(DamageCollisionHandler))]
public class DestructibleObstacle : DropOnDeath
{
}
