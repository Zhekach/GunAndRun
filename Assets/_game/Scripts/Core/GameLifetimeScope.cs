using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
        
        builder.RegisterEntryPoint<Bootstrap>();
        //builder.Register<HelloWordService>(Lifetime.Singleton);
        //builder.RegisterEntryPoint<GamePresenter>();
    }
}
