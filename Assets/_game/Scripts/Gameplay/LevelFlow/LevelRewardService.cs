using System;

public class LevelRewardService : ILevelRewardService
{
    private readonly IMoneyService _moneyService;
    private readonly LevelFlowSettings _settings;

    public LevelRewardService(IMoneyService moneyService, LevelFlowSettings settings)
    {
        _moneyService = moneyService ?? throw new ArgumentNullException(nameof(moneyService));
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }

    public void GrantCompletionReward(string levelId)
    {
        if (string.IsNullOrWhiteSpace(levelId))
            throw new ArgumentException("Level id can't be null or empty.", nameof(levelId));

        if (_settings.CompletionMoneyReward > 0)
            _moneyService.Add(_settings.CompletionMoneyReward);
    }
}
