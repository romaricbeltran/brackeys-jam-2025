using System;
using UnityEngine;

public static class Broadcaster
{
    public static event Action<AudioClipType> OnShortAudioRequest;
    public static event Action<AudioClipType> OnAudioRequest;
    public static event Action OnStopAudioRequest;
    public static event Action<Transform, AnimationType> OnPlayerAnimationRequest;

    public static event Action<GameOverPayLoad> OnGameOver;
    public static event Action<GameOverPayLoad> OnGameStart;
    public static event Action<bool> OnPauseRequest;
    public static event Action OnBackToMainPanel;

    public static void TriggerOnShortAudioRequest(AudioClipType audioClipType)
    {
        OnShortAudioRequest?.Invoke(audioClipType);
    }

    public static void TriggerOnAudioRequest(AudioClipType mainThemeType)
    {
        OnAudioRequest?.Invoke(mainThemeType);
    }
    
    public static void TriggerOnStopAudioRequest()
    {
        OnStopAudioRequest?.Invoke();
    }

    public static void TriggerOnAnimationRequest(Transform sender, AnimationType animationType)
    {
        OnPlayerAnimationRequest?.Invoke(sender, animationType);
    }

    public static void TriggerGameOver(GameOverPayLoad gameOverPayLoad)
    {
        OnGameOver?.Invoke(gameOverPayLoad);

        TriggerOnStopAudioRequest();

        if (gameOverPayLoad.IsVictory)
        {
            TriggerOnAudioRequest(AudioClipType.Victory);
        }
    }

    public static void TriggerOnPauseRequest(bool isActive)
    {
        OnPauseRequest?.Invoke(isActive);
    }

    public static void TriggerOnGameStart(GameOverPayLoad gameOverPayLoad)
    {
        OnGameStart?.Invoke(gameOverPayLoad);
    }

    public static void TriggerOnBackToMainPanel()
    {
        OnBackToMainPanel?.Invoke();
        TriggerOnAudioRequest(AudioClipType.MainTheme);
    }
}

public struct GameOverPayLoad
{
    public bool IsVictory;

    public GameOverPayLoad(bool isVictory = true)
    {
        IsVictory = isVictory;
    }
}