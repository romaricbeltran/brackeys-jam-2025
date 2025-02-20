using System;

public static class Broadcaster
{
    public static event Action<AudioClipType> OnAudioRequest;

    public static void TriggerOnAudioRequest(AudioClipType audioClipType)
    {
        OnAudioRequest?.Invoke(audioClipType);
    }
}
