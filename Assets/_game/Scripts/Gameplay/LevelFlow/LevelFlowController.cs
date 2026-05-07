using System;
using VContainer.Unity;
using UnityEngine;

public class LevelFlowController : ILevelFlow, IStartable, IDisposable
{
    private readonly ILevelStateMachine _stateMachine;
    private readonly ISceneLoader _sceneLoader;
    private readonly ILevelProgressService _levelProgress;
    private readonly ILevelRewardService _levelReward;
    private readonly LevelFlowSettings _settings;
    private readonly Health _playerHealth;

    public LevelFlowController(
        ILevelStateMachine stateMachine,
        ISceneLoader sceneLoader,
        ILevelProgressService levelProgress,
        ILevelRewardService levelReward,
        LevelFlowSettings settings,
        Health playerHealth)
    {
        _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
        _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
        _levelProgress = levelProgress ?? throw new ArgumentNullException(nameof(levelProgress));
        _levelReward = levelReward ?? throw new ArgumentNullException(nameof(levelReward));
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        _playerHealth = playerHealth;
    }

    public event Action<LevelState> StateChanged
    {
        add => _stateMachine.StateChanged += value;
        remove => _stateMachine.StateChanged -= value;
    }

    public LevelState State => _stateMachine.State;
    public bool IsGameplayRunning => _stateMachine.IsGameplayRunning;
    public bool CanGoToNextLevel => _settings.HasNextLevel && _levelProgress.IsCompleted(_settings.LevelId);

    public void Start()
    {
        Time.timeScale = 1f;
        _stateMachine.StateChanged += OnStateChanged;

        if (_playerHealth != null)
            _playerHealth.Died += FailLevel;

        OnStateChanged(_stateMachine.State);
    }

    public void Dispose()
    {
        _stateMachine.StateChanged -= OnStateChanged;

        if (_playerHealth != null)
            _playerHealth.Died -= FailLevel;

        Time.timeScale = 1f;
    }

    public void StartLevel()
    {
        _stateMachine.Enter(LevelState.Playing);
    }

    public void Pause()
    {
        if (_stateMachine.State == LevelState.Playing)
            _stateMachine.Enter(LevelState.Paused);
    }

    public void Resume()
    {
        if (_stateMachine.State == LevelState.Paused)
            _stateMachine.Enter(LevelState.Playing);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        _sceneLoader.LoadScene(Constants.GameSceneName);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        _sceneLoader.LoadScene(Constants.MainMenuSceneName);
    }

    public void GoToNextLevel()
    {
        if (CanGoToNextLevel == false)
            return;

        Time.timeScale = 1f;
        _sceneLoader.LoadScene(_settings.NextLevelSceneName);
    }

    public void CompleteLevel()
    {
        if (_stateMachine.State != LevelState.Playing)
            return;

        _levelReward.GrantCompletionReward(_settings.LevelId);
        _levelProgress.MarkCompleted(_settings.LevelId);
        _stateMachine.Enter(LevelState.Victory);
    }

    public void FailLevel()
    {
        if (_stateMachine.State != LevelState.Playing)
            return;

        _stateMachine.Enter(LevelState.Defeat);
    }

    private static void OnStateChanged(LevelState state)
    {
        Time.timeScale = state == LevelState.Paused || state == LevelState.Victory || state == LevelState.Defeat
            ? 0f
            : 1f;
    }
}
