public interface ILevelStateMachine : ILevelStateProvider
{
    void Enter(LevelState state);
}
