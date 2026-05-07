using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

public class LevelUI : MonoBehaviour, IStartable, IDisposable
{
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private TMP_Text _stateText;

    [Header("Panels")] [SerializeField] private GameObject _preparationPanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private GameObject _defeatPanel;

    [Header("Buttons")] [SerializeField] private Button _startButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button[] _nextLevelButtons = Array.Empty<Button>();
    [SerializeField] private Button[] _restartButtons = Array.Empty<Button>();
    [SerializeField] private Button[] _mainMenuButtons = Array.Empty<Button>();

    private IMoneyService _moneyService;
    private ILevelFlow _levelFlow;

    [Inject]
    public void Construct(IMoneyService moneyService, ILevelFlow levelFlow)
    {
        _moneyService = moneyService;
        _levelFlow = levelFlow;
    }

    public void Start()
    {
        _moneyService.BalanceChanged += UpdateMoneyText;
        _levelFlow.StateChanged += UpdateState;

        AddButtonListener(_startButton, _levelFlow.StartLevel);
        AddButtonListener(_pauseButton, _levelFlow.Pause);
        AddButtonListener(_resumeButton, _levelFlow.Resume);
        AddButtonListeners(_nextLevelButtons, _levelFlow.GoToNextLevel);
        AddButtonListeners(_restartButtons, _levelFlow.Restart);
        AddButtonListeners(_mainMenuButtons, _levelFlow.ExitToMainMenu);

        UpdateMoneyText(_moneyService.Balance);
        UpdateState(_levelFlow.State);
    }

    public void Dispose()
    {
        if (_moneyService != null)
            _moneyService.BalanceChanged -= UpdateMoneyText;

        if (_levelFlow != null)
        {
            _levelFlow.StateChanged -= UpdateState;
            RemoveButtonListener(_startButton, _levelFlow.StartLevel);
            RemoveButtonListener(_pauseButton, _levelFlow.Pause);
            RemoveButtonListener(_resumeButton, _levelFlow.Resume);
            RemoveButtonListeners(_nextLevelButtons, _levelFlow.GoToNextLevel);
            RemoveButtonListeners(_restartButtons, _levelFlow.Restart);
            RemoveButtonListeners(_mainMenuButtons, _levelFlow.ExitToMainMenu);
        }
    }

    private void UpdateMoneyText(int money)
    {
        if (_moneyText != null)
            _moneyText.text = money.ToString();
    }

    private void UpdateState(LevelState state)
    {
        SetActive(_preparationPanel, state == LevelState.Preparation);
        SetActive(_pausePanel, state == LevelState.Paused);
        SetActive(_victoryPanel, state == LevelState.Victory);
        SetActive(_defeatPanel, state == LevelState.Defeat);

        if (_pauseButton != null)
            _pauseButton.gameObject.SetActive(state == LevelState.Playing);

        foreach (var button in _nextLevelButtons)
        {
            if (button != null)
            {
                button.gameObject.SetActive(state == LevelState.Victory || state == LevelState.Defeat);
                button.interactable = _levelFlow.CanGoToNextLevel;
            }
        }

        if (_stateText != null)
            _stateText.text = state.ToString();
    }

    private static void SetActive(GameObject target, bool isActive)
    {
        if (target != null)
            target.SetActive(isActive);
    }

    private static void AddButtonListeners(Button[] buttons, UnityAction action)
    {
        if (buttons == null)
            return;

        for (int i = 0; i < buttons.Length; i++)
            AddButtonListener(buttons[i], action);
    }

    private static void RemoveButtonListeners(Button[] buttons, UnityAction action)
    {
        if (buttons == null)
            return;

        for (int i = 0; i < buttons.Length; i++)
            RemoveButtonListener(buttons[i], action);
    }

    private static void AddButtonListener(Button button, UnityAction action)
    {
        if (button != null)
            button.onClick.AddListener(action);
    }

    private static void RemoveButtonListener(Button button, UnityAction action)
    {
        if (button != null)
            button.onClick.RemoveListener(action);
    }
}