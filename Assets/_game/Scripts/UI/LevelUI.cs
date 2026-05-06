using System;
using TMPro;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class LevelUI : MonoBehaviour, IStartable, IDisposable
{
    [SerializeField] private TMP_Text _moneyText;
    
    private IMoneyService _moneyService;

    [Inject]
    public void Construct(IMoneyService moneyService)
    {
        _moneyService = moneyService;
    }

    public void Start()
    {
        _moneyService.BalanceChanged += UpdateMoneyText;
        
        UpdateMoneyText(_moneyService.Balance);
    }

    public void Dispose()
    {
        _moneyService.BalanceChanged -= UpdateMoneyText;
    }

    private void UpdateMoneyText(int money)
    {
        _moneyText.text = money.ToString();
    }
}
