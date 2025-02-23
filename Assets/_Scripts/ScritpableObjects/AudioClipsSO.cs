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
                targetAudioCLip = audioClipHandler.RandomAudioClip;
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
    [SerializeField] private List<AudioClip> _audioClips;
    public AudioClipType AudioClipType => _audioClipType;
    public AudioClip RandomAudioClip => GetRandomAudioClip();

    private AudioClip GetRandomAudioClip()
    {
        if (_audioClips.Count > 0)
        {
            List<AudioClip> tempList = _audioClips;

            if (_audioClip != null)
            {
                tempList.Add(_audioClip);
            }

            int randomIndex = UnityEngine.Random.Range(0, tempList.Count);
            return tempList[randomIndex];
        }

        return _audioClip;
    }
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
    PlayerHit,
    CarrierDies,
    Projectile,
    ButtonClick,
    GameOver,
    Victory
}
