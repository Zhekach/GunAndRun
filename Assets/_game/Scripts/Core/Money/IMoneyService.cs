using System;

public interface IMoneyService
{
    event Action<int> BalanceChanged;

    int Balance { get; }

    void Add(int amount);
    bool CanSpend(int amount);
    bool TrySpend(int amount);
    void Save();
    void Load();
}
