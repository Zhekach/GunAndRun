using System;
using UnityEngine;

public class MoneyService : IMoneyService
{
    private const string BalanceKey = "Money.Balance";

    private int _balance;

    public event Action<int> BalanceChanged;

    public int Balance => _balance;

    public MoneyService()
    {
        Load();
    }

    public void Add(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative.");

        if (amount == 0)
            return;

        SetBalance(_balance + amount);
        Save();
    }

    public bool CanSpend(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative.");

        return _balance >= amount;
    }

    public bool TrySpend(int amount)
    {
        if (CanSpend(amount) == false)
            return false;

        if (amount == 0)
            return true;

        SetBalance(_balance - amount);
        Save();

        return true;
    }

    public void Save()
    {
        PlayerPrefs.SetInt(BalanceKey, _balance);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        int savedBalance = Mathf.Max(0, PlayerPrefs.GetInt(BalanceKey, 0));
        SetBalance(savedBalance);
    }

    private void SetBalance(int balance)
    {
        if (_balance == balance)
            return;

        _balance = balance;
        BalanceChanged?.Invoke(_balance);
    }
}
