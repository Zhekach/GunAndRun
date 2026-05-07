using System;

public interface ILevelStateProvider
{
    event Action<LevelState> StateChanged;

    LevelState State { get; }
    bool IsGameplayRunning { get; }
}
