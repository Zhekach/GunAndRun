using System;
using VContainer.Unity;

public class MainMenuPresenter : IStartable, IDisposable
{
    private readonly MainMenuUI _mainMenuUI;
    private readonly ISceneLoader _sceneLoader;
    
    public MainMenuPresenter(MainMenuUI mainMenuUI, ISceneLoader sceneLoader)
    {
        _mainMenuUI = mainMenuUI;
        _sceneLoader = sceneLoader;
    }
    
    public void Start()
    {
        _mainMenuUI.PlayButton.onClick.AddListener(OnPlayButtonClick);
    }

    public void Dispose()
    {
        _mainMenuUI.PlayButton.onClick.RemoveListener(OnPlayButtonClick);
    }
    
    private void OnPlayButtonClick()
    {
        _sceneLoader.LoadScene(Constants.GameSceneName);
    }
}
