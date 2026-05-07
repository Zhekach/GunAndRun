public class LevelFlowSettings
{
    public LevelFlowSettings(string levelId, string nextLevelSceneName, int completionMoneyReward)
    {
        LevelId = string.IsNullOrWhiteSpace(levelId) ? Constants.GameSceneName : levelId;
        NextLevelSceneName = nextLevelSceneName;
        CompletionMoneyReward = completionMoneyReward;
    }

    public string LevelId { get; }
    public string NextLevelSceneName { get; }
    public int CompletionMoneyReward { get; }
    public bool HasNextLevel => string.IsNullOrWhiteSpace(NextLevelSceneName) == false;
}
