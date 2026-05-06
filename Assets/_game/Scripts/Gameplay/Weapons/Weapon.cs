using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponConfig _config;
    [SerializeField] private Transform _shootPoint;
    [SerializeField, Min(0.01f)] private float _fireRateMultiplier = 1f;

    private float _cooldown;
    private float _fireRateMultiplierBonus;

    public WeaponConfig Config => _config;
    public float FireRateMultiplier => Mathf.Max(0.01f, _fireRateMultiplier + _fireRateMultiplierBonus);
    public float ShotsPerSecond => _config != null ? _config.ShotsPerSecond * FireRateMultiplier : 0f;
    public float Cooldown => Mathf.Max(0f, _cooldown);

    private void Awake()
    {
        if (_shootPoint == null)
            _shootPoint = transform;
    }

    private void Update()
    {
        if (_config == null || _config.ProjectilePrefab == null)
            return;

        _cooldown -= Time.deltaTime;

        if (_cooldown > 0f)
            return;

        Shoot();
        _cooldown = 1f / (_config.ShotsPerSecond * FireRateMultiplier);
    }

    public void SetConfig(WeaponConfig config)
    {
        _config = config;
        _cooldown = 0f;
    }

    public void SetFireRateMultiplier(float multiplier)
    {
        _fireRateMultiplier = Mathf.Max(0.01f, multiplier);
    }

    public void AddFireRateMultiplier(float value)
    {
        SetFireRateMultiplier(_fireRateMultiplier + value);
    }

    public void AddFireRateMultiplierBonus(float value)
    {
        _fireRateMultiplierBonus += value;
    }

    private void Shoot()
    {
        int projectilesCount = _config.ProjectilesPerShot;
        float spreadStep = projectilesCount > 1 ? _config.SpreadAngle / (projectilesCount - 1) : 0f;
        float startAngle = projectilesCount > 1 ? -_config.SpreadAngle * 0.5f : 0f;

        for (int i = 0; i < projectilesCount; i++)
        {
            Quaternion spreadRotation = Quaternion.AngleAxis(startAngle + spreadStep * i, Vector3.up);
            Vector3 direction = spreadRotation * _shootPoint.forward;
            Projectile projectile = Instantiate(_config.ProjectilePrefab, _shootPoint.position, Quaternion.LookRotation(direction));

            projectile.Initialize(direction, _config.ProjectileSpeed, _config.Damage, gameObject, _config.ProjectileDistance);
        }
    }
}
