using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MainMenuLifetimeScope : LifetimeScope
{
    [SerializeField] private MainMenuUI _mainMenuUI;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_mainMenuUI);
        builder.RegisterEntryPoint<MainMenuPresenter>();
    }
}
