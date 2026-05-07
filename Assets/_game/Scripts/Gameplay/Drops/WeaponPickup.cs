using UnityEngine;

public class WeaponPickup : PickableItem
{
    [SerializeField] private WeaponConfig _config;

    public WeaponConfig Config => _config;

    public void SetConfig(WeaponConfig config)
    {
        _config = config;
    }

    public override bool Pick(GameObject picker)
    {
        if (_config == null || picker == null)
            return false;

        Weapon weapon = FindWeapon(picker);

        if (weapon == null)
            return false;

        weapon.SetConfig(_config);
        return true;
    }

    private static Weapon FindWeapon(GameObject picker)
    {
        Weapon weapon = picker.GetComponentInParent<Weapon>();

        if (weapon != null)
            return weapon;

        weapon = picker.GetComponentInChildren<Weapon>();

        if (weapon != null)
            return weapon;

        PlayerRunner player = picker.GetComponentInParent<PlayerRunner>();

        if (player != null)
            return player.GetComponentInChildren<Weapon>();

        return null;
    }
}
