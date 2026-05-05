using UnityEngine;

[RequireComponent(typeof(Health))]
public class HealthIndicator : MonoBehaviour
{
    [SerializeField] private Vector3 _offset = new Vector3(0f, 1.4f, 0f);
    [SerializeField] private Vector2 _size = new Vector2(1.2f, 0.12f);
    [SerializeField] private float _thickness = 0.02f;
    [SerializeField] private Color _backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.85f);
    [SerializeField] private Color _fillColor = new Color(0.15f, 0.9f, 0.25f, 1f);
    [SerializeField] private bool _hideWhenFull;

    private Health _health;
    private Transform _viewRoot;
    private Transform _fill;
    private Vector3 _fillFullScale;

    private void Awake()
    {
        _health = GetComponent<Health>();
        CreateView();
    }

    private void OnEnable()
    {
        if (_health != null)
            _health.Changed += OnHealthChanged;
    }

    private void Start()
    {
        Refresh(_health.CurrentHealth, _health.MaxHealth);
    }

    private void LateUpdate()
    {
        _viewRoot.position = transform.position + _offset;

        if (Camera.main != null)
            _viewRoot.rotation = Camera.main.transform.rotation;
    }

    private void OnDisable()
    {
        if (_health != null)
            _health.Changed -= OnHealthChanged;
    }

    private void OnHealthChanged(int currentHealth, int maxHealth)
    {
        Refresh(currentHealth, maxHealth);
    }

    private void Refresh(int currentHealth, int maxHealth)
    {
        float fillAmount = maxHealth > 0 ? Mathf.Clamp01((float)currentHealth / maxHealth) : 0f;

        _fill.localScale = new Vector3(_fillFullScale.x * fillAmount, _fillFullScale.y, _fillFullScale.z);
        _fill.localPosition = new Vector3((_fillFullScale.x - _fill.localScale.x) * -0.5f, 0f, -_thickness);
        _viewRoot.gameObject.SetActive(_hideWhenFull == false || fillAmount < 1f);
    }

    private void CreateView()
    {
        //TODO убрать хардкод текста
        _viewRoot = new GameObject("HealthIndicator").transform;
        _viewRoot.SetParent(transform);

        Transform background = CreateBarPart("Background", _backgroundColor);
        background.SetParent(_viewRoot);
        background.localPosition = Vector3.zero;
        background.localScale = new Vector3(_size.x, _size.y, _thickness);

        _fill = CreateBarPart("Fill", _fillColor);
        _fill.SetParent(_viewRoot);
        _fillFullScale = new Vector3(_size.x, _size.y, _thickness);
    }

    private static Transform CreateBarPart(string name, Color color)
    {
        GameObject part = GameObject.CreatePrimitive(PrimitiveType.Cube);
        part.name = name;

        if (part.TryGetComponent(out Collider collider))
            Destroy(collider);

        Renderer renderer = part.GetComponent<Renderer>();
        renderer.material.color = color;

        return part.transform;
    }
}
