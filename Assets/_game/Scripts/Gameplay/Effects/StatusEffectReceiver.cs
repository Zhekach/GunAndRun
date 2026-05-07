using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class StatusEffectReceiver : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Weapon _weapon;

    private readonly List<ActiveStatusEffect> _activeEffects = new List<ActiveStatusEffect>();
    private ILevelStateProvider _levelState;

    public event Action<StatusEffectConfig> Applied;
    public event Action<StatusEffectConfig> Ended;

    [Inject]
    private void Construct(ILevelStateProvider levelState)
    {
        _levelState = levelState;
    }

    private void Awake()
    {
        if (_health == null)
            _health = GetComponentInChildren<Health>();

        if (_weapon == null)
            _weapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        if (_levelState != null && _levelState.IsGameplayRunning == false)
            return;

        for (int i = _activeEffects.Count - 1; i >= 0; i--)
        {
            ActiveStatusEffect effect = _activeEffects[i];
            effect.RemainingTime -= Time.deltaTime;

            if (effect.RemainingTime > 0f)
                continue;

            RevertTemporaryChanges(effect.Config);
            _activeEffects.RemoveAt(i);
            Ended?.Invoke(effect.Config);
        }
    }

    private void OnDisable()
    {
        for (int i = _activeEffects.Count - 1; i >= 0; i--)
            RevertTemporaryChanges(_activeEffects[i].Config);

        _activeEffects.Clear();
    }

    public void Apply(StatusEffectConfig config, GameObject source = null)
    {
        if (config == null)
            return;

        ApplyInstantChanges(config, source);

        if (HasTemporaryChanges(config) == false)
        {
            Applied?.Invoke(config);
            return;
        }

        if (config.Duration <= 0f)
        {
            ApplyPermanentChanges(config, source);
            Applied?.Invoke(config);
            return;
        }

        int activeEffectIndex = FindActiveEffectIndex(config);

        if (activeEffectIndex >= 0 && config.RefreshDurationOnReapply)
        {
            _activeEffects[activeEffectIndex].RemainingTime = config.Duration;
            Applied?.Invoke(config);
            return;
        }

        ApplyTemporaryChanges(config);
        _activeEffects.Add(new ActiveStatusEffect(config, config.Duration));
        Applied?.Invoke(config);
    }

    public void Apply(StatusEffectTarget target, float value, GameObject source = null)
    {
        ApplyChange(target, value, source);
    }

    private void ApplyInstantChanges(StatusEffectConfig config, GameObject source)
    {
        foreach (StatusEffectChange change in config.Changes)
        {
            if (change.IsTemporary)
                continue;

            ApplyChange(change, source);
        }
    }

    private void ApplyTemporaryChanges(StatusEffectConfig config)
    {
        foreach (StatusEffectChange change in config.Changes)
        {
            if (change.IsTemporary)
                ApplyChange(change, gameObject);
        }
    }

    private void ApplyPermanentChanges(StatusEffectConfig config, GameObject source)
    {
        foreach (StatusEffectChange change in config.Changes)
        {
            if (change.IsTemporary)
                ApplyChange(change, source);
        }
    }

    private void RevertTemporaryChanges(StatusEffectConfig config)
    {
        foreach (StatusEffectChange change in config.Changes)
        {
            if (change.IsTemporary)
                ApplyChange(change.Target, -change.Value, gameObject);
        }
    }

    private void ApplyChange(StatusEffectChange change, GameObject source)
    {
        ApplyChange(change.Target, change.Value, source);
    }

    private void ApplyChange(StatusEffectTarget target, float value, GameObject source)
    {
        switch (target)
        {
            case StatusEffectTarget.Health:
                ApplyHealthChange(value, source);
                break;

            case StatusEffectTarget.FireRateMultiplier:
                if (_weapon != null)
                    _weapon.AddFireRateMultiplierBonus(value);
                break;
        }
    }

    private void ApplyHealthChange(float value, GameObject source)
    {
        if (_health == null || Mathf.Approximately(value, 0f))
            return;

        int roundedValue = Mathf.RoundToInt(Mathf.Abs(value));

        if (value > 0f)
            _health.Heal(roundedValue);
        else
            _health.TakeDamage(roundedValue, source);
    }

    private int FindActiveEffectIndex(StatusEffectConfig config)
    {
        for (int i = 0; i < _activeEffects.Count; i++)
        {
            if (_activeEffects[i].Config == config)
                return i;
        }

        return -1;
    }

    private static bool HasTemporaryChanges(StatusEffectConfig config)
    {
        foreach (StatusEffectChange change in config.Changes)
        {
            if (change.IsTemporary)
                return true;
        }

        return false;
    }

    private class ActiveStatusEffect
    {
        public ActiveStatusEffect(StatusEffectConfig config, float remainingTime)
        {
            Config = config;
            RemainingTime = remainingTime;
        }

        public StatusEffectConfig Config { get; }
        public float RemainingTime { get; set; }
    }
}
