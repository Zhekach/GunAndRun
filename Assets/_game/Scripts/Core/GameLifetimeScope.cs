using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<Bootstrap>();
        
        builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
        //builder.Register<HelloWordService>(Lifetime.Singleton);
        //builder.RegisterEntryPoint<GamePresenter>();
    }
}
