using GD.MinMaxSlider;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClipsSO _audioClipsSO;
    [SerializeField] private AudioSource m_sfxSource;
    [SerializeField] private AudioSource m_mainMusicSource;
    [SerializeField][MinMaxSlider(0.5f, 2.0f)] private Vector2 _pitchRandomRange;

    private void Start()
    {
        if(_audioClipsSO.TryGetTargetAudioClipType(AudioClipType.MainTheme, out AudioClip mainThemeClip))
        {
            m_mainMusicSource.clip = mainThemeClip;
            m_mainMusicSource.Play();
        }
    }

    private void OnEnable()
    {
        Broadcaster.OnAudioRequest -= PlaySingleAudio;
        Broadcaster.OnAudioRequest += PlaySingleAudio;
    }

    private void OnDisable()
    {
        Broadcaster.OnAudioRequest -= PlaySingleAudio;
    }

    private void PlaySingleAudio(AudioClipType targetAudioClipType)
    {
        if (_audioClipsSO.TryGetTargetAudioClipType(targetAudioClipType, out AudioClip targetClip))
        {
            m_sfxSource.pitch = UnityEngine.Random.Range(_pitchRandomRange.x, _pitchRandomRange.y);
            m_sfxSource.PlayOneShot(targetClip);
        }
    }
}