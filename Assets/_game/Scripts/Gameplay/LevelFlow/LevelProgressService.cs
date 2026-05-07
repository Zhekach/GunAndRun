using System;
using UnityEngine;

public class LevelProgressService : ILevelProgressService
{
    private const string CompletedLevelKeyPrefix = "LevelProgress.Completed.";

    public event Action<string> LevelCompleted;

    public bool IsCompleted(string levelId)
    {
        ValidateLevelId(levelId);

        return PlayerPrefs.GetInt(GetCompletedLevelKey(levelId), 0) == 1;
    }

    public void MarkCompleted(string levelId)
    {
        ValidateLevelId(levelId);

        if (IsCompleted(levelId))
            return;

        PlayerPrefs.SetInt(GetCompletedLevelKey(levelId), 1);
        PlayerPrefs.Save();
        LevelCompleted?.Invoke(levelId);
    }

    private static string GetCompletedLevelKey(string levelId)
    {
        return CompletedLevelKeyPrefix + levelId;
    }

    private static void ValidateLevelId(string levelId)
    {
        if (string.IsNullOrWhiteSpace(levelId))
            throw new ArgumentException("Level id can't be null or empty.", nameof(levelId));
    }
}
