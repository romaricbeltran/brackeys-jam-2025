using UnityEngine;
using UnityEngine.UI;

public class UIRoot : MonoBehaviour
{
    [SerializeField] RectTransform _mainBackground;

    [Space]
    [Space]

    [SerializeField] RectTransform _mainPanel;
    [SerializeField] RectTransform _settingsPanel;
    [SerializeField] RectTransform _gamePanel;
    [SerializeField] RectTransform _pausePanel;
    [SerializeField] RectTransform _gameOverPanel;
    [SerializeField] RectTransform _carrierDeadOverlay;

    [Space]
    [Space]

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _backFromSettingsButton;
    [SerializeField] private Button _backFromPauseButton;
    [SerializeField] private Button _restartButton;

    private void Start()
    {
        EnablePanel(UIPanelType.MainPanel);
    }

    private void OnEnable()
    {
        Broadcaster.OnGameStart -= HandleGameStart;
        Broadcaster.OnGameStart += HandleGameStart;

        Broadcaster.OnPauseRequest -= HandlePauseRequest;
        Broadcaster.OnPauseRequest += HandlePauseRequest;

        Broadcaster.OnGameOver -= HandleGameOver;
        Broadcaster.OnGameOver += HandleGameOver;

        HealthComponent.OnHealthChanged -= OnHealthChange;
        HealthComponent.OnHealthChanged += OnHealthChange;

        _startButton.onClick.RemoveListener(StartGameRequest);
        _startButton.onClick.AddListener(StartGameRequest);

        _resumeButton.onClick.RemoveListener(ResumeGameRequest);
        _resumeButton.onClick.AddListener(ResumeGameRequest);

        _settingsButton.onClick.RemoveListener(HandleOptionButtonRequest);
        _settingsButton.onClick.AddListener(HandleOptionButtonRequest);

        _backFromSettingsButton.onClick.RemoveListener(HandleBackRequest);
        _backFromSettingsButton.onClick.AddListener(HandleBackRequest);

        _backFromPauseButton.onClick.RemoveListener(HandleBackRequest);
        _backFromPauseButton.onClick.AddListener(HandleBackRequest);

        _restartButton.onClick.RemoveListener(HandleBackRequest);
        _restartButton.onClick.AddListener(HandleBackRequest);
    }

    private void StartGameRequest()
    {
        Broadcaster.TriggerOnGameStart(new GameOverPayLoad());
        EnablePanel(UIPanelType.GamePanel);
    }

    private void ResumeGameRequest()
    {
        Broadcaster.TriggerOnPauseRequest(false);
        EnablePanel(UIPanelType.GamePanel);
    }

    private void OnDisable()
    {
        Broadcaster.OnGameStart -= HandleGameStart;
        Broadcaster.OnPauseRequest -= HandlePauseRequest;
        Broadcaster.OnGameOver -= HandleGameOver;
        HealthComponent.OnHealthChanged -= OnHealthChange;

        _startButton.onClick.RemoveListener(StartGameRequest);
        _resumeButton.onClick.RemoveListener(ResumeGameRequest);
        _settingsButton.onClick.RemoveListener(HandleOptionButtonRequest);
        _backFromSettingsButton.onClick.RemoveListener(HandleBackFromSettings);
        _backFromPauseButton.onClick.RemoveListener(HandleBackRequest);
        _restartButton.onClick.RemoveListener(HandleBackRequest);
    }

    private void HandleGameStart(GameOverPayLoad _)
    {
        EnablePanel(UIPanelType.MainPanel, false);
    }

    private void HandlePauseRequest(bool isActive)
    {
        EnablePanel(UIPanelType.PausePanel, isActive);
    }

    private void HandleGameOver(GameOverPayLoad gameOverPayLoad)
    {
        EnablePanel(UIPanelType.GameOverPanel);
        Debug.Log($"[UIRoot] Is Victory {gameOverPayLoad.IsVictory}");
        _carrierDeadOverlay.gameObject.SetActive(!gameOverPayLoad.IsVictory);
    }

    private void HandleOptionButtonRequest()
    {
        EnablePanel(UIPanelType.SettingsPanel);
    }

    private void HandleBackFromSettings()
    {
        EnablePanel(UIPanelType.MainPanel);
    }

    private void HandleBackRequest()
    {
        Broadcaster.TriggerOnBackToMainPanel();
        EnablePanel(UIPanelType.MainPanel);
    }

    private void OnHealthChange(HealthComponent healthComponent)
    {
        if(healthComponent.transform.CompareTag("Carrier"))
        {
            if(healthComponent.IsDead)
            {
                Broadcaster.TriggerGameOver(new GameOverPayLoad(false));
            }
        }
    }

    private void EnablePanel(UIPanelType panelType, bool isActive = true)
    {
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }

        Broadcaster.TriggerOnShortAudioRequest(AudioClipType.ButtonClick);

        _mainBackground.gameObject.SetActive(false);
        _mainPanel.gameObject.SetActive(false);
        _gamePanel.gameObject.SetActive(false);
        _pausePanel.gameObject.SetActive(false);
        _settingsPanel.gameObject.SetActive(false);
        _gameOverPanel.gameObject.SetActive(false);

        switch (panelType)
        {
            case UIPanelType.MainPanel:
                _mainBackground.gameObject.SetActive(isActive);
                _mainPanel.gameObject.SetActive(isActive);
                break;

            case UIPanelType.PausePanel:
                Time.timeScale = 0f;
                _pausePanel.gameObject.SetActive(isActive);
                break;

            case UIPanelType.GamePanel:
                _gamePanel.gameObject.SetActive(isActive);
                break;

            case UIPanelType.SettingsPanel:
                _mainBackground.gameObject.SetActive(isActive);
                _settingsPanel.gameObject.SetActive(isActive);
                break;

            case UIPanelType.GameOverPanel:
                _mainBackground.gameObject.SetActive(isActive);
                _gameOverPanel.gameObject.SetActive(isActive);
                break;

            default:
                _mainPanel.gameObject.SetActive(true);
                break;
        }
    }
}

public enum UIPanelType
{
    None,
    MainPanel,
    GamePanel,
    PausePanel,
    SettingsPanel,
    GameOverPanel
}
