using System;
using UnityEngine;

[Serializable]
public class StatusEffectChange
{
    [SerializeField] private StatusEffectTarget _target;
    [SerializeField] private float _value;

    public StatusEffectTarget Target => _target;
    public float Value => _value;
    public bool IsTemporary => _target == StatusEffectTarget.FireRateMultiplier;
}
