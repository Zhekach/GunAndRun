using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayLifetimeScope : LifetimeScope
{
    [SerializeField] private DebugOverlay _debugOverlay;
    [SerializeField] private DebugMoney _debugMoney;
    [SerializeField] private PlayerRunner _player;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private Weapon _playerWeapon;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_debugOverlay);
        builder.RegisterComponent(_debugMoney);
        builder.RegisterComponent(_player);
        builder.RegisterComponent(_playerHealth);
        builder.RegisterComponent(_playerWeapon);
    }
}