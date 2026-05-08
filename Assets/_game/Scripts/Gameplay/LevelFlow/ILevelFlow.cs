public interface ILevelFlow : ILevelStateProvider
{
    bool CanGoToNextLevel { get; }

    void StartLevel();
    void Pause();
    void Resume();
    void Restart();
    void ExitToMainMenu();
    void GoToNextLevel();
    void EnterBonusZone();
    void CompleteLevel();
    void FailLevel();
}
