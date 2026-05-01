using VContainer.Unity;

public class GamePresenter : ITickable
{
    private readonly HelloWordService _helloWordService;

    public GamePresenter(HelloWordService helloWordService)
    {
        _helloWordService = helloWordService;
    }

    public void Tick()
    {
        _helloWordService.HelloWord();
    }
}
