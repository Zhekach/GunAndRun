using UnityEngine;
using VContainer;

public class LevelFinishTrigger : MonoBehaviour
{
    private ILevelFlow _levelFlow;

    [Inject]
    private void Construct(ILevelFlow levelFlow)
    {
        _levelFlow = levelFlow;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_levelFlow == null || _levelFlow.IsGameplayRunning == false)
            return;

        if (other.GetComponentInParent<PlayerRunner>() == null)
            return;

        _levelFlow.CompleteLevel();
    }
}
