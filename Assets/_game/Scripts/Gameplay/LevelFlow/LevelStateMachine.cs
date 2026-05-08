using System;

public class LevelStateMachine : ILevelStateMachine
{
    public event Action<LevelState> StateChanged;

    public LevelState State { get; private set; } = LevelState.Preparation;
    public bool IsGameplayRunning => State == LevelState.Playing || State == LevelState.BonusZone;

    public void Enter(LevelState state)
    {
        if (State == state)
            return;

        if (CanTransit(State, state) == false)
            throw new InvalidOperationException($"Level state transition from '{State}' to '{state}' is not allowed.");

        State = state;
        StateChanged?.Invoke(State);
    }

    private static bool CanTransit(LevelState from, LevelState to)
    {
        switch (from)
        {
            case LevelState.Preparation:
                return to == LevelState.Playing;

            case LevelState.Playing:
                return to == LevelState.Paused || to == LevelState.BonusZone || to == LevelState.Victory || to == LevelState.Defeat;

            case LevelState.BonusZone:
                return to == LevelState.Paused || to == LevelState.Victory;

            case LevelState.Paused:
                return to == LevelState.Playing || to == LevelState.BonusZone;

            case LevelState.Victory:
            case LevelState.Defeat:
                return false;

            default:
                return false;
        }
    }
}
