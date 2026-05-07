using System.Text;
using TMPro;
using UnityEngine;
using VContainer;

public class DebugOverlay : MonoBehaviour
{
    private const KeyCode ToggleKey = KeyCode.F;
    private const float RefreshInterval = 0.1f;

    private readonly StringBuilder _builder = new StringBuilder(256);

    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private bool _isVisibleInAwake;

    private IMoneyService _money;
    private ILevelStateProvider _levelState;
    
    private PlayerRunner _player;
    private Health _health;
    private Weapon _weapon;
    private float _nextRefreshTime;

    [Inject]
    private void Construct(PlayerRunner player, Health health, Weapon weapon, IMoneyService money, ILevelStateProvider levelState)
    {
        _player = player;
        _health = health;
        _weapon = weapon;
        _money = money;
        _levelState = levelState;
    }

    private void Awake()
    {
        SetVisible(_isVisibleInAwake);
    }

    private void Update()
    {
        if (_panel == null || _text == null)
            return;

        if (Input.GetKeyDown(ToggleKey))
            SetVisible(_panel.activeSelf == false);

        if (_panel.activeSelf == false || Time.unscaledTime < _nextRefreshTime)
            return;

        _nextRefreshTime = Time.unscaledTime + RefreshInterval;
        RefreshText();
    }

    private void SetVisible(bool isVisible)
    {
        if (_panel != null)
            _panel.SetActive(isVisible);

        if (_canvas != null)
            _canvas.enabled = true;

        _nextRefreshTime = 0f;
    }

    private void RefreshText()
    {
        _builder.Clear();
        _builder.AppendLine("DEBUG");
        _builder.AppendLine($"Toggle: {ToggleKey}");
        _builder.AppendLine();

        if (_player == null)
        {
            _builder.AppendLine("Player: not found");
            _text.text = _builder.ToString();
            return;
        }

        _builder.AppendLine($"HP: {FormatHealth()}");
        _builder.AppendLine($"Fire rate: {FormatFireRate()}");
        _builder.AppendLine($"Money: {FormatMoney()}");
        _builder.AppendLine($"Level state: {FormatLevelState()}");
        _builder.AppendLine($"Weapon: {FormatWeapon()}");
        _builder.AppendLine($"Position: {FormatPosition(_player.transform.position)}");

        _text.text = _builder.ToString();
    }

    private static string FormatPosition(Vector3 position)
    {
        return $"{position.x:0.##}, {position.y:0.##}, {position.z:0.##}";
    }
    
    private string FormatHealth()
    {
        return _health != null ? $"{_health.CurrentHealth}/{_health.MaxHealth}" : "not found";
    }

    private string FormatFireRate()
    {
        return _weapon != null ? $"{_weapon.ShotsPerSecond:0.##}/s x{_weapon.FireRateMultiplier:0.##}" : "not found";
    }

    private string FormatWeapon()
    {
        return _weapon != null && _weapon.Config != null ? _weapon.Config.name : "not found";
    }

    private string FormatMoney()
    {
        return _money.Balance.ToString();
    }

    private string FormatLevelState()
    {
        return _levelState != null ? _levelState.State.ToString() : "not found";
    }
}
