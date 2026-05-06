using System;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

public class DebugMoney : MonoBehaviour, IStartable, IDisposable
{
    [SerializeField] private Button _addButton;
    [SerializeField] private Button _removeButton;
    [SerializeField] private int _moneyDelta;
    
    private IMoneyService _moneyService;

    [Inject]
    public void Construct(IMoneyService moneyService)
    {
        _moneyService = moneyService;
    }


    public void Start()
    {
        _addButton.onClick.AddListener(AddMoney);
        _removeButton.onClick.AddListener(RemoveMoney);
    }

    public void Dispose()
    {
        _addButton.onClick.RemoveListener(AddMoney);
        _removeButton.onClick.RemoveListener(RemoveMoney);
    }

    private void AddMoney()
    {
        _moneyService.Add(_moneyDelta);
    }

    private void RemoveMoney()
    {
        _moneyService.TrySpend(_moneyDelta);
    }
}
