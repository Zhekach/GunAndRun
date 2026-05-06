using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
        builder.Register<IMoneyService, MoneyService>(Lifetime.Singleton);
    }
}
