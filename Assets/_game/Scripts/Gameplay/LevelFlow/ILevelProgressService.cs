using System;

public interface ILevelProgressService
{
    event Action<string> LevelCompleted;

    bool IsCompleted(string levelId);
    void MarkCompleted(string levelId);
}
