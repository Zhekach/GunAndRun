using UnityEngine;
using VContainer;

public class MoneyPickup : PickableItem
{
    [SerializeField, Min(1)] private int _amount = 1;

    private IMoneyService _moneyService;

    [Inject]
    private void Construct(IMoneyService moneyService)
    {
        _moneyService = moneyService;
    }

    public override bool Pick(GameObject picker)
    {
        if (_moneyService == null)
            return false;

        _moneyService.Add(_amount);
        return true;
    }
}
