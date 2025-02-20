using System;
using UnityEngine;

public static class Broadcaster
{
    public static event Action<AudioClipType> OnAudioRequest;
    public static event Action<Transform, AnimationType> OnPlayerAnimationRequest;
    public static event Action<GameOverPayLoad> OnGameOver;

    public static void TriggerOnAudioRequest(AudioClipType audioClipType)
    {
        OnAudioRequest?.Invoke(audioClipType);
    }

    public static void TriggerOnAnimationRequest(Transform sender, AnimationType animationType)
    {
        OnPlayerAnimationRequest?.Invoke(sender, animationType);
    }

    public static void TriggerGameOver(GameOverPayLoad gameOverPayLoad)
    {
        OnGameOver?.Invoke(gameOverPayLoad);
    }
}

public struct GameOverPayLoad
{

}
