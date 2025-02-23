using GD.MinMaxSlider;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClipsSO _audioClipsSO;
    [SerializeField] private AudioSource m_sfxSource;
    [SerializeField] private AudioSource m_mainMusicSource;
    [SerializeField][MinMaxSlider(0.5f, 2.0f)] private Vector2 _pitchRandomRange;


    private AudioClipType m_currentMainTheme;

    private void Start()
    {
        PlayAudio(AudioClipType.MainTheme);
    }

    private void OnEnable()
    {
        Broadcaster.OnShortAudioRequest -= PlayShortAudio;
        Broadcaster.OnShortAudioRequest += PlayShortAudio;

        Broadcaster.OnAudioRequest -= PlayAudio;
        Broadcaster.OnAudioRequest += PlayAudio;
    }

    private void OnDisable()
    {
        Broadcaster.OnShortAudioRequest -= PlayShortAudio;
        Broadcaster.OnAudioRequest -= PlayAudio;
    }

    private void PlayAudio(AudioClipType mainThemeType)
    {

        if (m_currentMainTheme == mainThemeType)
        {
            Debug.LogWarning("YOU REQUESTED THE SAME MAIN TYPE, WON'T BE PLAYED!!!");

            return; // GUARD CASE
        }

        if (_audioClipsSO.TryGetTargetAudioClipType(mainThemeType, out AudioClip mainThemeClip))
        {
            m_currentMainTheme = mainThemeType;
            m_mainMusicSource.clip = mainThemeClip;
            m_mainMusicSource.Play();
        }
    }

    private void PlayShortAudio(AudioClipType targetAudioClipType)
    {
        if (_audioClipsSO.TryGetTargetAudioClipType(targetAudioClipType, out AudioClip targetClip))
        {
            m_sfxSource.pitch = UnityEngine.Random.Range(_pitchRandomRange.x, _pitchRandomRange.y);
            m_sfxSource.PlayOneShot(targetClip);
        }
    }
}