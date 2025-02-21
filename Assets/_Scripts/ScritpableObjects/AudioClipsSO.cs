using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AudioClipsSO : ScriptableObject
{
    [SerializeField] private List<AudioClipHandler> _audioClipHandlers;

    public List<AudioClipHandler> AudioClipHandlers => _audioClipHandlers;

    public bool TryGetTargetAudioClipType(AudioClipType audioClipTypeTarget, out AudioClip targetAudioCLip)
    {
        targetAudioCLip = null;

        foreach (var audioClipHandler in AudioClipHandlers)
        {
            if (audioClipHandler.AudioClipType == audioClipTypeTarget)
            {
                targetAudioCLip = audioClipHandler.TargetClip;
                return true;
            }
        }

        return false;
    }
}

[Serializable]
public class AudioClipHandler
{
    [SerializeField] private AudioClipType _audioClipType;
    [SerializeField] private AudioClip _audioClip;
    public AudioClipType AudioClipType => _audioClipType;
    public AudioClip TargetClip => _audioClip;
}

public enum AudioClipType
{
    None,
    MainTheme,
    PickupFlower,
    ActivateLever,
    BringFlowerToCarrier,
    Dash,
    CarrierHit,
    CarrierDies,
    Projectile,
    ButtonClick,
    GameOver
}
