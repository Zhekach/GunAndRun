using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gun And Run/Status Effect", fileName = "StatusEffect")]
public class StatusEffectConfig : ScriptableObject
{
    [SerializeField] private string _displayName;
    [SerializeField, Min(0f)] private float _duration = 5f;
    [SerializeField] private bool _refreshDurationOnReapply = true;
    [SerializeField] private StatusEffectChange[] _changes;

    public string DisplayName => string.IsNullOrWhiteSpace(_displayName) ? name : _displayName;
    public float Duration => _duration;
    public bool RefreshDurationOnReapply => _refreshDurationOnReapply;
    public IReadOnlyList<StatusEffectChange> Changes => _changes ?? Array.Empty<StatusEffectChange>();
}
