using System;
using System.Collections.Generic;
using UnityEngine;

public static class Broadcaster
{
    public static event Action<AudioClipType> OnAudioRequest;
    public static event Action<Transform, AnimationType> OnPlayerAnimationRequest;

    public static void TriggerOnAudioRequest(AudioClipType audioClipType)
    {
        OnAudioRequest?.Invoke(audioClipType);
    }

    public static void TriggerOnAnimationRequest(Transform sender, AnimationType animationType)
    {
        OnPlayerAnimationRequest?.Invoke(sender, animationType);
    }
}
