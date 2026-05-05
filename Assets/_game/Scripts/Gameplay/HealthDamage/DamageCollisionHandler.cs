using System.Collections.Generic;
using UnityEngine;

public class DamageCollisionHandler : MonoBehaviour
{
    private static readonly HashSet<DamagePair> _handledPairs = new HashSet<DamagePair>();
    private static int _handledFrame = -1;

    private IDamageSource _damageSource;
    private IDamageable _damageable;

    private void Awake()
    {
        _damageSource = GetComponent<IDamageSource>();
        _damageable = GetComponent<IDamageable>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        HandleContact(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleContact(other.gameObject);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        HandleContact(hit.gameObject);
    }

    private void HandleContact(GameObject other)
    {
        if (other == null || other == gameObject)
            return;

        ResetHandledPairsIfNeeded();

        IDamageable otherDamageable = other.GetComponentInParent<IDamageable>();
        IDamageSource otherDamageSource = other.GetComponentInParent<IDamageSource>();

        TryApplyDamage(_damageSource, otherDamageable);
        TryApplyDamage(otherDamageSource, _damageable);
    }

    private static void TryApplyDamage(IDamageSource damageSource, IDamageable damageable)
    {
        if (damageSource == null || damageable == null)
            return;

        DamagePair damagePair = new DamagePair(damageSource, damageable);

        if (_handledPairs.Add(damagePair))
            damageSource.TryDamage(damageable);
    }

    private static void ResetHandledPairsIfNeeded()
    {
        if (_handledFrame == Time.frameCount)
            return;

        _handledFrame = Time.frameCount;
        _handledPairs.Clear();
    }

    private readonly struct DamagePair
    {
        private readonly int _sourceHashCode;
        private readonly int _targetHashCode;

        public DamagePair(IDamageSource damageSource, IDamageable damageable)
        {
            _sourceHashCode = damageSource.GetHashCode();
            _targetHashCode = damageable.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is DamagePair other &&
                   _sourceHashCode == other._sourceHashCode &&
                   _targetHashCode == other._targetHashCode;
        }

        public override int GetHashCode()
        {
            return (_sourceHashCode * 397) ^ _targetHashCode;
        }
    }
}
