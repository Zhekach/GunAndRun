using UnityEngine;

[CreateAssetMenu(menuName = "Gun And Run/Weapon Config", fileName = "WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField, Min(1)] private int _damage = 10;
    [SerializeField, Min(0.01f)] private float _shotsPerSecond = 2f;
    [SerializeField, Min(0.01f)] private float _projectileSpeed = 18f;
    [SerializeField, Min(0.01f)] private float _projectileDistance = 30f;
    [SerializeField, Min(1)] private int _projectilesPerShot = 1;
    [SerializeField, Min(0f)] private float _spreadAngle = 0f;

    public Projectile ProjectilePrefab => _projectilePrefab;
    public int Damage => _damage;
    public float ShotsPerSecond => _shotsPerSecond;
    public float ProjectileSpeed => _projectileSpeed;
    public float ProjectileDistance => _projectileDistance;
    public int ProjectilesPerShot => _projectilesPerShot;
    public float SpreadAngle => _spreadAngle;
}
