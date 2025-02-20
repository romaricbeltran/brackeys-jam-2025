using System;

public static class Broadcaster
{
    public static event Action<AudioClipType> OnAudioRequest;
    public static event Action<AnimationType> OnPlayerAnimationRequest;

    public static void TriggerOnAudioRequest(AudioClipType audioClipType)
    {
        OnAudioRequest?.Invoke(audioClipType);
    }

    public static void TriggerOnAnimationRequest(AnimationType animationType)
    {
        OnPlayerAnimationRequest?.Invoke(animationType);
    }
}
