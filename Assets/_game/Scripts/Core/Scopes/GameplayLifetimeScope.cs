using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayLifetimeScope : LifetimeScope
{
    [SerializeField] private LevelUI _levelUI;
    
    [SerializeField] private DebugOverlay _debugOverlay;
    [SerializeField] private DebugMoney _debugMoney;
    
    [SerializeField] private PlayerRunner _player;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private Weapon _playerWeapon;
    
    [Header("Level settings")]
    [SerializeField] private string _levelId = Constants.GameSceneName;
    [SerializeField] private string _nextLevelSceneName;
    [SerializeField, Min(0)] private int _completionMoneyReward;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(new LevelFlowSettings(_levelId, _nextLevelSceneName, _completionMoneyReward));
        builder.Register<ILevelRewardService, LevelRewardService>(Lifetime.Scoped);
        builder.Register<LevelStateMachine>(Lifetime.Scoped)
            .As<ILevelStateMachine>();
        builder.RegisterEntryPoint<LevelFlowController>();

        builder.RegisterComponent(_levelUI);
        builder.RegisterComponent(_debugOverlay);
        builder.RegisterComponent(_debugMoney);
        builder.RegisterComponent(_player);
        builder.RegisterComponent(_playerHealth);
        builder.RegisterComponent(_playerWeapon);
        builder.RegisterBuildCallback(InjectSceneComponents);
    }

    //Todo refactor По хорошему надо создать фабрику
    private void InjectSceneComponents(IObjectResolver resolver)
    {
        DropOnDeath[] dropOnDeathComponents = FindObjectsOfType<DropOnDeath>(true);

        for (int i = 0; i < dropOnDeathComponents.Length; i++)
            resolver.Inject(dropOnDeathComponents[i]);

        LevelFinishTrigger[] finishTriggers = FindObjectsOfType<LevelFinishTrigger>(true);

        for (int i = 0; i < finishTriggers.Length; i++)
            resolver.Inject(finishTriggers[i]);

        StatusEffectReceiver[] statusEffectReceivers = FindObjectsOfType<StatusEffectReceiver>(true);

        for (int i = 0; i < statusEffectReceivers.Length; i++)
            resolver.Inject(statusEffectReceivers[i]);
    }
}
