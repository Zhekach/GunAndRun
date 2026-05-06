using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class StatusEffectGate : MonoBehaviour, IDamageable
{
    [SerializeField] private StatusEffectTarget _target;
    [SerializeField] private float _value = 0.5f;
    [SerializeField] private bool _canIncreaseValue = true;
    [SerializeField] private float _increasePerHit = 0.1f;
    [SerializeField] private float _maxValue = 1.2f;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private string _valueFormat = "+0.##;-0.##;0";

    public GameObject GameObject => gameObject;
    public int CurrentHealth => 1;
    public int MaxHealth => 1;
    public bool IsAlive => true;

    private void Awake()
    {
        Collider trigger = GetComponent<Collider>();
        trigger.isTrigger = true;

        if (_label == null)
            _label = GetComponentInChildren<TMP_Text>();

        UpdateLabel();
    }

    private void OnValidate()
    {
        if (_maxValue < _value)
            _maxValue = _value;

        if (_increasePerHit < 0f)
            _increasePerHit = 0f;

        UpdateLabel();
    }

    private void OnTriggerEnter(Collider other)
    {
        StatusEffectReceiver receiver = other.GetComponentInParent<StatusEffectReceiver>();

        if (receiver == null)
            return;

        receiver.Apply(_target, _value, gameObject);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage, GameObject damageSource)
    {
        if (_canIncreaseValue == false || damage <= 0)
            return;

        _value = Mathf.Min(_maxValue, _value + _increasePerHit);
        UpdateLabel();
    }

    private void UpdateLabel()
    {
        if (_label == null)
            return;

        _label.text = $"{GetDisplayName(_target)}\n{_value.ToString(_valueFormat)}";
    }

    private static string GetDisplayName(StatusEffectTarget target)
    {
        switch (target)
        {
            case StatusEffectTarget.Health:
                return "Health";

            case StatusEffectTarget.FireRateMultiplier:
                return "Fire Rate";

            default:
                return target.ToString();
        }
    }
}
