using UnityEngine;

[RequireComponent(typeof(ContactDamage))]
[RequireComponent(typeof(DamageCollisionHandler))]
public class Projectile : MonoBehaviour
{
    [SerializeField, Min(0.01f)] private float _speed = 18f;
    [SerializeField, Min(0.01f)] private float _maxDistance = 30f;

    private ContactDamage _contactDamage;
    private Vector3 _direction = Vector3.forward;
    private float _traveledDistance;

    private void Awake()
    {
        _contactDamage = GetComponent<ContactDamage>();
        _contactDamage.DamageDealt += DestroySelf;
    }

    private void OnDestroy()
    {
        if (_contactDamage != null)
            _contactDamage.DamageDealt -= DestroySelf;
    }

    private void Update()
    {
        float distance = _speed * Time.deltaTime;

        transform.position += _direction * distance;
        _traveledDistance += distance;

        if (_traveledDistance >= _maxDistance)
            DestroySelf();
    }

    public void Initialize(Vector3 direction, float speed, int damage, GameObject owner, float maxDistance)
    {
        _direction = direction.normalized;
        _speed = speed;
        _maxDistance = maxDistance;
        _traveledDistance = 0f;
        _contactDamage.Initialize(damage, owner);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
